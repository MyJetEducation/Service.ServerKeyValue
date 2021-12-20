using System;

namespace Service.ServerKeyValue.Domain.Models
{
	public class ServerKeyValueEntity
	{
		public string Id { get; set; }
		public Guid? UserId { get; set; }
		public string Key { get; set; }
		public string Value { get; set; }
	}
}