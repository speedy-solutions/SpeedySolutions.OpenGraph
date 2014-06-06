using System;

namespace SpeedySolutions.Web.OpenGraph
{
	public class OgFileWithSize(Uri url) : OgFile(url)
	{
		[OgProperty("width")]
		public int? Width
		{ get; set; }

		[OgProperty("height")]
		public int? Height
		{ get; set; }

		/// <summary>
		/// Casts the URI to a <see cref="OgFileWithSize"/>
		/// </summary>
		/// <param name="url"></param>
		public static implicit operator OgFileWithSize(Uri url)
		{
			var image = new OgFileWithSize(url);

			if(url.Scheme == "https")
				image.SecureUrl = url;

			return image;
		}
	}
}
