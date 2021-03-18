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
    public class IndexModel : PageModel
    {
        private readonly CRUDMToM.Data.AppDbContext _context;

        public IndexModel(CRUDMToM.Data.AppDbContext context)
        {
            _context = context;
        }

        public IList<Employee> Employee { get;set; }

        public async Task OnGetAsync()
        {
            Employee = await _context.Empployees.ToListAsync();
        }
    }
}
