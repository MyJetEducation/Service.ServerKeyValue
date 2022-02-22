using Autofac;
using Microsoft.Extensions.Logging;
using Service.Grpc;
using Service.ServerKeyValue.Grpc;

// ReSharper disable UnusedMember.Global

namespace Service.ServerKeyValue.Client
{
	public static class AutofacHelper
	{
		public static void RegisterServerKeyValueClient(this ContainerBuilder builder, string grpcServiceUrl, ILogger logger)
		{
			var factory = new ServerKeyValueClientFactory(grpcServiceUrl, logger);

			builder.RegisterInstance(factory.GetServerKeyValueRepository()).As<IGrpcServiceProxy<IServerKeyValueService>>().SingleInstance();
		}
	}
}