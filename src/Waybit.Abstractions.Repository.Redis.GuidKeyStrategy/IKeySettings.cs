using System;

namespace Waybit.Abstractions.Repository.Redis.GuidKeyStrategy
{
	/// <summary>
	/// Redis key settings
	/// </summary>
	public interface IKeySettings
	{
		/// <summary>
		/// Key lifetime
		/// </summary>
		TimeSpan Expiry { get; }
	}
}
