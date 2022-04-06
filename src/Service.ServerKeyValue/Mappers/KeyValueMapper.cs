using Service.ServerKeyValue.Grpc.Models;
using Service.ServerKeyValue.Postgres.Models;

namespace Service.ServerKeyValue.Mappers
{
	public static class KeyValueMapper
	{
		public static KeyValueGrpcModel ToGrpcModel(this ServerKeyValueEntity entity) => new KeyValueGrpcModel
		{
			Key = entity.Key,
			Value = entity.Value
		};

		public static ServerKeyValueEntity ToEntity(this KeyValueGrpcModel grpcModel, string userId) => new ServerKeyValueEntity
		{
			Id = $"{userId}-{grpcModel.Key}",
			UserId = userId,
			Key = grpcModel.Key,
			Value = grpcModel.Value
		};
	}
}