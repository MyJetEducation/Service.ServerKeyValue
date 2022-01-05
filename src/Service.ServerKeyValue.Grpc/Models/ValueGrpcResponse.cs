using System.Runtime.Serialization;

namespace Service.ServerKeyValue.Grpc.Models
{
	[DataContract]
	public class ValueGrpcResponse
	{
		[DataMember(Order = 1)]
		public string Value { get; set; }
	}
}