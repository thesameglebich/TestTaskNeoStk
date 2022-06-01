using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TestTaskNeoStk.CommonModels;
using TestTaskNeoStk.DataAccessLayer;
using TestTaskNeoStk.DataAccessLayer.Models;
using TestTaskNeoStk.ViewModels.Persons.ResponseModels.AdditionalModels;

namespace TestTaskNeoStk.ViewModels.Persons.ResponseModels
{
    /// <summary>
    /// Модель ответа для сотрудника компании
    /// </summary>
    public class PersonResponseModel
    {
        /// <summary>
        /// Идентификатор сотрудника
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Отображаемое имя
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Навыки
        /// </summary>
        public List<SkillDto> Skills { get; set; }

        public PersonResponseModel() { }

        public Result<PersonResponseModel> Init(long id, DB dB)
        {
            var person = dB.Persons
                .Include(p => p.Skills)
                .FirstOrDefault(p => p.Id == id);

            if (person == null)
            {
                return new Result<PersonResponseModel>(HttpStatusCode.NotFound, new Error("Сотрудник не найден"));
            }

            var response = new PersonResponseModel
            {
                Id = person.Id,
                Name = person.Name,
                DisplayName = person.DisplayName ?? person.Name,
                Skills = person.Skills
                    .Select(s => new SkillDto
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Level = s.Level
                    })
                    .ToList()
            };

            return new Result<PersonResponseModel>(response);
        }
    }
}
