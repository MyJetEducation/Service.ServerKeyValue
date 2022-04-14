using System.Linq;
using System.Threading.Tasks;
using Service.Core.Client.Models;
using Service.ServerKeyValue.Grpc;
using Service.ServerKeyValue.Grpc.Models;
using Service.ServerKeyValue.Mappers;
using Service.ServerKeyValue.Postgres.Models;
using Service.ServerKeyValue.Postgres.Services;

namespace Service.ServerKeyValue.Services
{
	public class ServerKeyValueService : IServerKeyValueService
	{
		private readonly IServerKeyValueRepository _serverKeyValueRepository;

		public ServerKeyValueService(IServerKeyValueRepository serverKeyValueRepository) => _serverKeyValueRepository = serverKeyValueRepository;

		public async ValueTask<ValueGrpcResponse> GetSingle(ItemsGetSingleGrpcRequest request)
		{
			ServerKeyValueEntity[] entities = await _serverKeyValueRepository.GetEntities(request.UserId, request.Key);

			return new ValueGrpcResponse
			{
				Value = entities?.FirstOrDefault()?.Value
			};
		}

		public async ValueTask<ItemsGrpcResponse> Get(ItemsGetGrpcRequest request)
		{
			ServerKeyValueEntity[] entities = await _serverKeyValueRepository.GetEntities(request.UserId, request.Keys);

			return new ItemsGrpcResponse
			{
				Items = entities?.Select(entity => entity.ToGrpcModel()).ToArray()
			};
		}

		public async ValueTask<CommonGrpcResponse> Put(ItemsPutGrpcRequest request)
		{
			string userId = request.UserId;

			bool saved = await _serverKeyValueRepository.SaveEntities(userId, request.Items.Select(model => model.ToEntity(userId)).ToArray());

			return CommonGrpcResponse.Result(saved);
		}

		public async ValueTask<CommonGrpcResponse> Delete(ItemsDeleteGrpcRequest request)
		{
			bool deleted = await _serverKeyValueRepository.DeleteEntities(request.UserId, request.Keys);

			return CommonGrpcResponse.Result(deleted);
		}

		public async ValueTask<KeysGrpcResponse> GetKeys(GetKeysGrpcRequest request)
		{
			string[] keys = await _serverKeyValueRepository.GetKeys(request.UserId);

			return new KeysGrpcResponse {Keys = keys};
		}
	}
}