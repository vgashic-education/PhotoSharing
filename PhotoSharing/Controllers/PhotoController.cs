using PhotoSharing.Models;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotoSharing.Controllers
{
	[ValueReporter]
	public class PhotoController : Controller
	{

		private PhotoSharingContext context = new PhotoSharingContext();


		// GET: Photo
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


		[HttpGet]
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

			if (!ModelState.IsValid)
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


		[HttpGet]
		public ActionResult Delete(int id)
		{
			Photo photo = context.Photos.Where(p => p.PhotoID == id).FirstOrDefault();

			if (photo == null)
			{
				return HttpNotFound();
			}

			return View("Delete", photo);
		}


		[HttpPost]
		[ActionName("Delete")]
		public ActionResult DeleteConfirmed(int id)
		{
			Photo photo = context.Photos.Where(p => p.PhotoID == id).FirstOrDefault();

			if (photo == null)
			{
				return HttpNotFound();
			}

			context.Photos.Remove(photo);
			context.SaveChanges();
			return RedirectToAction("Index");
		}


		public FileContentResult GetImage(int id)
		{
			Photo photo = context.Photos.Where(p => p.PhotoID == id).FirstOrDefault();

			if (photo != null)
			{
				return File(photo.PhotoFile, photo.ImageMimeType);
			}
			else
			{
				return null;
			}
		}
	}
}