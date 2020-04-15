using System;
using Newtonsoft.Json;
using NUnit.Framework;
using Shouldly;
using Waybit.Abstractions.Repository.Redis.JsonConverter.UnitTests.TestData;

namespace Waybit.Abstractions.Repository.Redis.JsonConverter.UnitTests
{
	[Parallelizable]
	public class JsonEntityConverterTests : JsonEntityConverterFixture
	{
		[Test]
		public void Can_convert_entity_identity()
		{
			// Arrange
			string fileText = ReadStringFromFile("testEntity.json");
			var expected = JsonConvert.DeserializeObject<TestEntity>(fileText);
			
			// Act
			TestEntity actual = EntityConverter.Deserialize<TestEntity, Guid>(fileText);
			
			// Assert
			actual.ShouldNotBeNull();
			actual.Id.ShouldBe(expected.Id);
		}
	}
}
