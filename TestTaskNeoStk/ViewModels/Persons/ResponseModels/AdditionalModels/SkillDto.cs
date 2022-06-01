using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTaskNeoStk.ViewModels.Persons.ResponseModels.AdditionalModels
{
    /// <summary>
    /// Модель ДТО для навыка
    /// </summary>
    public class SkillDto
    {
        /// <summary>
        /// Идентификатор 
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Название навыка
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Уровень навыка
        /// </summary>
        public byte Level { get; set; }
    }
}
