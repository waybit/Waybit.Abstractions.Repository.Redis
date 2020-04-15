using System;
using System.Collections.Generic;

namespace Waybit.Abstractions.Repository.Redis.Router
{
	/// <summary>
	/// Build a Redis key
	/// </summary>
	public struct Router
	{
		private const string Separator = ":";

		private readonly string _key;

		/// <summary>
		/// Initialize instance of a value object <see cref="Router"/>
		/// </summary>
		/// <param name="prefix">Key prefix</param>
		public Router(string prefix)
		{
			_key = prefix;
		}

		/// <summary>
		/// Returns built key
		/// </summary>
		/// <param name="value">Next parameter in key chain</param>
		public Router this[string value]
		{
			get
			{
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentException("Value cannot be null or empty.", nameof(value));
				}

				return this.Concat(value);
			}
		}

		/// <inheritdoc />
		public static implicit operator string(Router router)
		{
			return router.ToString();
		}

		/// <inheritdoc />
		public static implicit operator Router(string value)
		{
			return new Router(value);
		}

		/// <inheritdoc />
		public override string ToString()
		{
			return this._key ?? string.Empty;
		}

		private string Concat(string value)
		{
			if (string.IsNullOrEmpty(_key))
			{
				return value;
			}

			return string.Join(Separator, _key, value);
		}
	}
}
