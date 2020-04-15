using System;

namespace Waybit.Abstractions.Repository.Redis
{
	/// <summary>
	/// Redis key generation strategy
	/// </summary>
	/// <typeparam name="TKey">Redis key type</typeparam>
	public interface IRedisKeyStrategy<out TKey> 
		where TKey : IEquatable<TKey>
	{
		/// <summary>
		/// Generate new key for redis storage
		/// </summary>
		TKey GenerateNewKey();

		/// <summary>
		/// Key lifetime
		/// </summary>
		TimeSpan Expiry { get; }
	}
}
