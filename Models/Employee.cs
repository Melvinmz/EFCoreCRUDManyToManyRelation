using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDMToM.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [MaxLength(50)]
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [MaxLength(50)]
        [Required]
        [Display(Name = "Last Name")]
        public String LastName { get; set; }
        public IList<Employeeskill> Employeeskills { get; set; }
     
    }
}
