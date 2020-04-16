using System;
using Waybit.Abstractions.Domain;

namespace Waybit.Abstractions.Repository.Redis
{
	/// <summary>
	/// Service for getting score value from entity. Used in sorted set repository
	/// </summary>
	/// <typeparam name="TEntity">Entity type</typeparam>
	/// <typeparam name="TKey">Entity key</typeparam>
	public interface IScoreFetcher<in TEntity, TKey>
		where TKey : IEquatable<TKey>
		where TEntity : Entity<TKey>, IAggregateRoot
	{
		/// <summary>
		/// Fetch score value from entity
		/// </summary>
		/// <param name="entity">Entity</param>
		int FetchScore(TEntity entity);
	}
}
