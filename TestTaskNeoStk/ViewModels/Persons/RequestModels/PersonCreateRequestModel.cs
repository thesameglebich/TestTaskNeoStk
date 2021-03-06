using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestTaskNeoStk.ViewModels.Persons.RequestModels
{
    /// <summary>
    /// Модель создания сотрудника
    /// </summary>
    public class PersonCreateRequestModel
    {
        /// <summary>
        /// Имя
        /// </summary>
        [Required]
        [MinLength(1)]
        public string Name { get; set; }

        /// <summary>
        /// Отображаемое имя
        /// </summary>
        [MinLength(1)]
        public string? DisplayName { get; set; }

        /// <summary>
        /// Навыки
        /// </summary>
        public List<SkillRequestModel> Skills { get; set; }
    }
}
