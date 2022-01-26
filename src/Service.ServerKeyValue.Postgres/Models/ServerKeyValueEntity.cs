using System.ComponentModel.DataAnnotations.Schema;

namespace Service.ServerKeyValue.Postgres.Models
{
	public class ServerKeyValueEntity
	{
		public string Id { get; set; }
		public Guid? UserId { get; set; }
		public string Key { get; set; }

		[Column(TypeName = "jsonb")]
		public string Value { get; set; }
	}
}