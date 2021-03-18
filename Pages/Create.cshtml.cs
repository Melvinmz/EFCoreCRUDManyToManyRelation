using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CRUDMToM.Data;
using CRUDMToM.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CRUDMToM.Pages
{
    public class CreateModel : PageModel
    {
        private readonly CRUDMToM.Data.AppDbContext _context;
        [BindProperty]
        public IList<SelectListItem> SkillList { get; set; }
        [BindProperty]
        public Employee Employee { get; set; }

        [BindProperty]
        [MaxLength(50)]
        [Display(Name = "Add a New Skill")]
        public String NewSkill { get; set; }
        public CreateModel(CRUDMToM.Data.AppDbContext context)
        {
            _context = context;
        }
        public IActionResult OnGet()
        {
            SkillList = _context.Skills.ToList<Skill>().Select(m=>new SelectListItem {Text= m.SkillName,Value=m.Id.ToString()}).ToList<SelectListItem>();
            return Page();
        }      
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }          
            IList<Employeeskill> Employeeskills=new List<Employeeskill>();      
        
            foreach (SelectListItem skill in SkillList)
            {
                if (skill.Selected)
                {
                    Employeeskills.Add(new Employeeskill {SkillId= Convert.ToInt32(skill.Value)}); 
                }
            } 
            //checking if a new skills was added or not
            if(!string.IsNullOrEmpty(NewSkill))
            {
                //when a new skill is added, create a new skill instance and assign it to and EmployeeSkill entity. 
                //It is then assigned to a collection of Employeeskills
                Skill skill = new Skill { SkillName = NewSkill};
                Employeeskill employeeskill = new Employeeskill { Skills = skill };
               Employeeskills.Add(employeeskill);   
            }                   
            //The collection of Employeeskills is assigned to the Employee entity and saved to the database
            Employee.Employeeskills = Employeeskills;        
            _context.Empployees.Add(Employee);            
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
