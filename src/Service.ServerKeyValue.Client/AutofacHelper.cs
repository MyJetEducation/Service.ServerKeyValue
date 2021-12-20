using Autofac;
using Service.ServerKeyValue.Grpc;

// ReSharper disable UnusedMember.Global

namespace Service.ServerKeyValue.Client
{
	public static class AutofacHelper
	{
		public static void RegisterKeyValueClient(this ContainerBuilder builder, string grpcServiceUrl)
		{
			var factory = new ServerKeyValueClientFactory(grpcServiceUrl);

			builder.RegisterInstance(factory.GetServerKeyValueRepository()).As<IServerKeyValueService>().SingleInstance();
		}
	}
}