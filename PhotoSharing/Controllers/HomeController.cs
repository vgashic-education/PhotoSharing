using PhotoSharing.Models;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace PhotoSharing.Controllers
{
	public class HomeController : Controller
	{
		private PhotoSharingContext context = new PhotoSharingContext();


		public ActionResult Index()
		{
			return View("Index", context.Photos.ToList());
		}


		public ActionResult Display(int id)
		{
			Photo photo = context.Photos.Where(p => p.PhotoID == id).FirstOrDefault();

			if (photo == null)
			{
				return HttpNotFound();
			}

			return View("Display", photo);
		}


		public ActionResult Create()
		{
			Photo newPhoto = new Photo()
			{
				CreatedDate = DateTime.Now
			};

			return View("Create", newPhoto);

		}


		[HttpPost]
		public ActionResult Create(Photo photo, HttpPostedFileBase image)
		{
			photo.CreatedDate = DateTime.Now;

			if (ModelState.IsValid)
			{
				return View("Create", photo);
			}
			else
			{
				if (image != null)
				{
					photo.ImageMimeType = image.ContentType;
					photo.PhotoFile = new byte[image.ContentLength];

					image.InputStream.Read(photo.PhotoFile, 0, image.ContentLength);
				}

				context.Photos.Add(photo);
				context.SaveChanges();

				return RedirectToAction("Index");
			}
		}


		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}


		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}