using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using StackExchange.Redis;
using Waybit.Abstractions.Domain;

namespace Waybit.Abstractions.Repository.Redis
{
	/// <summary>
	/// Used sorted sets redis type
	/// </summary>
	/// <typeparam name="TEntity">Entity type</typeparam>
	/// <typeparam name="TKey">Entity id type</typeparam>
	public abstract class SortedSetRepository<TEntity, TKey> : AbstractRepository<TEntity, TKey>, IRepository<TEntity, TKey>
		where TKey : IEquatable<TKey>
		where TEntity : Entity<TKey>, IAggregateRoot
	{
		/// <inheritdoc />
		protected SortedSetRepository(
			IDatabaseAsync database,
			IEntityConverter converter)
			: base(database, converter)
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
		public async Task<TEntity> GetByIdAsync(TKey id, CancellationToken cancellationToken)
		{
			string key = Router[id.ToString()];

			SortedSetEntry? entry = await Database.SortedSetPopAsync(
				key: key,
				order: Order.Ascending, 
				flags: CommandFlags.PreferSlave);

			return GetFromSortedSetEntry(entry);
		}

		/// <inheritdoc />
		public async Task<TKey> SaveAsync(TEntity entity, CancellationToken cancellationToken)
		{
			string key = Router[entity.Id.ToString()];
			string value = Converter.Serialize(entity);

			int score = GetScore(entity);
			await Database.SortedSetAddAsync(key, value, score, CommandFlags.PreferMaster);

			return entity.Id;
		}

		/// <inheritdoc />
		public async Task RemoveAsync(TEntity entity, CancellationToken cancellationToken)
		{
			string key = Router[entity.Id.ToString()];
			string value = Converter.Serialize(entity);

			await Database.SortedSetRemoveAsync(key, value);
		}

		/// <summary>
		/// Factory method for getting score value from entity
		/// </summary>
		/// <param name="entity">Entity</param>
		public abstract int GetScore(TEntity entity);
		
		private TEntity GetFromSortedSetEntry(SortedSetEntry? entry)
		{
			if (entry is null)
			{
				return null;
			}

			TEntity value = Converter.Deserialize<TEntity, TKey>(entry.Value.Element);
			return value;
		}
	}
}
