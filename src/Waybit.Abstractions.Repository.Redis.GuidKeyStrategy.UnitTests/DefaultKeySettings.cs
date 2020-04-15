using System;

namespace Waybit.Abstractions.Repository.Redis.GuidKeyStrategy.UnitTests
{
	/// <inheritdoc />
	public class DefaultKeySettings : IKeySettings
	{
		/// <inheritdoc />
		public TimeSpan Expiry => TimeSpan.FromDays(1);
	}
}
