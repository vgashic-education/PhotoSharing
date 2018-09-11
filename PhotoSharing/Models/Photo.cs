using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhotoSharing.Models
{
	public class Photo
	{
		public int PhotoID { get; set; }

		public string Title { get; set; }

		[DisplayName("Picture")]
		public byte[] PhotoFile { get; set; }

		public string ImageMimeType { get; set; }

		[DataType(DataType.MultilineText)]
		public string Description { get; set; }
		
		[DataType(DataType.DateTime)]
		[DisplayName("Created date")]
		[DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
		public DateTime CreatedDate { get; set; }

		public string UserName { get; set; }

		public virtual IEnumerable<Comment> Comments { get; set; }
	}
}