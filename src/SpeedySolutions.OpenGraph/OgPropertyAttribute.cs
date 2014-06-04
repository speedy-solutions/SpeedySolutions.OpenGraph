using System;

namespace SpeedySolutions.OpenGraph
{
	[AttributeUsage(AttributeTargets.Property)]
	internal class OgPropertyAttribute : Attribute
	{
		public OgPropertyAttribute(string name, string ogNamespace = "og", bool required = false)
		{
			Name = name;
			Namespace = ogNamespace;
			Required = required;
		}

		public string Name { get; private set; }
		public string Namespace { get; private set; }
		public bool Required { get; private set; }

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
