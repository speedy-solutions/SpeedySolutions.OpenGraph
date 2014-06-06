using System;

namespace SpeedySolutions.Web.OpenGraph
{
	[AttributeUsage(AttributeTargets.Property)]
	internal class OgPropertyAttribute(string name, string ogNamespace = "og", bool required = false) : Attribute
	{
		public string Name { get; } = name;
		public string Namespace { get; } = ogNamespace;
		public bool Required { get; } = required;

		public override string ToString()
		{
			return ToString(null);
		}

		public string ToString(string prefix)
		{
			if (prefix != null)
			{
				if (string.IsNullOrWhiteSpace(Name))
					return prefix;

				return string.Format("{0}:{1}", prefix, Name);
			}

			if (string.IsNullOrWhiteSpace(Namespace))
				return Name;

			return string.Format("{0}:{1}", Namespace, Name);
		}
	}
}
