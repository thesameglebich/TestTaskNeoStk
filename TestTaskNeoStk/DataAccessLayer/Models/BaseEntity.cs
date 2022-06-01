using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTaskNeoStk.DataAccessLayer.Models
{
    /// <summary>
    /// Базовый класс для всех классов модели предметной области.
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// Идентификатор сущности
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Дата создания сущности
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Дата редактирования сущности
        /// </summary>
        public DateTime? ModifiedOn { get; set; }
    }
}
