using System;
using StackExchange.Redis;
using Waybit.Abstractions.Domain;

namespace Waybit.Abstractions.Repository.Redis
{
	/// <summary>
	/// Redis abstract repository
	/// </summary>
	/// <typeparam name="TEntity">Entity type</typeparam>
	/// <typeparam name="TKey">Entity id type</typeparam>
	public abstract class AbstractRepository<TEntity, TKey>
		where TKey : IEquatable<TKey>
		where TEntity : Entity<TKey>, IAggregateRoot
	{
		/// <summary>
		/// Entity redis router
		/// </summary>
		protected Router.Router Router => new Router.Router(typeof(TEntity).Name.ToLowerInvariant());

		/// <summary>
		/// Redis database
		/// </summary>
		protected IDatabaseAsync Database { get; private set; }

		/// <summary>
		/// Value converter
		/// </summary>
		protected IEntityConverter Converter { get; private set; }

		/// <summary>
		/// Initialize instance of class <see cref="Repository"/>
		/// </summary>
		protected AbstractRepository(
			IDatabaseAsync database,
			IEntityConverter converter)
		{
			Database = database ?? throw new ArgumentNullException(nameof(database));
			Converter = converter ?? throw new ArgumentNullException(nameof(converter));
		}
	}
}
