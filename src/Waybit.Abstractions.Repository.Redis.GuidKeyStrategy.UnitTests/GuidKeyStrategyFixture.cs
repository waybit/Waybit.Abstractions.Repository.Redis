using System;
using NUnit.Framework;

namespace Waybit.Abstractions.Repository.Redis.GuidKeyStrategy.UnitTests
{
	[TestFixture]
	public abstract class GuidKeyStrategyFixture
	{
		public IRedisKeyStrategy<Guid> KeysStrategy { get; private set; }

		protected GuidKeyStrategyFixture()
		{
			KeysStrategy = new GuidKeyStrategy(new DefaultKeySettings());
		}
	}
}
