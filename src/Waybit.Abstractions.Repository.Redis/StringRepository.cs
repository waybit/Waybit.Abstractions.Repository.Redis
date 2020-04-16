using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using StackExchange.Redis;
using Waybit.Abstractions.Domain;

namespace Waybit.Abstractions.Repository.Redis
{
	/// <summary>
	/// Used string redis type
	/// </summary>
	/// <typeparam name="TEntity">Entity type</typeparam>
	/// <typeparam name="TKey">Entity id type</typeparam>
	public abstract class StringRepository<TEntity, TKey> : AbstractRepository<TEntity, TKey>, IRepository<TEntity, TKey>
		where TKey : IEquatable<TKey>
		where TEntity : Entity<TKey>, IAggregateRoot
	{
		/// <summary>
		/// Initialize instance of class <see cref="Repository"/>
		/// </summary>
		protected StringRepository(
			IDatabaseAsync database,
			IEntityConverter converter,
			IRedisKeyStrategy<TKey> redisKeyStrategy)
			: base(database, converter, redisKeyStrategy)
		{
		}

		/// <inheritdoc />
		public Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
		{
			return Task.FromException<IEnumerable<TEntity>>(
				new NotSupportedException(
					$"Cannot get all entities from this repository {this.GetType().FullName}"));
		}

		/// <inheritdoc />
		public virtual async Task<TEntity> GetByIdAsync(TKey id, CancellationToken cancellationToken)
		{
			string key = Router[id.ToString()];
			
			RedisValue value = await Database.StringGetAsync(key, CommandFlags.PreferSlave);

			return Converter.Deserialize<TEntity, TKey>(value);
		}

		/// <inheritdoc />
		public virtual async Task<TKey> AddAsync(TEntity entity, CancellationToken cancellationToken)
		{
			TKey id = entity.Id.Equals(default)
				? RedisKeyStrategy.GenerateNewKey()
				: entity.Id;

			string value = Converter.Serialize(entity);

			string key = Router[id.ToString()];

			await Database.StringSetAsync(key, value, RedisKeyStrategy.Expiry);

			return id;
		}

		/// <inheritdoc />
		public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
		{
			string value = Converter.Serialize(entity);
			string key = Router[entity.Id.ToString()];
			
			await Database.StringSetAsync(key, value, RedisKeyStrategy.Expiry);
		}
	}
}
