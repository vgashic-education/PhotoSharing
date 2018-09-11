using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Web;

namespace PhotoSharing.Models
{
	public class PhotoSharingInitializer : DropCreateDatabaseAlways<PhotoSharingContext>
	{

		protected override void Seed(PhotoSharingContext dbContext)
		{
			#region Photos

			IEnumerable<Photo> photos = new List<Photo>() {
				new Photo {
					Title = "Test photo",
					Description = "Just a test photo for testing test",
					UserName = "NaokiSato",
					PhotoFile = GetFileBytes(@"\images\flower.jpg"),
					ImageMimeType = "image/jpeg",
					CreatedDate = DateTime.Now
				}
			};

			// add photos to dbcontext
			foreach (Photo item in photos)
			{
				dbContext.Photos.Add(item);
			}

			dbContext.SaveChanges();

			#endregion


			#region Comments

			IEnumerable<Comment> comments = new List<Comment>
			{
				new Comment
				{
					PhotoID = 1,
					UserName = "NaokiSato",
					Subject = "Test comment",
					Body = "Test comment for test photo"
				}
			};

			#endregion
		}

		//This gets a byte array for a file at the path specified
		//The path is relative to the route of the web site
		//It is used to seed images
		private byte[] GetFileBytes(string path)
		{
			FileStream fileOnDisk = new FileStream(HttpRuntime.AppDomainAppPath + path, FileMode.Open);
			byte[] fileBytes;
			using (BinaryReader br = new BinaryReader(fileOnDisk))
			{
				fileBytes = br.ReadBytes((int)fileOnDisk.Length);
			}
			return fileBytes;
		}
	}
}