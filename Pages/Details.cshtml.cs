using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CRUDMToM.Data;
using CRUDMToM.Models;

namespace CRUDMToM.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly CRUDMToM.Data.AppDbContext _context;

        public DetailsModel(CRUDMToM.Data.AppDbContext context)
        {
            _context = context;
        }

        public Employee Employee { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Employee = await _context.Empployees.Include(m=>m.Employeeskills).ThenInclude(s=>s.Skills).AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);            
            if (Employee == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
