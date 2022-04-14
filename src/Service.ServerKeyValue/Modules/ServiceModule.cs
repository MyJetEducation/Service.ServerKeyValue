using Autofac;
using MyJetWallet.Sdk.ServiceBus;
using MyServiceBus.Abstractions;
using MyServiceBus.TcpClient;
using Service.ServerKeyValue.Postgres.Services;
using Service.ServerKeyValue.Services;
using Service.ServiceBus.Models;

namespace Service.ServerKeyValue.Modules
{
	public class ServiceModule : Module
	{
		private const string QueueName = "MyJetEducation-ServerKeyValue";

		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<ServerKeyValueRepository>().AsImplementedInterfaces().SingleInstance();
			builder.RegisterType<ClearProgressService>().AsImplementedInterfaces().SingleInstance();

			MyServiceBusTcpClient serviceBusClient = builder.RegisterMyServiceBusTcpClient(Program.ReloadedSettings(e => e.ServiceBusReader), Program.LogFactory);
			builder.RegisterMyServiceBusSubscriberBatch<ClearEducationProgressServiceBusModel>(serviceBusClient, ClearEducationProgressServiceBusModel.TopicName, QueueName, TopicQueueType.Permanent);
		}
	}
}