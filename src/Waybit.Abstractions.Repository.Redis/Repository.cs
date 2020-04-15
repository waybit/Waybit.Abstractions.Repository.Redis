using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using StackExchange.Redis;
using Waybit.Abstractions.Domain;

namespace Waybit.Abstractions.Repository.Redis
{
	/// <inheritdoc />
	public abstract class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
		where TKey : IEquatable<TKey>
		where TEntity : Entity<TKey>, IAggregateRoot
	{
		private readonly IDatabaseAsync _database;
		private readonly IEntityConverter _converter;
		private readonly IRedisKeyStrategy<TKey> _redisKeyStrategy;

		/// <summary>
		/// Initialize instance of class <see cref="Repository"/>
		/// </summary>
		protected Repository(
			IDatabaseAsync database,
			IEntityConverter converter,
			IRedisKeyStrategy<TKey> redisKeyStrategy)
		{
			_database = database ?? throw new ArgumentNullException(nameof(database));
			_converter = converter ?? throw new ArgumentNullException(nameof(converter));
			_redisKeyStrategy = redisKeyStrategy ?? throw new ArgumentNullException(nameof(redisKeyStrategy));
		}

		/// <inheritdoc />
		public Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
		{
			return Task.FromException<IEnumerable<TEntity>>(
				new NotSupportedException(
					$"Cannot get all entities from this repository {this.GetType().FullName}"));
		}

		/// <inheritdoc />
		public async Task<TEntity> GetByIdAsync(TKey id, CancellationToken cancellationToken)
		{
			string key = id.ToString();
			
			RedisValue value = await _database.StringGetAsync(key, CommandFlags.PreferSlave);

			return _converter.Deserialize<TEntity, TKey>(value);
		}

		/// <inheritdoc />
		public async Task<TKey> AddAsync(TEntity entity, CancellationToken cancellationToken)
		{
			TKey key = _redisKeyStrategy.GenerateNewKey();
			string value = _converter.Serialize(entity);

			await _database.StringSetAsync(key.ToString(), value, _redisKeyStrategy.Expiry);

			return key;
		}

		/// <inheritdoc />
		public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
		{
			string value = _converter.Serialize(entity);
			string key = entity.Id.ToString();
			
			await _database.StringSetAsync(key, value, _redisKeyStrategy.Expiry);
		}
	}
}
