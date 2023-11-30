using EDUTECH.Dto;
using EDUTECH.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EDUTECH.Controllers
{
    public class AdminController : Controller
    {
        const string key = "UserId";
        private readonly EDUTECHDBContext _context;

        public AdminController(EDUTECHDBContext context)
        {
            this._context = context;
        }
        public async Task<IActionResult> Index()
        {
            var uid = HttpContext.Session.GetInt32(key);
            uid = 1;
            if (uid != null || uid>0)
            {
                AdminViewModel adminViewModel = new AdminViewModel
                {
                    User = await _context.Users.Where(x => x.Id == uid).FirstOrDefaultAsync(),
                    Users = await _context.Users.ToListAsync(),
                    Books = await _context.Books.ToListAsync(),
                    Colleges = await _context.Colleges.ToListAsync(),
                    Events = await _context.Events.ToListAsync(),
                    Messages = await _context.Messages.ToListAsync(),
                    Sections = await _context.Sections.ToListAsync(),
                    Specialties = await _context.Specialties.ToListAsync(),
                    Universities = await _context.Universities.ToListAsync()

                };
                return View(adminViewModel);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }

        }
        public async Task<IActionResult> Events()
        {
            var uid = HttpContext.Session.GetInt32(key);
            uid = 1;
            if (uid != null || uid > 0)
            {
                AdminViewModel adminViewModel = new AdminViewModel
                {
                    User = await _context.Users.Where(x => x.Id == uid).FirstOrDefaultAsync(),
                    Users = await _context.Users.ToListAsync(),
                    Books = await _context.Books.ToListAsync(),
                    Colleges = await _context.Colleges.ToListAsync(),
                    Events = await _context.Events.ToListAsync(),
                    Messages = await _context.Messages.ToListAsync(),
                    Sections = await _context.Sections.ToListAsync(),
                    Specialties = await _context.Specialties.ToListAsync(),
                    Universities = await _context.Universities.ToListAsync()

                };
                adminViewModel.EventsDto = new List<EventsDto>();
                foreach (var item in adminViewModel.Events)
                {
                    adminViewModel.EventsDto.Add(new EventsDto
                    {
                        title=item.Name,
                       start= item.StartDate.Value.ToString(),
                       end= item.EndDate.Value.ToString(),
                       className= "bg-cyan"

                    });
                }
                return View(adminViewModel);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }

        }
        public async Task<IActionResult> Users()
        {
            var uid = HttpContext.Session.GetInt32(key);
            uid = 1;
            if (uid != null || uid > 0)
            {
                AdminViewModel adminViewModel = new AdminViewModel
                {
                    User = await _context.Users.Where(x => x.Id == uid).FirstOrDefaultAsync(),
                    Users = await _context.Users.ToListAsync(),
                    Books = await _context.Books.ToListAsync(),
                    Colleges = await _context.Colleges.ToListAsync(),
                    Events = await _context.Events.ToListAsync(),
                    Messages = await _context.Messages.ToListAsync(),
                    Sections = await _context.Sections.ToListAsync(),
                    Specialties = await _context.Specialties.ToListAsync(),
                    Universities = await _context.Universities.ToListAsync()

                };
                adminViewModel.EventsDto = new List<EventsDto>();
                foreach (var item in adminViewModel.Events)
                {
                    adminViewModel.EventsDto.Add(new EventsDto
                    {
                        title = item.Name,
                        start = item.StartDate.Value.ToString(),
                        end = item.EndDate.Value.ToString(),
                        className = "bg-cyan"

                    });
                }
                return View(adminViewModel);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }

        }
        public async Task<IActionResult> Universities()
        {
            var uid = HttpContext.Session.GetInt32(key);
            uid = 1;
            if (uid != null || uid > 0)
            {
                AdminViewModel adminViewModel = new AdminViewModel
                {
                    User = await _context.Users.Where(x => x.Id == uid).FirstOrDefaultAsync(),
                    Users = await _context.Users.ToListAsync(),
                    Books = await _context.Books.ToListAsync(),
                    Colleges = await _context.Colleges.ToListAsync(),
                    Events = await _context.Events.ToListAsync(),
                    Messages = await _context.Messages.ToListAsync(),
                    Sections = await _context.Sections.ToListAsync(),
                    Specialties = await _context.Specialties.ToListAsync(),
                    Universities = await _context.Universities.Include(x=>x.User).ToListAsync()

                };
                adminViewModel.EventsDto = new List<EventsDto>();
                foreach (var item in adminViewModel.Events)
                {
                    adminViewModel.EventsDto.Add(new EventsDto
                    {
                        title = item.Name,
                        start = item.StartDate.Value.ToString(),
                        end = item.EndDate.Value.ToString(),
                        className = "bg-cyan"

                    });
                }
                return View(adminViewModel);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }

        }
        public async Task<IActionResult> Books()
        {
            var uid = HttpContext.Session.GetInt32(key);
            uid = 1;
            if (uid != null || uid > 0)
            {
                AdminViewModel adminViewModel = new AdminViewModel
                {
                    User = await _context.Users.Where(x => x.Id == uid).FirstOrDefaultAsync(),
                    Users = await _context.Users.ToListAsync(),
                    Books = await _context.Books.Include(x=>x.University).ToListAsync(),
                    Colleges = await _context.Colleges.ToListAsync(),
                    Events = await _context.Events.ToListAsync(),
                    Messages = await _context.Messages.ToListAsync(),
                    Sections = await _context.Sections.ToListAsync(),
                    Specialties = await _context.Specialties.ToListAsync(),
                    Universities = await _context.Universities.ToListAsync()

                };
                adminViewModel.EventsDto = new List<EventsDto>();
                foreach (var item in adminViewModel.Events)
                {
                    adminViewModel.EventsDto.Add(new EventsDto
                    {
                        title = item.Name,
                        start = item.StartDate.Value.ToString(),
                        end = item.EndDate.Value.ToString(),
                        className = "bg-cyan"

                    });
                }
                return View(adminViewModel);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }

        }
        public async Task<IActionResult> Colleges()
        {
            var uid = HttpContext.Session.GetInt32(key);
            uid = 1;
            if (uid != null || uid > 0)
            {
                AdminViewModel adminViewModel = new AdminViewModel
                {
                    User = await _context.Users.Where(x => x.Id == uid).FirstOrDefaultAsync(),
                    Users = await _context.Users.ToListAsync(),
                    Books = await _context.Books.ToListAsync(),
                    Colleges = await _context.Colleges.Include(x=>x.University).ToListAsync(),
                    Events = await _context.Events.ToListAsync(),
                    Messages = await _context.Messages.ToListAsync(),
                    Sections = await _context.Sections.ToListAsync(),
                    Specialties = await _context.Specialties.ToListAsync(),
                    Universities = await _context.Universities.ToListAsync()

                };
                adminViewModel.EventsDto = new List<EventsDto>();
                foreach (var item in adminViewModel.Events)
                {
                    adminViewModel.EventsDto.Add(new EventsDto
                    {
                        title = item.Name,
                        start = item.StartDate.Value.ToString(),
                        end = item.EndDate.Value.ToString(),
                        className = "bg-cyan"

                    });
                }
                return View(adminViewModel);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }

        }
        public async Task<IActionResult> Sections()
        {
            var uid = HttpContext.Session.GetInt32(key);
            uid = 1;
            if (uid != null || uid > 0)
            {
                AdminViewModel adminViewModel = new AdminViewModel
                {
                    User = await _context.Users.Where(x => x.Id == uid).FirstOrDefaultAsync(),
                    Users = await _context.Users.ToListAsync(),
                    Books = await _context.Books.ToListAsync(),
                    Colleges = await _context.Colleges.ToListAsync(),
                    Events = await _context.Events.ToListAsync(),
                    Messages = await _context.Messages.ToListAsync(),
                    Sections = await _context.Sections.Include(x=>x.College).ThenInclude(x=>x.University).ToListAsync(),
                    Specialties = await _context.Specialties.ToListAsync(),
                    Universities = await _context.Universities.ToListAsync()

                };
                adminViewModel.EventsDto = new List<EventsDto>();
                foreach (var item in adminViewModel.Events)
                {
                    adminViewModel.EventsDto.Add(new EventsDto
                    {
                        title = item.Name,
                        start = item.StartDate.Value.ToString(),
                        end = item.EndDate.Value.ToString(),
                        className = "bg-cyan"

                    });
                }
                return View(adminViewModel);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }

        }
        public async Task<IActionResult> Specialties()
        {
            var uid = HttpContext.Session.GetInt32(key);
            uid = 1;
            if (uid != null || uid > 0)
            {
                AdminViewModel adminViewModel = new AdminViewModel
                {
                    User = await _context.Users.Where(x => x.Id == uid).FirstOrDefaultAsync(),
                    Users = await _context.Users.ToListAsync(),
                    Books = await _context.Books.ToListAsync(),
                    Colleges = await _context.Colleges.ToListAsync(),
                    Events = await _context.Events.ToListAsync(),
                    Messages = await _context.Messages.ToListAsync(),
                    Sections = await _context.Sections.ToListAsync(),
                    Specialties = await _context.Specialties.Include(x=>x.ScientificDegree)
                    .Include(x=>x.Section).ThenInclude(x=>x.College).ThenInclude(x=>x.University).ToListAsync(),
                    Universities = await _context.Universities.ToListAsync()

                };
                adminViewModel.EventsDto = new List<EventsDto>();
                foreach (var item in adminViewModel.Events)
                {
                    adminViewModel.EventsDto.Add(new EventsDto
                    {
                        title = item.Name,
                        start = item.StartDate.Value.ToString(),
                        end = item.EndDate.Value.ToString(),
                        className = "bg-cyan"

                    });
                }
                return View(adminViewModel);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }

        }
        public async Task<IActionResult> Messages()
        {
            var uid = HttpContext.Session.GetInt32(key);
            uid = 1;
            if (uid != null || uid > 0)
            {
                AdminViewModel adminViewModel = new AdminViewModel
                {
                    User = await _context.Users.Where(x => x.Id == uid).FirstOrDefaultAsync(),
                    Users = await _context.Users.ToListAsync(),
                    Books = await _context.Books.ToListAsync(),
                    Colleges = await _context.Colleges.ToListAsync(),
                    Events = await _context.Events.ToListAsync(),
                    Messages = await _context.Messages.ToListAsync(),
                    Sections = await _context.Sections.ToListAsync(),
                    Specialties = await _context.Specialties.ToListAsync(),
                    Universities = await _context.Universities.ToListAsync()

                };
                adminViewModel.EventsDto = new List<EventsDto>();
                foreach (var item in adminViewModel.Events)
                {
                    adminViewModel.EventsDto.Add(new EventsDto
                    {
                        title = item.Name,
                        start = item.StartDate.Value.ToString(),
                        end = item.EndDate.Value.ToString(),
                        className = "bg-cyan"

                    });
                }
                return View(adminViewModel);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }

        }

    }
}
