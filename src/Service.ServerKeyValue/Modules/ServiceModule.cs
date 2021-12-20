using Autofac;
using Service.ServerKeyValue.Domain;

namespace Service.ServerKeyValue.Modules
{
	public class ServiceModule : Module
	{
		protected override void Load(ContainerBuilder builder) => builder.RegisterType<ServerKeyValueRepository>().AsImplementedInterfaces().SingleInstance();
	}
}