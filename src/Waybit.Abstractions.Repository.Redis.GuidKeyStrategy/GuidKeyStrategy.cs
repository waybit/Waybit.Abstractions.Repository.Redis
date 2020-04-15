using System;
using MassTransit;

namespace Waybit.Abstractions.Repository.Redis.GuidKeyStrategy
{
	/// <inheritdoc />
	public class GuidKeyStrategy : IRedisKeyStrategy<Guid>
	{
		private readonly IKeySettings _settings;

		/// <summary>
		/// Initialize instance of class <see cref="GuidKeyStrategy"/>
		/// </summary>
		public GuidKeyStrategy(IKeySettings settings)
		{
			_settings = settings ?? throw new ArgumentNullException(nameof(settings));
		}

		/// <inheritdoc />
		public Guid GenerateNewKey()
		{
			return NewId.NextGuid();
		}

		/// <inheritdoc />
		public TimeSpan Expiry => _settings.Expiry;
	}
}
