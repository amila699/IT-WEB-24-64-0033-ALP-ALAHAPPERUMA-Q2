using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Q2.Data;
using Q2.Model;

namespace Q2.Pages.student
{
    public class DeleteModel : PageModel
    {
        private readonly Q2.Data.Q2Context _context;

        public DeleteModel(Q2.Data.Q2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Student Student { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Student
                .Include(x=>x.course)
                .FirstOrDefaultAsync(m => m.StudentID == id);

            if (course == null)
            {
                return NotFound();
            }
            else
            {
                Student = course;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Student.FindAsync(id);
            if (course != null)
            {
                Student = course;
                _context.Student.Remove(course);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}