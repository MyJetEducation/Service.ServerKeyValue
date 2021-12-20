using System.Runtime.Serialization;

namespace Service.ServerKeyValue.Grpc.Models
{
	[DataContract]
	public class KeysGrpcResponse
	{
		[DataMember(Order = 1)]
		public string[] Keys { get; set; }
	}
}