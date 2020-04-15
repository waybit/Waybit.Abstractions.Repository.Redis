using System;
using Newtonsoft.Json;
using StackExchange.Redis;
using Waybit.Abstractions.Domain;

namespace Waybit.Abstractions.Repository.Redis.JsonConverter
{
	/// <inheritdoc />
	public class JsonEntityConverter : IEntityConverter
	{
		/// <inheritdoc />
		public string Serialize(object entity)
		{
			return JsonConvert.SerializeObject(entity);
		}

		/// <inheritdoc />
		public TEntity Deserialize<TEntity, TKey>(in string value) 
			where TEntity : Entity<TKey>
			where TKey : IEquatable<TKey>
		{
			return JsonConvert.DeserializeObject<TEntity>(value);
		}
	}
}
