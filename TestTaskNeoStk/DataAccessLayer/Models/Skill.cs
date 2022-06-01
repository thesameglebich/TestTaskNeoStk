using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestTaskNeoStk.DataAccessLayer.Models
{
    /// <summary>
    /// Навык сотрудника
    /// </summary>
    public class Skill: BaseEntity
    {
        /// <summary>
        /// Название навыка
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Уровень навыка
        /// </summary>
        [Range(1, 10)]
        [Required]
        public byte Level { get; set; }

        public long PersonId { get; set; }

        /// <summary>
        /// Сотрудник, к котором принадлежит навык
        /// </summary>
        public Person Person { get; set; }
    }
}
