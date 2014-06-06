using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedySolutions.Web.OpenGraph
{
	public class OgFile(Uri url)
	{
		[OgProperty("", required: true)]
		public Uri Url
		{ get; set; }
		= url;

		[OgProperty("secure_url")]
		public Uri SecureUrl
		{ get; set; }

		[OgProperty("type")]
		public string Type
		{ get; set; }

		/// <summary>
		/// Casts the URI to a <see cref="OgFile"/>
		/// </summary>
		/// <param name="url"></param>
		public static implicit operator OgFile(Uri url)
		{
			var image = new OgFile(url);

			if(url.Scheme == "https")
				image.SecureUrl = url;

			return image;
		}
	}
}
