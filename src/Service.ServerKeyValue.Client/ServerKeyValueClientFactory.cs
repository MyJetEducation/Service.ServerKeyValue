using JetBrains.Annotations;
using MyJetWallet.Sdk.Grpc;
using Service.ServerKeyValue.Grpc;

namespace Service.ServerKeyValue.Client
{
	[UsedImplicitly]
	public class ServerKeyValueClientFactory : MyGrpcClientFactory
	{
		public ServerKeyValueClientFactory(string grpcServiceUrl) : base(grpcServiceUrl)
		{
		}

		public IServerKeyValueService GetServerKeyValueRepository() => CreateGrpcService<IServerKeyValueService>();
	}
}