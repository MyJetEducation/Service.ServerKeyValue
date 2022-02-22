using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using Service.Grpc;
using Service.ServerKeyValue.Grpc;

namespace Service.ServerKeyValue.Client
{
	[UsedImplicitly]
	public class ServerKeyValueClientFactory : GrpcClientFactory
	{
		public ServerKeyValueClientFactory(string grpcServiceUrl, ILogger logger) : base(grpcServiceUrl, logger)
		{
		}

		public IGrpcServiceProxy<IServerKeyValueService> GetServerKeyValueRepository() => CreateGrpcService<IServerKeyValueService>();
	}
}