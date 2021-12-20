using System.Runtime.Serialization;

namespace Service.ServerKeyValue.Grpc.Models
{
	[DataContract]
	public class ItemsGrpcResponse
	{
		[DataMember(Order = 1)]
		public KeyValueGrpcModel[] Items { get; set; }
	}
}