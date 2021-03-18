using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDMToM.Models
{
    public class Skill
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string SkillName { get; set; }      
        public IList<Employeeskill> Employeeskills { get; set; }
    }
}
