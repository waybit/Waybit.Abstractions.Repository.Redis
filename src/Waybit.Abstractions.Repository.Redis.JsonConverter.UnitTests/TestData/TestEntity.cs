using System;
using Waybit.Abstractions.Domain;

namespace Waybit.Abstractions.Repository.Redis.JsonConverter.UnitTests.TestData
{
	public class TestEntity : Entity<Guid>
	{
		/// <inheritdoc />
		public TestEntity()
			: base(Guid.Parse("4a35b1db-c3f3-4e6d-86be-b643cf3d3071"))
		{
		}
	}
}
