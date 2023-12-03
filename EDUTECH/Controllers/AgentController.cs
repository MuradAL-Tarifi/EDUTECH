using EDUTECH.Dto;
using EDUTECH.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EDUTECH.Controllers
{
    public class AgentController : Controller
    {
        const string key = "UserId";
        private readonly EDUTECHDBContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public AgentController(EDUTECHDBContext context, IWebHostEnvironment webHostEnvironment)
        {
            this._context = context;
            this.webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            var uid = HttpContext.Session.GetInt32(key);

            if (uid != null || uid > 0)
            {
                University university = await _context.Universities.Where(x => x.UserId == uid).FirstOrDefaultAsync();
                AgentViewModel agentViewModel = new AgentViewModel
                {
                    User = await _context.Users.Where(x => x.Id == uid).FirstOrDefaultAsync(),
                    Books = await _context.Books.Where(x => x.UniversityId == university.Id).ToListAsync(),
                    Colleges = await _context.Colleges.Where(x => x.UniversityId == university.Id).ToListAsync(),
                    Events = await _context.Events.Where(x => x.UniversityId == university.Id).ToListAsync(),
                    Sections = await _context.Sections.Include(x => x.College).Where(x => x.College.UniversityId == university.Id).ToListAsync(),
                    Specialties = await _context.Specialties.Include(x => x.Section).ThenInclude(x => x.College).Where(x => x.Section.College.UniversityId == university.Id).ToListAsync(),
                    University = await _context.Universities.Where(x => x.UserId == uid).FirstOrDefaultAsync()
                };

                return View(agentViewModel);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }

        }
        public async Task<IActionResult> UniversityProfile()
        {
            var uid = HttpContext.Session.GetInt32(key);

            if (uid != null || uid > 0)
            {
                University university = await _context.Universities.Where(x => x.UserId == uid).FirstOrDefaultAsync();
                AgentViewModel agentViewModel = new AgentViewModel
                {
                    User = await _context.Users.Where(x => x.Id == uid).FirstOrDefaultAsync(),
                    Books = await _context.Books.Where(x => x.UniversityId == university.Id).ToListAsync(),
                    Colleges = await _context.Colleges.Where(x => x.UniversityId == university.Id).ToListAsync(),
                    Events = await _context.Events.Where(x => x.UniversityId == university.Id).ToListAsync(),
                    Sections = await _context.Sections.Include(x => x.College).Where(x => x.College.UniversityId == university.Id).ToListAsync(),
                    Specialties = await _context.Specialties.Include(x => x.Section).ThenInclude(x => x.College).Where(x => x.Section.College.UniversityId == university.Id).ToListAsync(),
                    University = await _context.Universities.Where(x => x.UserId == uid).FirstOrDefaultAsync()
                };

                return View(agentViewModel);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }

        }
        [HttpPost]
        public async Task<IActionResult> UniversityProfile(AgentViewModel adminViewModel)
        {
            var uid = HttpContext.Session.GetInt32(key);

            if (uid != null || uid > 0)
            {
                var files = HttpContext.Request.Form.Files;
                string ImagePath = null;

                if (files.Count > 0)
                {
                    string webRootPath = webHostEnvironment.WebRootPath;

                    string ImageName = DateTime.Now.ToFileTime().ToString() + Path.GetExtension(files[0].FileName);

                    FileStream fileStream = new FileStream(Path.Combine(webRootPath, "Images", ImageName), FileMode.Create);

                    files[0].CopyTo(fileStream);

                    ImagePath = @"\Images\" + ImageName;

                    adminViewModel.University.Logo = ImagePath;

                }
                else
                {
                    var university = await _context.Universities.Where(x => x.Id == adminViewModel.University.Id).AsNoTracking().FirstOrDefaultAsync();
                    adminViewModel.University.Logo = university?.Logo;
                    adminViewModel.University.UserId = university.UserId;
                }
                _context.Universities.Update(adminViewModel.University);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            else
            {
                return RedirectToAction("Login", "User");
            }

        }
        public async Task<IActionResult> Books()
        {
            var uid = HttpContext.Session.GetInt32(key);

            if (uid != null || uid > 0)
            {
                University university = await _context.Universities.Where(x => x.UserId == uid).FirstOrDefaultAsync();

                AgentViewModel adminViewModel = new AgentViewModel
                {
                    User = await _context.Users.Where(x => x.Id == uid).FirstOrDefaultAsync(),
                    Books = await _context.Books.Include(x => x.University).Where(x => x.UniversityId == university.Id).ToListAsync(),
                    Colleges = await _context.Colleges.Where(x => x.UniversityId == university.Id).ToListAsync(),
                    Events = await _context.Events.Where(x => x.UniversityId == university.Id).ToListAsync(),
                    Sections = await _context.Sections.Include(x => x.College).Where(x => x.College.UniversityId == university.Id).ToListAsync(),
                    Specialties = await _context.Specialties.Include(x => x.Section).ThenInclude(x => x.College).Where(x => x.Section.College.UniversityId == university.Id).ToListAsync(),
                    University = await _context.Universities.Where(x => x.UserId == uid).FirstOrDefaultAsync()

                };
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

            if (uid != null || uid > 0)
            {
                University university = await _context.Universities.Where(x => x.UserId == uid).FirstOrDefaultAsync();

                AgentViewModel adminViewModel = new AgentViewModel
                {
                    User = await _context.Users.Where(x => x.Id == uid).FirstOrDefaultAsync(),
                    Books = await _context.Books.Include(x => x.University).Where(x => x.UniversityId == university.Id).ToListAsync(),
                    Colleges = await _context.Colleges.Include(x => x.University).Where(x => x.UniversityId == university.Id).ToListAsync(),
                    Events = await _context.Events.Where(x => x.UniversityId == university.Id).ToListAsync(),
                    Sections = await _context.Sections.Include(x => x.College).Where(x => x.College.UniversityId == university.Id).ToListAsync(),
                    Specialties = await _context.Specialties.Include(x => x.Section).ThenInclude(x => x.College).Where(x => x.Section.College.UniversityId == university.Id).ToListAsync(),
                    University = await _context.Universities.Where(x => x.UserId == uid).FirstOrDefaultAsync()

                };
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

            if (uid != null || uid > 0)
            {
                University university = await _context.Universities.Where(x => x.UserId == uid).FirstOrDefaultAsync();

                AgentViewModel adminViewModel = new AgentViewModel
                {
                    User = await _context.Users.Where(x => x.Id == uid).FirstOrDefaultAsync(),
                    Books = await _context.Books.Include(x => x.University).Where(x => x.UniversityId == university.Id).ToListAsync(),
                    Colleges = await _context.Colleges.Include(x => x.University).Where(x => x.UniversityId == university.Id).ToListAsync(),
                    Events = await _context.Events.Where(x => x.UniversityId == university.Id).ToListAsync(),
                    Sections = await _context.Sections.Include(x => x.College).ThenInclude(x => x.University).Where(x => x.College.UniversityId == university.Id).ToListAsync(),
                    Specialties = await _context.Specialties.Include(x => x.Section).ThenInclude(x => x.College).ThenInclude(x => x.University).Where(x => x.Section.College.UniversityId == university.Id).ToListAsync(),
                    University = await _context.Universities.Where(x => x.UserId == uid).FirstOrDefaultAsync()

                };
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

            if (uid != null || uid > 0)
            {
                University university = await _context.Universities.Where(x => x.UserId == uid).FirstOrDefaultAsync();

                AgentViewModel adminViewModel = new AgentViewModel
                {
                    User = await _context.Users.Where(x => x.Id == uid).FirstOrDefaultAsync(),
                    Books = await _context.Books.Include(x => x.University).Where(x => x.UniversityId == university.Id).ToListAsync(),
                    Colleges = await _context.Colleges.Where(x => x.UniversityId == university.Id).ToListAsync(),
                    Events = await _context.Events.Where(x => x.UniversityId == university.Id).ToListAsync(),
                    Sections = await _context.Sections.Include(x => x.College).Where(x => x.College.UniversityId == university.Id).ToListAsync(),
                    Specialties = await _context.Specialties.Include(x => x.Section).ThenInclude(x => x.College).Include(x => x.ScientificDegree).Where(x => x.Section.College.UniversityId == university.Id).ToListAsync(),
                    University = await _context.Universities.Where(x => x.UserId == uid).FirstOrDefaultAsync()

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

            if (uid != null || uid > 0)
            {
                University university = await _context.Universities.Where(x => x.UserId == uid).FirstOrDefaultAsync();

                AgentViewModel adminViewModel = new AgentViewModel
                {
                    User = await _context.Users.Where(x => x.Id == uid).FirstOrDefaultAsync(),
                    Books = await _context.Books.Include(x => x.University).Where(x => x.UniversityId == university.Id).ToListAsync(),
                    Colleges = await _context.Colleges.Where(x => x.UniversityId == university.Id).ToListAsync(),
                    Events = await _context.Events.Where(x => x.UniversityId == university.Id).ToListAsync(),
                    Sections = await _context.Sections.Include(x => x.College).Where(x => x.College.UniversityId == university.Id).ToListAsync(),
                    Specialties = await _context.Specialties.Include(x => x.Section).ThenInclude(x => x.College).Where(x => x.Section.College.UniversityId == university.Id).ToListAsync(),
                    University = await _context.Universities.Where(x => x.UserId == uid).FirstOrDefaultAsync()

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
        public async Task<IActionResult> AddEvent(int id = 0)
        {
            var uid = HttpContext.Session.GetInt32(key);

            if (uid != null || uid > 0)
            {
                if (id == 0)
                {
                    University university = await _context.Universities.Where(x => x.UserId == uid).FirstOrDefaultAsync();

                    AgentViewModel adminViewModel = new AgentViewModel
                    {
                        User = await _context.Users.Where(x => x.Id == uid).FirstOrDefaultAsync(),
                        Books = await _context.Books.Include(x => x.University).Where(x => x.UniversityId == university.Id).ToListAsync(),
                        Colleges = await _context.Colleges.Where(x => x.UniversityId == university.Id).ToListAsync(),
                        Events = await _context.Events.Where(x => x.UniversityId == university.Id).ToListAsync(),
                        Sections = await _context.Sections.Include(x => x.College).Where(x => x.College.UniversityId == university.Id).ToListAsync(),
                        Specialties = await _context.Specialties.Include(x => x.Section).ThenInclude(x => x.College).Where(x => x.Section.College.UniversityId == university.Id).ToListAsync(),
                        University = await _context.Universities.Where(x => x.UserId == uid).FirstOrDefaultAsync(),
                        AddEditEvent = new Event()

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
                    University university = await _context.Universities.Where(x => x.UserId == uid).FirstOrDefaultAsync();

                    AgentViewModel adminViewModel = new AgentViewModel
                    {
                        User = await _context.Users.Where(x => x.Id == uid).FirstOrDefaultAsync(),
                        Books = await _context.Books.Include(x => x.University).Where(x => x.UniversityId == university.Id).ToListAsync(),
                        Colleges = await _context.Colleges.Where(x => x.UniversityId == university.Id).ToListAsync(),
                        Events = await _context.Events.Where(x => x.UniversityId == university.Id).ToListAsync(),
                        Sections = await _context.Sections.Include(x => x.College).Where(x => x.College.UniversityId == university.Id).ToListAsync(),
                        Specialties = await _context.Specialties.Include(x => x.Section).ThenInclude(x => x.College).Where(x => x.Section.College.UniversityId == university.Id).ToListAsync(),
                        University = await _context.Universities.Where(x => x.UserId == uid).FirstOrDefaultAsync(),
                        AddEditEvent = await _context.Events.Where(x => x.Id == id).FirstOrDefaultAsync()

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

            }
            else
            {
                return RedirectToAction("Login", "User");
            }

        }
        [HttpPost]
        public async Task<IActionResult> AddEvent(AgentViewModel adminViewModel)
        {
            var uid = HttpContext.Session.GetInt32(key);

            if (uid != null || uid > 0)
            {
                University university = await _context.Universities.Where(x => x.UserId == uid).FirstOrDefaultAsync();
                adminViewModel.AddEditEvent.UniversityId = university.Id;
                if (adminViewModel.AddEditEvent.Id == 0)
                {
                    var files = HttpContext.Request.Form.Files;
                    string ImagePath = null;

                    if (files.Count > 0)
                    {
                        string webRootPath = webHostEnvironment.WebRootPath;

                        string ImageName = DateTime.Now.ToFileTime().ToString() + Path.GetExtension(files[0].FileName);

                        FileStream fileStream = new FileStream(Path.Combine(webRootPath, "Images", ImageName), FileMode.Create);

                        files[0].CopyTo(fileStream);

                        ImagePath = @"\Images\" + ImageName;

                        adminViewModel.AddEditEvent.Photo = ImagePath;

                    }
                    else
                    {
                        adminViewModel.AddEditEvent.Photo = ImagePath;

                    }

                    await _context.Events.AddAsync(adminViewModel.AddEditEvent);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Events");
                }
                else
                {
                    var files = HttpContext.Request.Form.Files;
                    string ImagePath = null;

                    if (files.Count > 0)
                    {
                        string webRootPath = webHostEnvironment.WebRootPath;

                        string ImageName = DateTime.Now.ToFileTime().ToString() + Path.GetExtension(files[0].FileName);

                        FileStream fileStream = new FileStream(Path.Combine(webRootPath, "Images", ImageName), FileMode.Create);

                        files[0].CopyTo(fileStream);

                        ImagePath = @"\Images\" + ImageName;

                        adminViewModel.AddEditEvent.Photo = ImagePath;

                    }
                    else
                    {
                        var eventw = await _context.Events.Where(x => x.Id == adminViewModel.AddEditEvent.Id).AsNoTracking().FirstOrDefaultAsync();
                        adminViewModel.AddEditEvent.Photo = eventw?.Photo;
                    }

                    _context.Events.Update(adminViewModel.AddEditEvent);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Events");
                }
            }
            else
            {
                return RedirectToAction("Login", "User");
            }

        }
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var uid = HttpContext.Session.GetInt32(key);

            if (uid != null || uid > 0)
            {
                var user = _context.Events.Where(u => u.Id == id).FirstOrDefault();
                if (user != null)
                {
                    _context.Events.Remove(user);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction("Events");
            }
            else
            {
                return RedirectToAction("Login", "User");
            }

        }
        public async Task<IActionResult> AddBook(int id = 0)
        {
            var uid = HttpContext.Session.GetInt32(key);

            if (uid != null || uid > 0)
            {
                if (id == 0)
                {
                    University university = await _context.Universities.Where(x => x.UserId == uid).FirstOrDefaultAsync();

                    AgentViewModel adminViewModel = new AgentViewModel
                    {
                        User = await _context.Users.Where(x => x.Id == uid).FirstOrDefaultAsync(),
                        Books = await _context.Books.Include(x => x.University).Where(x => x.UniversityId == university.Id).ToListAsync(),
                        Colleges = await _context.Colleges.Where(x => x.UniversityId == university.Id).ToListAsync(),
                        Events = await _context.Events.Where(x => x.UniversityId == university.Id).ToListAsync(),
                        Sections = await _context.Sections.Include(x => x.College).Where(x => x.College.UniversityId == university.Id).ToListAsync(),
                        Specialties = await _context.Specialties.Include(x => x.Section).ThenInclude(x => x.College).Where(x => x.Section.College.UniversityId == university.Id).ToListAsync(),
                        University = await _context.Universities.Where(x => x.UserId == uid).FirstOrDefaultAsync(),
                        AddEditEvent = new Event(),
                        AddEditBook = new Book(),

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
                    University university = await _context.Universities.Where(x => x.UserId == uid).FirstOrDefaultAsync();

                    AgentViewModel adminViewModel = new AgentViewModel
                    {
                        User = await _context.Users.Where(x => x.Id == uid).FirstOrDefaultAsync(),
                        Books = await _context.Books.Include(x => x.University).Where(x => x.UniversityId == university.Id).ToListAsync(),
                        Colleges = await _context.Colleges.Where(x => x.UniversityId == university.Id).ToListAsync(),
                        Events = await _context.Events.Where(x => x.UniversityId == university.Id).ToListAsync(),
                        Sections = await _context.Sections.Include(x => x.College).Where(x => x.College.UniversityId == university.Id).ToListAsync(),
                        Specialties = await _context.Specialties.Include(x => x.Section).ThenInclude(x => x.College).Where(x => x.Section.College.UniversityId == university.Id).ToListAsync(),
                        University = await _context.Universities.Where(x => x.UserId == uid).FirstOrDefaultAsync(),
                        AddEditEvent = await _context.Events.Where(x => x.Id == id).FirstOrDefaultAsync(),
                        AddEditBook = await _context.Books.Where(x => x.Id == id).FirstOrDefaultAsync()

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

            }
            else
            {
                return RedirectToAction("Login", "User");
            }

        }
        [HttpPost]
        public async Task<IActionResult> AddBook(AgentViewModel adminViewModel)
        {
            var uid = HttpContext.Session.GetInt32(key);

            if (uid != null || uid > 0)
            {
                University university = await _context.Universities.Where(x => x.UserId == uid).FirstOrDefaultAsync();
                adminViewModel.AddEditBook.UniversityId = university.Id;

                if (adminViewModel.AddEditBook.Id == 0)
                {
                    await _context.Books.AddAsync(adminViewModel.AddEditBook);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Books");
                }
                else
                {

                    _context.Books.Update(adminViewModel.AddEditBook);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Books");
                }
            }
            else
            {
                return RedirectToAction("Login", "User");
            }

        }
        public async Task<IActionResult> DeleteBook(int id)
        {
            var uid = HttpContext.Session.GetInt32(key);

            if (uid != null || uid > 0)
            {
                var user = _context.Books.Where(u => u.Id == id).FirstOrDefault();
                if (user != null)
                {
                    _context.Books.Remove(user);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction("Books");
            }
            else
            {
                return RedirectToAction("Login", "User");
            }

        }
        public async Task<IActionResult> AddCollege(int id = 0)
        {
            var uid = HttpContext.Session.GetInt32(key);

            if (uid != null || uid > 0)
            {
                if (id == 0)
                {
                    University university = await _context.Universities.Where(x => x.UserId == uid).FirstOrDefaultAsync();

                    AgentViewModel adminViewModel = new AgentViewModel
                    {
                        User = await _context.Users.Where(x => x.Id == uid).FirstOrDefaultAsync(),
                        Books = await _context.Books.Include(x => x.University).Where(x => x.UniversityId == university.Id).ToListAsync(),
                        Colleges = await _context.Colleges.Where(x => x.UniversityId == university.Id).ToListAsync(),
                        Events = await _context.Events.Where(x => x.UniversityId == university.Id).ToListAsync(),
                        Sections = await _context.Sections.Include(x => x.College).Where(x => x.College.UniversityId == university.Id).ToListAsync(),
                        Specialties = await _context.Specialties.Include(x => x.Section).ThenInclude(x => x.College).Where(x => x.Section.College.UniversityId == university.Id).ToListAsync(),
                        University = await _context.Universities.Where(x => x.UserId == uid).FirstOrDefaultAsync(),
                        AddEditEvent = new Event(),
                        AddEditBook = new Book(),
                        AddEditCollege = new College(),

                    };
                    return View(adminViewModel);
                }
                else
                {
                    University university = await _context.Universities.Where(x => x.UserId == uid).FirstOrDefaultAsync();

                    AgentViewModel adminViewModel = new AgentViewModel
                    {
                        User = await _context.Users.Where(x => x.Id == uid).FirstOrDefaultAsync(),
                        Books = await _context.Books.Include(x => x.University).Where(x => x.UniversityId == university.Id).ToListAsync(),
                        Colleges = await _context.Colleges.Where(x => x.UniversityId == university.Id).ToListAsync(),
                        Events = await _context.Events.Where(x => x.UniversityId == university.Id).ToListAsync(),
                        Sections = await _context.Sections.Include(x => x.College).Where(x => x.College.UniversityId == university.Id).ToListAsync(),
                        Specialties = await _context.Specialties.Include(x => x.Section).ThenInclude(x => x.College).Where(x => x.Section.College.UniversityId == university.Id).ToListAsync(),
                        University = await _context.Universities.Where(x => x.UserId == uid).FirstOrDefaultAsync(),
                        AddEditEvent = await _context.Events.Where(x => x.Id == id).FirstOrDefaultAsync(),
                        AddEditBook = await _context.Books.Where(x => x.Id == id).FirstOrDefaultAsync(),
                        AddEditCollege = await _context.Colleges.Where(x => x.Id == id).FirstOrDefaultAsync()
                    };
                    return View(adminViewModel);
                }

            }
            else
            {
                return RedirectToAction("Login", "User");
            }

        }
        [HttpPost]
        public async Task<IActionResult> AddCollege(AgentViewModel adminViewModel)
        {
            var uid = HttpContext.Session.GetInt32(key);

            if (uid != null || uid > 0)
            {
                University university = await _context.Universities.Where(x => x.UserId == uid).FirstOrDefaultAsync();
                adminViewModel.AddEditCollege.UniversityId = university.Id;

                if (adminViewModel.AddEditCollege.Id == 0)
                {
                    await _context.Colleges.AddAsync(adminViewModel.AddEditCollege);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Colleges");
                }
                else
                {

                    _context.Colleges.Update(adminViewModel.AddEditCollege);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Colleges");
                }
            }
            else
            {
                return RedirectToAction("Login", "User");
            }

        }
        public async Task<IActionResult> DeleteCollege(int id)
        {
            var uid = HttpContext.Session.GetInt32(key);

            if (uid != null || uid > 0)
            {
                var user = _context.Colleges.Where(u => u.Id == id).FirstOrDefault();
                if (user != null)
                {
                    _context.Colleges.Remove(user);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction("Colleges");
            }
            else
            {
                return RedirectToAction("Login", "User");
            }

        }
        public async Task<IActionResult> AddSection(int id = 0)
        {
            var uid = HttpContext.Session.GetInt32(key);

            if (uid != null || uid > 0)
            {
                if (id == 0)
                {
                    University university = await _context.Universities.Where(x => x.UserId == uid).FirstOrDefaultAsync();

                    AgentViewModel adminViewModel = new AgentViewModel
                    {
                        User = await _context.Users.Where(x => x.Id == uid).FirstOrDefaultAsync(),
                        Books = await _context.Books.Include(x => x.University).Where(x => x.UniversityId == university.Id).ToListAsync(),
                        Colleges = await _context.Colleges.Where(x => x.UniversityId == university.Id).ToListAsync(),
                        Events = await _context.Events.Where(x => x.UniversityId == university.Id).ToListAsync(),
                        Sections = await _context.Sections.Include(x => x.College).Where(x => x.College.UniversityId == university.Id).ToListAsync(),
                        Specialties = await _context.Specialties.Include(x => x.Section).ThenInclude(x => x.College).Where(x => x.Section.College.UniversityId == university.Id).ToListAsync(),
                        University = await _context.Universities.Where(x => x.UserId == uid).FirstOrDefaultAsync(),
                        AddEditEvent = new Event(),
                        AddEditBook = new Book(),
                        AddEditCollege = new College(),
                        AddEditSection = new Section()
                        {
                            College = new College()
                        },

                    };
                    return View(adminViewModel);
                }
                else
                {
                    University university = await _context.Universities.Where(x => x.UserId == uid).FirstOrDefaultAsync();

                    AgentViewModel adminViewModel = new AgentViewModel
                    {
                        User = await _context.Users.Where(x => x.Id == uid).FirstOrDefaultAsync(),
                        Books = await _context.Books.Include(x => x.University).Where(x => x.UniversityId == university.Id).ToListAsync(),
                        Colleges = await _context.Colleges.Where(x => x.UniversityId == university.Id).ToListAsync(),
                        Events = await _context.Events.Where(x => x.UniversityId == university.Id).ToListAsync(),
                        Sections = await _context.Sections.Include(x => x.College).Where(x => x.College.UniversityId == university.Id).ToListAsync(),
                        Specialties = await _context.Specialties.Include(x => x.Section).ThenInclude(x => x.College).Where(x => x.Section.College.UniversityId == university.Id).ToListAsync(),
                        University = await _context.Universities.Where(x => x.UserId == uid).FirstOrDefaultAsync(),
                        AddEditEvent = await _context.Events.Where(x => x.Id == id).FirstOrDefaultAsync(),
                        AddEditBook = await _context.Books.Where(x => x.Id == id).FirstOrDefaultAsync(),
                        AddEditCollege = await _context.Colleges.Where(x => x.Id == id).FirstOrDefaultAsync(),
                        AddEditSection = await _context.Sections.Include(x => x.College).Where(x => x.Id == id).FirstOrDefaultAsync()

                    };
                    return View(adminViewModel);
                }

            }
            else
            {
                return RedirectToAction("Login", "User");
            }

        }
        [HttpPost]
        public async Task<IActionResult> AddSection(AgentViewModel adminViewModel)
        {
            var uid = HttpContext.Session.GetInt32(key);

            if (uid != null || uid > 0)
            {
                University university = await _context.Universities.Where(x => x.UserId == uid).FirstOrDefaultAsync();

                if (adminViewModel.AddEditSection.Id == 0)
                {
                    await _context.Sections.AddAsync(adminViewModel.AddEditSection);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Sections");
                }
                else
                {

                    _context.Sections.Update(adminViewModel.AddEditSection);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Sections");
                }
            }
            else
            {
                return RedirectToAction("Login", "User");
            }

        }
        public async Task<IActionResult> DeleteSection(int id)
        {
            var uid = HttpContext.Session.GetInt32(key);

            if (uid != null || uid > 0)
            {
                var user = _context.Sections.Where(u => u.Id == id).FirstOrDefault();
                if (user != null)
                {
                    _context.Sections.Remove(user);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction("Sections");
            }
            else
            {
                return RedirectToAction("Login", "User");
            }

        }
        public async Task<IActionResult> AddSpecialty(int id = 0)
        {
            var uid = HttpContext.Session.GetInt32(key);

            if (uid != null || uid > 0)
            {
                if (id == 0)
                {
                    University university = await _context.Universities.Where(x => x.UserId == uid).FirstOrDefaultAsync();

                    AgentViewModel adminViewModel = new AgentViewModel
                    {
                        User = await _context.Users.Where(x => x.Id == uid).FirstOrDefaultAsync(),
                        Books = await _context.Books.Include(x => x.University).Where(x => x.UniversityId == university.Id).ToListAsync(),
                        Colleges = await _context.Colleges.Where(x => x.UniversityId == university.Id).ToListAsync(),
                        Events = await _context.Events.Where(x => x.UniversityId == university.Id).ToListAsync(),
                        Sections = await _context.Sections.Include(x => x.College).Where(x => x.College.UniversityId == university.Id).ToListAsync(),
                        Specialties = await _context.Specialties.Include(x => x.Section).ThenInclude(x => x.College).Where(x => x.Section.College.UniversityId == university.Id).ToListAsync(),
                        University = await _context.Universities.Where(x => x.UserId == uid).FirstOrDefaultAsync(),
                        scientificDegrees = await _context.ScientificDegrees.ToListAsync(),
                        AddEditEvent = new Event(),
                        AddEditBook = new Book(),
                        AddEditCollege = new College(),
                        AddEditSection = new Section(),
                        AddEditSpecialty = new Specialty(),

                    };
                    return View(adminViewModel);
                }
                else
                {
                    University university = await _context.Universities.Where(x => x.UserId == uid).FirstOrDefaultAsync();

                    AgentViewModel adminViewModel = new AgentViewModel
                    {
                        User = await _context.Users.Where(x => x.Id == uid).FirstOrDefaultAsync(),
                        Books = await _context.Books.Include(x => x.University).Where(x => x.UniversityId == university.Id).ToListAsync(),
                        Colleges = await _context.Colleges.Where(x => x.UniversityId == university.Id).ToListAsync(),
                        Events = await _context.Events.Where(x => x.UniversityId == university.Id).ToListAsync(),
                        Sections = await _context.Sections.Include(x => x.College).Where(x => x.College.UniversityId == university.Id).ToListAsync(),
                        Specialties = await _context.Specialties.Include(x => x.Section).ThenInclude(x => x.College).Include(x => x.ScientificDegree).Where(x => x.Section.College.UniversityId == university.Id).ToListAsync(),
                        University = await _context.Universities.Where(x => x.UserId == uid).FirstOrDefaultAsync(),
                        scientificDegrees = await _context.ScientificDegrees.ToListAsync(),
                        AddEditEvent = await _context.Events.Where(x => x.Id == id).FirstOrDefaultAsync(),
                        AddEditBook = await _context.Books.Where(x => x.Id == id).FirstOrDefaultAsync(),
                        AddEditCollege = await _context.Colleges.Where(x => x.Id == id).FirstOrDefaultAsync(),
                        AddEditSection = await _context.Sections.Include(x => x.College).Where(x => x.Id == id).FirstOrDefaultAsync(),
                        AddEditSpecialty = await _context.Specialties.Include(x => x.Section).ThenInclude(x => x.College).Include(x => x.ScientificDegree).Where(x => x.Id == id).FirstOrDefaultAsync()


                    };
                    return View(adminViewModel);
                }

            }
            else
            {
                return RedirectToAction("Login", "User");
            }

        }
        [HttpPost]
        public async Task<IActionResult> AddSpecialty(AgentViewModel adminViewModel)
        {
            var uid = HttpContext.Session.GetInt32(key);

            if (uid != null || uid > 0)
            {
                University university = await _context.Universities.Where(x => x.UserId == uid).FirstOrDefaultAsync();

                if (adminViewModel.AddEditSpecialty.Id == 0)
                {
                    await _context.Specialties.AddAsync(adminViewModel.AddEditSpecialty);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Specialties");
                }
                else
                {

                    _context.Specialties.Update(adminViewModel.AddEditSpecialty);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Specialties");
                }
            }
            else
            {
                return RedirectToAction("Login", "User");
            }

        }
        public async Task<IActionResult> DeleteSpecialty(int id)
        {
            var uid = HttpContext.Session.GetInt32(key);

            if (uid != null || uid > 0)
            {
                var user = _context.Specialties.Where(u => u.Id == id).FirstOrDefault();
                if (user != null)
                {
                    _context.Specialties.Remove(user);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction("Specialties");
            }
            else
            {
                return RedirectToAction("Login", "User");
            }

        }


    }
}
