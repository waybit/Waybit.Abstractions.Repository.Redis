using System;
using StackExchange.Redis;
using Waybit.Abstractions.Domain;

namespace Waybit.Abstractions.Repository.Redis
{
	/// <summary>
	/// Serialize/Deserialize entity for redis repository
	/// </summary>
	public interface IEntityConverter
	{
		/// <summary>
		/// Serialize entity as string view
		/// </summary>
		/// <param name="entity">Domain entity</param>
		string Serialize(object entity);

		/// <summary>
		/// Deserialize entity string view to entity object 
		/// </summary>
		/// <param name="value">Entity string view</param>
		/// <typeparam name="TEntity">Entity type</typeparam>
		/// <typeparam name="TKey">Entity identity type</typeparam>
		TEntity Deserialize<TEntity, TKey>(in string value)
			where TKey : IEquatable<TKey>
			where TEntity : Entity<TKey>;
	}
}
