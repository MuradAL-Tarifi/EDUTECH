using EDUTECH.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EDUTECH.Controllers
{
	public class UniversitiesController : Controller
	{
		private readonly EDUTECHDBContext context;

		public UniversitiesController(EDUTECHDBContext context)
		{
			this.context = context;
		}
		public IActionResult Index(string search)
		{
            var universities = context.Universities.Where(x => (string.IsNullOrEmpty(search) || x.Name.Contains(search))).Include(x => x.Events).Include(x => x.Requirements).Include(x => x.Books).Include(x => x.Colleges)
.ThenInclude(x => x.Sections).ThenInclude(x => x.Specialties).ThenInclude(x => x.ScientificDegree)
.ToList();
            return View(universities);
        }
		public IActionResult UniversityDetails(int id)
		{
			var universities= context.Universities.Where(x => x.Id == id).Include(x=>x.Events).Include(x=>x.Requirements).Include(x=>x.Books).Include(x=>x.Colleges)
				.ThenInclude(x=>x.Sections).ThenInclude(x=>x.Specialties).ThenInclude(x=>x.ScientificDegree)
				.FirstOrDefault();
			return View(universities);
		}
        public IActionResult EventDetails(int id)
        {
            var universities = context.Events.Where(x => x.Id == id).Include(x => x.University)
				.FirstOrDefault();
            return View(universities);
        }
    }
}
