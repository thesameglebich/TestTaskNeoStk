using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestTaskNeoStk.ViewModels.Persons.RequestModels
{
    public class SkillRequestModel
    {
        public long? Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(1,10)]
        public byte Level { get; set; }
    }
}
