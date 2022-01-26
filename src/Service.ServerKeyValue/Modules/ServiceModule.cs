using Autofac;
using Service.ServerKeyValue.Postgres.Services;

namespace Service.ServerKeyValue.Modules
{
	public class ServiceModule : Module
	{
		protected override void Load(ContainerBuilder builder) => builder.RegisterType<ServerKeyValueRepository>().AsImplementedInterfaces().SingleInstance();
	}
}