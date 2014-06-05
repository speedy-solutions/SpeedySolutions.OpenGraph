using System;
using System.Text;
using System.Reflection;

namespace SpeedySolutions.OpenGraph
{
	public class OpenGraph(string type = "website")
	{
		[OgProperty("title", required: true)]
		public string Title { get; set; }

		[OgProperty("type", required: true)]
		public string Type { get; } = type;

		[OgProperty("url", required: true)]
		public Uri Url { get; set; }

		[OgProperty("image", required: true)]
		public OgImage Image { get; set; }

		public override string ToString()
		{
			var sb = new StringBuilder();

			GetTags(this, sb);

			return sb.ToString();
		}

		private static void GetTags(object obj, StringBuilder sb, string prefix = null)
		{
			var properties = obj.GetType().GetRuntimeProperties();

			foreach (var prop in properties)
			{
				foreach (var attr in prop.GetCustomAttributes<OgPropertyAttribute>())
				{
					var value = prop.GetValue(obj);
					var ogProperty = attr.ToString(prefix);
					if (value == null || value is string && string.IsNullOrWhiteSpace((string)value))
					{
						if (attr.Required)
							throw new ArgumentNullException("value", string.Format("The property {0} is required", attr.ToString()));
						else
							continue;
					}
					else if (value is string || value is Uri || value is int)
					{
						sb.AppendLine(string.Format(@"<meta property=""{0}"" content=""{1}"">", ogProperty, value));
					}
					else
					{
						GetTags(value, sb, ogProperty);
					}
				}
			}
		}
	}
}
