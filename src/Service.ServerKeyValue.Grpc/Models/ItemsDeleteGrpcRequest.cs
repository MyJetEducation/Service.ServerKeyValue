using System.Runtime.Serialization;

namespace Service.ServerKeyValue.Grpc.Models
{
	[DataContract]
	public class ItemsDeleteGrpcRequest
	{
		[DataMember(Order = 1)]
		public string UserId { get; set; }

		[DataMember(Order = 2)]
		public string[] Keys { get; set; }
	}
}