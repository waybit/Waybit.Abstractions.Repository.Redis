using System;
using System.IO;
using System.Reflection;
using NUnit.Framework;

namespace Waybit.Abstractions.Repository.Redis.JsonConverter.UnitTests
{
	[TestFixture]
	public class JsonEntityConverterFixture
	{
		protected JsonEntityConverterFixture()
		{
			EntityConverter = new JsonEntityConverter();
		}

		public IEntityConverter EntityConverter { get; private set; }

		protected string ReadStringFromFile(string filename)
		{
			string basePath = Path.GetDirectoryName(typeof(JsonEntityConverterTests).Assembly.Location);
			string filePath = Path.Combine(basePath, "TestData", filename);

			return File.ReadAllText(filePath);
		}
	}
}
