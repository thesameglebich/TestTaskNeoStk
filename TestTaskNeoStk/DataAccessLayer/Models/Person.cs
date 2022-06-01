using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestTaskNeoStk.DataAccessLayer.Models
{
    /// <summary>
    /// Сотрудник компании
    /// </summary>
    public class Person: BaseEntity
    {
        /// <summary>
        /// Имя сотрудника
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Отображаемое имя сотрудника
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Навыки сотрудника
        /// </summary>
        public ICollection<Skill> Skills { get; set; }
    }
}
