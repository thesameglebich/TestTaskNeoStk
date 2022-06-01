using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTaskNeoStk.CommonModels;
using TestTaskNeoStk.ViewModels.Persons.RequestModels;

namespace TestTaskNeoStk.Services
{
    public interface IPersonCrudService
    {
        /// <summary>
        /// Создание сотрудника
        /// </summary>
        public Result Create(PersonCreateRequestModel personModel);

        /// <summary>
        /// Редактирование сотрудника
        /// </summary>
        public Result Edit(PersonEditRequestModel personModel);

        /// <summary>
        /// Удаление сотрудника
        /// </summary>
        public Result Delete(long id);
    }
}
