using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using FoodLoc.Helpers;
using FoodLoc.Models;
using FoodLocWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FoodLocWeb.Controllers
{
    public class PostsController : Controller
    {

        private readonly ApplicationDbContext _context;
        private string imagesfolder = @"\images\Posts\";
        private IHostingEnvironment _environment;
        private const int _miles = 5;

        public PostsController(ApplicationDbContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: Posts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Posts.OrderByDescending(x => x.UploadedDate).Take(16).ToListAsync());
        }
        [Route("Posts/ViewPostByLocation/{lat}/{lon}")]
        public IActionResult ViewPostByLocation(double lat, double lon)
        {
            var locHelper = new LocationHelper();
            var posts = locHelper.GetLocationinCertainMileRadius(_context, lat, lon, _miles, 16);
            return PartialView("_Posts", posts);
        }
        //public IList<PostViewModel> ViewPostByLocation(double lat, double lon)
        //{
        //    var locHelper = new LocationHelper();
        //    var posts = locHelper.GetLocationinCertainMileRadius(_context, lon, lat, _miles, 50);
        //    return posts;
        //}
        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Posts = await _context.Posts.SingleOrDefaultAsync(m => m.Id == id);
            if (Posts == null)
            {
                return NotFound();
            }

            return View(Posts);
        }

        // GET: Posts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Latitude,Longitude,PhotoTitle,UpoadedBy, RestaurantName")] Posts Posts, IFormFile ImageName)
        {
            if (ImageName == null)
            {
                return View(Posts);
            }
            if (Math.Abs(Posts.Latitude) <= 0.00 && Math.Abs(Posts.Longitude) <= 0.00)
            {
                ModelState.AddModelError("Error", "Please enable the location");
                return View(Posts);
            }
            else if (!FormFileHelpers.IsImage(ImageName))
            {
                ModelState.AddModelError("Error", "The uploaded image is not valid image");
                return View(Posts);
            }
            if (ModelState.IsValid)
            {
                if (ImageName != null && ImageName.Length > 0)
                {
                    var imagename = FormFileHelpers.Save(ImageName, _environment.WebRootPath + imagesfolder);
                    Posts.ImageName = imagename;
                    Posts.UploadedDate = System.DateTime.Now;
                }
                _context.Add(Posts);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(Posts);
        }

    }
}
