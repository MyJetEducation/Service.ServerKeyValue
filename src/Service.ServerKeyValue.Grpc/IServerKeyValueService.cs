using System.ServiceModel;
using System.Threading.Tasks;
using Service.Core.Client.Models;
using Service.ServerKeyValue.Grpc.Models;

namespace Service.ServerKeyValue.Grpc
{
	[ServiceContract]
	public interface IServerKeyValueService
	{
		[OperationContract]
		ValueTask<ValueGrpcResponse> GetSingle(ItemsGetSingleGrpcRequest request);

		[OperationContract]
		ValueTask<ItemsGrpcResponse> Get(ItemsGetGrpcRequest request);

		[OperationContract]
		ValueTask<CommonGrpcResponse> Put(ItemsPutGrpcRequest request);

		[OperationContract]
		ValueTask<CommonGrpcResponse> Delete(ItemsDeleteGrpcRequest request);

		[OperationContract]
		ValueTask<KeysGrpcResponse> GetKeys(GetKeysGrpcRequest request);
	}
}