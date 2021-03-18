using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDMToM.Models
{
    [Table("EmployeeSkills")]
    public class Employeeskill
    {        
        public Employee Employees { get; set; }
        public int EmployeeId { get; set; }

        public Skill Skills { get; set; }
        public int SkillId { get; set; }
                
    }
}
