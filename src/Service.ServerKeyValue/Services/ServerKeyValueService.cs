using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
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
		private readonly ILogger<ServerKeyValueService> _logger;

		public ServerKeyValueService(IServerKeyValueRepository serverKeyValueRepository, ILogger<ServerKeyValueService> logger)
		{
			_serverKeyValueRepository = serverKeyValueRepository;
			_logger = logger;
		}

		public async ValueTask<ValueGrpcResponse> GetSingle(ItemsGetSingleGrpcRequest request)
		{
			_logger.LogDebug("GetSingle called with: {@request}", request);

			ServerKeyValueEntity[] entities = await _serverKeyValueRepository.GetEntities(request.UserId, request.Key);

			return new ValueGrpcResponse
			{
				Value = entities?.FirstOrDefault()?.Value
			};
		}

		public async ValueTask<ItemsGrpcResponse> Get(ItemsGetGrpcRequest request)
		{
			_logger.LogDebug("Get called with: {@request}", request);

			ServerKeyValueEntity[] entities = await _serverKeyValueRepository.GetEntities(request.UserId, request.Keys);

			return new ItemsGrpcResponse
			{
				Items = entities?.Select(entity => entity.ToGrpcModel()).ToArray()
			};
		}

		public async ValueTask<CommonGrpcResponse> Put(ItemsPutGrpcRequest request)
		{
			_logger.LogDebug("Put called with: {@request}", request);

			string userId = request.UserId;

			bool saved = await _serverKeyValueRepository.SaveEntities(userId, request.Items.Select(model => model.ToEntity(userId)).ToArray());

			return CommonGrpcResponse.Result(saved);
		}

		public async ValueTask<CommonGrpcResponse> Delete(ItemsDeleteGrpcRequest request)
		{
			_logger.LogDebug("Delete called with: {@request}", request);

			bool deleted = await _serverKeyValueRepository.DeleteEntities(request.UserId, request.Keys);

			return CommonGrpcResponse.Result(deleted);
		}

		public async ValueTask<KeysGrpcResponse> GetKeys(GetKeysGrpcRequest request)
		{
			_logger.LogDebug("GetKeys called with: {@request}", request);
			
			string[] keys = await _serverKeyValueRepository.GetKeys(request.UserId);

			return new KeysGrpcResponse {Keys = keys};
		}
	}
}