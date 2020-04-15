using System;
using NUnit.Framework;
using Shouldly;

namespace Waybit.Abstractions.Repository.Redis.Router.UnitTests
{
	[Parallelizable]
	public class RouterTests
	{
		[Test]
		public void Can_create_empty_route()
		{
			// Act
			string route = new Router();

			// Assert
			route.ShouldBe(string.Empty);
		}
		
		[Test]
		public void Can_empty_route_to_string()
		{
			// Act
			var route = new Router();

			// Assert
			route.ToString().ShouldBe(string.Empty);
		}

		[Test]
		public void Can_create_one_part_route()
		{
			// Arrange
			const string firstPart = "orders";
			
			// Act
			string route = new Router(firstPart);
			
			// Assert
			route.ShouldBe(firstPart);
		}

		[Test]
		public void Can_crate_two_parts_route()
		{
			// Arrange
			const string firstPart = "orders";
			const string secondPart = "3425";
			string fullRoute = $"{firstPart}:{secondPart}";
			
			// Act
			string route = new Router(firstPart)[secondPart];
			
			// Assert
			route.ShouldBe(fullRoute);
		}

		[Test]
		public void Can_create_multiple_parts_route()
		{
			// Arrange
			const string first = "orders";
			const string second = "83239";
			const string third = "products";
			const string fourth = "76256994";
			const string fifth = "description";

			string expected = $"{first}:{second}:{third}:{fourth}:{fifth}";
			
			// Act
			string route = new Router(first)[second][third][fourth][fifth];
			
			// Assert
			route.ShouldBe(expected);
		}

		[Test]
		public void Can_concat_null_part()
		{
			// Arrange
			const string first = "orders";
			const string second = null;

			// Act & Assert
			var exception = Should.Throw<ArgumentException>(() => new Router(first)[second]);
			exception.ParamName.ShouldBe("value");
		}
	}
}
