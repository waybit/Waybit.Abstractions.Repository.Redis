using System;
using NUnit.Framework;
using Shouldly;

namespace Waybit.Abstractions.Repository.Redis.GuidKeyStrategy.UnitTests
{
	[Parallelizable]
	public class GuidKeyStrategyTests : GuidKeyStrategyFixture
	{
		[Test]
		public void Can_create_key()
		{
			// Act
			Guid key = KeysStrategy.GenerateNewKey();

			// Assert
			key.ShouldNotBeNull();
			key.ShouldNotBe(default);
		}

		[Test]
		[Repeat(1000)]
		public void Can_generate_next_key()
		{
			// Act
			Guid key = KeysStrategy.GenerateNewKey();
			Guid nextKey = KeysStrategy.GenerateNewKey();
			
			// Assert
			key.ShouldNotBeNull();
			key.ShouldNotBe(default);
			nextKey.ShouldNotBeNull();
			nextKey.ShouldNotBe(default);
			key.ShouldBeLessThan(nextKey);
		}
	}
}
