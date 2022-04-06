using MyJetWallet.Sdk.Service;
using MyYamlParser;

namespace Service.ServerKeyValue.Settings
{
	public class SettingsModel
	{
		[YamlProperty("ServerKeyValue.SeqServiceUrl")]
		public string SeqServiceUrl { get; set; }

		[YamlProperty("ServerKeyValue.ZipkinUrl")]
		public string ZipkinUrl { get; set; }

		[YamlProperty("ServerKeyValue.ElkLogs")]
		public LogElkSettings ElkLogs { get; set; }

		[YamlProperty("ServerKeyValue.PostgresConnectionString")]
		public string PostgresConnectionString { get; set; }
	}
}
