using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TestTaskNeoStk.CommonModels.Attributes;
using TestTaskNeoStk.DataAccessLayer;
using TestTaskNeoStk.Services;
using TestTaskNeoStk.ViewModels.Persons.RequestModels;
using TestTaskNeoStk.ViewModels.Persons.ResponseModels;
using TestTaskNeoStk.ViewModels.Persons.ResponseModels.AdditionalModels;

namespace TestTaskNeoStk.Controllers
{
    [ApiController]
    [Route("/api/v1")]
    public class PersonController: ControllerBase
    {
        private readonly DB _db;
        private readonly IPersonCrudService _personCrudService;
        public PersonController(DB dB, IPersonCrudService personCrudService)
        {
            _db = dB;
            _personCrudService = personCrudService;
        }

        /// <summary>
        /// Получить список всех сотрудников
        /// </summary>
        [HttpGet("persons")]
        public IActionResult GetPersons()
        {
            var response = _db.Persons
                .Select(p => new PersonResponseModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    DisplayName = p.DisplayName ?? p.Name,
                    Skills = p.Skills
                        .Select(s => new SkillDto
                        {
                            Id = s.Id,
                            Name = s.Name,
                            Level = s.Level
                        })
                        .ToList()
                })
                .ToList();

            return Ok(response);
        }

        /// <summary>
        /// Получить сотрудника
        /// </summary>
        [HttpGet("person/{id}")]
        public IActionResult GetPerson(long id)
        {
            var result = new PersonResponseModel().Init(id, _db);

            if (result.HttpStatusCode != HttpStatusCode.OK)
            {
                return StatusCode((int)result.HttpStatusCode, result.Errors);
            }

            return Ok(result.Entity);
        }

        /// <summary>
        /// Создать сотрудника
        /// </summary>
        [ValidateModel]
        [HttpPost("person")]
        public IActionResult Create([FromBody] PersonCreateRequestModel person)
        {
            var result = _personCrudService.Create(person);

            if (result.HttpStatusCode != HttpStatusCode.OK)
            {
                return StatusCode((int)result.HttpStatusCode, result.Errors);
            }

            return Ok();
        }

        /// <summary>
        /// Обновить информацию о сотруднике
        /// </summary>
        [ValidateModel]
        [HttpPut("person/{id}")]
        public IActionResult Edit([FromBody] PersonEditRequestModel person, long id)
        {
            person.Id = id;
            var result = _personCrudService.Edit(person);

            if (result.HttpStatusCode != HttpStatusCode.OK)
            {
                return StatusCode((int)result.HttpStatusCode, result.Errors);
            }

            return Ok();
        }

        /// <summary>
        /// Обновить информацию о сотруднике
        /// </summary>
        [HttpDelete("person/{id}")]
        public IActionResult Delete(long id)
        {
            var result = _personCrudService.Delete(id);

            if (result.HttpStatusCode != HttpStatusCode.OK)
            {
                return StatusCode((int)result.HttpStatusCode, result.Errors);
            }

            return Ok();
        }
    }
}
