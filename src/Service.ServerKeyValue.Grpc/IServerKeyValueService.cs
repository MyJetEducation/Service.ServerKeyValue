using System.ServiceModel;
using System.Threading.Tasks;
using Service.Core.Grpc.Models;
using Service.ServerKeyValue.Grpc.Models;

namespace Service.ServerKeyValue.Grpc
{
	[ServiceContract]
	public interface IServerKeyValueService
	{
		[OperationContract]
		ValueTask<ItemsGrpcResponse> Get(ItemsGetGrpcRequest grpcRequest);

		[OperationContract]
		ValueTask<CommonGrpcResponse> Put(ItemsPutGrpcRequest grpcRequest);

		[OperationContract]
		ValueTask<CommonGrpcResponse> Delete(ItemsDeleteGrpcRequest grpcRequest);

		[OperationContract]
		ValueTask<KeysGrpcResponse> GetKeys(GetKeysGrpcRequest grpcRequest);
	}
}