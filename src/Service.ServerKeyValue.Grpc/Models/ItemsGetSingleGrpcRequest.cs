using System;
using System.Runtime.Serialization;

namespace Service.ServerKeyValue.Grpc.Models
{
	[DataContract]
	public class ItemsGetSingleGrpcRequest
	{
		[DataMember(Order = 1)]
		public Guid? UserId { get; set; }

		[DataMember(Order = 2)]
		public string Key { get; set; }
	}
}