using EDUTECH.Dto;
using EDUTECH.Models;
using Microsoft.AspNetCore.Hosting;
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
        private readonly IWebHostEnvironment webHostEnvironment;

        public AdminController(EDUTECHDBContext context, IWebHostEnvironment webHostEnvironment)
        {
            this._context = context;
            this.webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            var uid = HttpContext.Session.GetInt32(key);
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
        public async Task<IActionResult> AddUser(int id = 0)
        {
            var uid = HttpContext.Session.GetInt32(key);
            
            if (uid != null || uid > 0)
            {
                if (id == 0)
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
                        Universities = await _context.Universities.ToListAsync(),
                        AddEditUser = new User()

                    };
                    return View(adminViewModel);
                }
                else
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
                        Universities = await _context.Universities.ToListAsync(),
                        AddEditUser = await _context.Users.Where(x => x.Id == id).FirstOrDefaultAsync(),

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
        public async Task<IActionResult> AddUser(AdminViewModel adminViewModel)
        {
            var uid = HttpContext.Session.GetInt32(key);
            if (uid != null || uid > 0)
            {
                if (adminViewModel.AddEditUser.Id == 0)
                {
                    adminViewModel.AddEditUser.CreatedDate = DateTime.Now;
                    adminViewModel.AddEditUser.CreatedBy = uid.ToString();
                    await _context.Users.AddAsync(adminViewModel.AddEditUser);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Users");
                }
                else
                {
                    adminViewModel.AddEditUser.UpdatedDate = DateTime.Now;
                    adminViewModel.AddEditUser.UpdatedBy = uid.ToString();

                     _context.Users.Update(adminViewModel.AddEditUser);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Users");
                }
            }
            else
            {
                return RedirectToAction("Login", "User");
            }

        }
        public async Task<IActionResult> DeleteUser(int id)
        {
            var uid = HttpContext.Session.GetInt32(key);
            if (uid != null || uid > 0)
            {
                var user = _context.Users.Where(u => u.Id== id).FirstOrDefault();
                if (user != null)
                {
                     _context.Users.Remove(user);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction("Users");
            }
            else
            {
                return RedirectToAction("Login", "User");
            }

        }
        public async Task<IActionResult> Universities()
        {
            var uid = HttpContext.Session.GetInt32(key);
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
                return View(adminViewModel);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }

        }
        public async Task<IActionResult> AddUniversity(int id = 0)
        {
            var uid = HttpContext.Session.GetInt32(key);
            if (uid != null || uid > 0)
            {
                if (id == 0)
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
                        Universities = await _context.Universities.ToListAsync(),
                        AddEditUser = new User(),
                        AddEditUniversity=new University(),

                    };
                    return View(adminViewModel);
                }
                else
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
                        Universities = await _context.Universities.ToListAsync(),
                        AddEditUser = await _context.Users.Where(x => x.Id == id).FirstOrDefaultAsync(),
                        AddEditUniversity = await _context.Universities.Where(x => x.Id == id).FirstOrDefaultAsync(),

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
        public async Task<IActionResult> AddUniversity(AdminViewModel adminViewModel)
        {
            var uid = HttpContext.Session.GetInt32(key);
            if (uid != null || uid > 0)
            {
                if (adminViewModel.AddEditUniversity.Id == 0)
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

                        adminViewModel.AddEditUniversity.Logo = ImagePath;

                    }
                    else
                    {
                        adminViewModel.AddEditUniversity.Logo = ImagePath;

                    }


                    await _context.Universities.AddAsync(adminViewModel.AddEditUniversity);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Universities");
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

                        adminViewModel.AddEditUniversity.Logo = ImagePath;

                    }
                    else
                    {
                       var university = await _context.Universities.Where(x => x.Id == adminViewModel.AddEditUniversity.Id).AsNoTracking().FirstOrDefaultAsync();
                        adminViewModel.AddEditUniversity.Logo = university?.Logo;
                    }
                    _context.Universities.Update(adminViewModel.AddEditUniversity);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Universities");
                }
            }
            else
            {
                return RedirectToAction("Login", "User");
            }

        }
        public async Task<IActionResult> DeleteUniversity(int id)
        {
            var uid = HttpContext.Session.GetInt32(key);
            if (uid != null || uid > 0)
            {
                var user = _context.Universities.Where(u => u.Id == id).FirstOrDefault();
                if (user != null)
                {
                    _context.Universities.Remove(user);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction("Universities");
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
