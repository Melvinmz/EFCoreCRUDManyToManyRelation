using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRUDMToM.Data;
using CRUDMToM.Models;
using System.Diagnostics;

namespace CRUDMToM.Pages
{
    public class EditModel : PageModel
    {
        private readonly CRUDMToM.Data.AppDbContext _context;
        [BindProperty]
        public IList<SelectListItem> SkillList { get; set; }

        public EditModel(CRUDMToM.Data.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Employee Employee { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Employee = await _context.Empployees.Include(m => m.Employeeskills)
                .FirstOrDefaultAsync(m => m.Id == id);
            SkillList = _context.Skills.ToList<Skill>().Select(m => new SelectListItem
            {
                Text = m.SkillName,
                Value = m.Id.ToString(),
                Selected = Employee.Employeeskills.Any(S => S.SkillId == m.Id) ? true : false
            }).ToList<SelectListItem>();


            if (Employee == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Employee EmployeeFromDB = await _context.Empployees.
                Include(m => m.Employeeskills).FirstOrDefaultAsync(m => m.Id == Employee.Id);
            IList<Employeeskill> Employeeskills = new List<Employeeskill>();
            //variable to hold removed skills 
            IList<Employeeskill> SkillsToRemove = new List<Employeeskill>();
            //variable to hold newly added skills 
            IList<Employeeskill> SkillsToAdd = new List<Employeeskill>();
            foreach (SelectListItem skill in SkillList)
            {
                if (skill.Selected)
                {
                    // Add all the selected skills to Employeeskills collection.
                    Employeeskills.Add(new Employeeskill 
                    { EmployeeId = Employee.Id, SkillId = Convert.ToInt32(skill.Value) });
                    //if a new skill is assigned to the employee it is added
                    //to the SkillsToAdd collection
                    Employeeskill selectedSkill = EmployeeFromDB.Employeeskills.
                        Where(m => m.SkillId == Convert.ToInt32(skill.Value)).FirstOrDefault();
                    if (selectedSkill==null)
                    {
                        SkillsToAdd.Add(new Employeeskill
                        { EmployeeId = Employee.Id, SkillId = Convert.ToInt32(skill.Value) });
                       
                    }
                }
            }
            //If a skill is not in the edited skill list, but present
            //in the skill list from the DB, it is added to 
            // the SkillsToRemove collection.
            foreach (Employeeskill employeeskill in EmployeeFromDB.Employeeskills)
            {
                if (Employeeskills.Any(e => e.EmployeeId == employeeskill.EmployeeId
                && e.SkillId == employeeskill.SkillId) == false)
                {
                    SkillsToRemove.Add(employeeskill);
                }

            }
            //Section which assigns the modified values 
            //to the employee entity from the database
            EmployeeFromDB.FirstName = Employee.FirstName;
            EmployeeFromDB.LastName = Employee.LastName;
            //Delete the skills which are to be removed
            _context.RemoveRange(SkillsToRemove);
            //Adding newly assigned skills
            foreach (var empSKill in SkillsToAdd)
            {

                EmployeeFromDB.Employeeskills.Add(empSKill);
            }           

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(Employee.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool EmployeeExists(int id)
        {
            return _context.Empployees.Any(e => e.Id == id);
        }
    }
}
