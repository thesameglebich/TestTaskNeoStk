using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TestTaskNeoStk.CommonModels;
using TestTaskNeoStk.DataAccessLayer;
using TestTaskNeoStk.DataAccessLayer.Models;
using TestTaskNeoStk.ViewModels.Persons.RequestModels;
using TestTaskNeoStk.ViewModels.Persons.ResponseModels.AdditionalModels;

namespace TestTaskNeoStk.Services
{
    public class PersonCrudService : IPersonCrudService
    {
        private readonly DB _db;

        public PersonCrudService(DB db)
        {
            _db = db;
        }

        /// <summary>
        /// Создание сотрудника
        /// </summary>
        public Result Create(PersonCreateRequestModel personModel)
        {
            var person = new Person
            {
                CreatedOn = DateTime.Now,
                Name = personModel.Name,
                DisplayName = personModel.DisplayName
            };

            _db.Add(person);
            _db.SaveChanges();

            foreach (var skillModel in personModel.Skills)
            {
                var skill = new Skill
                {
                    CreatedOn = DateTime.Now,
                    Name = skillModel.Name,
                    Level = skillModel.Level,
                    PersonId = person.Id
                };

                _db.Add(skill);
            }

            _db.SaveChanges();

            return new Result(HttpStatusCode.OK);
        }

        /// <summary>
        /// Удаление сотрудника
        /// </summary>
        public Result Delete(long id)
        {
            var person = _db.Persons.FirstOrDefault(p => p.Id == id);

            if (person == null)
            {
                return new Result(HttpStatusCode.NotFound, new Error("Сотрудник не найден"));
            }

            _db.Persons.Remove(person);
            _db.SaveChanges();

            return new Result(HttpStatusCode.OK);
        }

        /// <summary>
        /// Редактирование сотрудника
        /// </summary>
        public Result Edit(PersonEditRequestModel personModel)
        {
            var person = _db.Persons.FirstOrDefault(p => p.Id == personModel.Id);

            if (person == null)
            {
                return new Result(HttpStatusCode.NotFound, new Error("Сотрудник не найден"));
            }

            var validateResult = ValidateSkillIds(personModel.Skills);
            if (validateResult.HttpStatusCode != HttpStatusCode.OK)
            {
                return validateResult;
            }

            person.Name = personModel.Name;
            person.DisplayName = personModel.DisplayName;
            UpdateSkills(person.Id, personModel.Skills);

            _db.SaveChanges();

            return new Result(HttpStatusCode.OK);
        }

        /// <summary>
        /// Проверка на то, был(и) ли передан(ы) некорректный(ые) Id(s)
        /// </summary>
        private Result ValidateSkillIds(IList<SkillRequestModel> modelSkills)
        {
            var modelskillIds = modelSkills
                .Where(s => s.Id != null)
                .Select(s => s.Id)
                .ToList();


            var hasWrongIds = _db.Skills
                .Where(s => modelskillIds.Contains(s.Id))
                .Count() != modelskillIds.Count();

            if (hasWrongIds)
            {
                return new Result(HttpStatusCode.BadRequest,
                    new Error("Ошибка при передаче Id навыка. Есть Id, которого не сущетсвует"));
            }

            return new Result(HttpStatusCode.OK);
        }

        /// <summary>
        /// Обновить навыки сотрудника
        /// </summary>
        private void UpdateSkills(long personId, List<SkillRequestModel> modelSkills)
        {
            RemoveOldSkills(modelSkills);
            CreateNewSkills(modelSkills, personId);
            UpdateExistingSkills(modelSkills);
        }

        /// <summary>
        /// Удалить старые навыки сотрудника
        /// </summary>
        private void RemoveOldSkills(List<SkillRequestModel> modelSkills)
        {
            var skillIds = modelSkills.Select(s => s.Id);

            //Удаление старых навыков
            var deletedSkills = _db.Skills
                .Where(s => !skillIds.Contains(s.Id))
                .ToList();

            foreach (var skill in deletedSkills)
            {
                _db.Skills.Remove(skill);
            }
        }

        /// <summary>
        /// Создать новые навыки сотрудника
        /// </summary>
        private void CreateNewSkills(List<SkillRequestModel> modelSkills, long personId)
        {
            var newSkills = modelSkills.Where(s => s.Id == null);

            foreach (var skill in newSkills)
            {
                _db.Add(new Skill
                {
                    CreatedOn = DateTime.Now,
                    Name = skill.Name,
                    Level = skill.Level,
                    PersonId =personId
                });
            }
        }

        /// <summary>
        /// Обновить существующие навыки сотрудника
        /// </summary>
        private void UpdateExistingSkills(List<SkillRequestModel> modelSkills)
        {
            var skillIds = modelSkills.Select(s => s.Id);

            var updatedSkills = _db.Skills
                .Where(s => skillIds.Contains(s.Id))
                .ToList();

            foreach (var skill in updatedSkills)
            {
                var modelSkill = modelSkills.First(s => s.Id == skill.Id);

                skill.Name = modelSkill.Name;
                skill.Level = modelSkill.Level;
                skill.ModifiedOn = DateTime.Now;
            }
        }
    }
}
