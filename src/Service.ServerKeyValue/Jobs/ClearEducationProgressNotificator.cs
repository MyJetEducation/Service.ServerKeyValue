using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetCoreDecorators;
using Microsoft.Extensions.Logging;
using Service.ServerKeyValue.Grpc;
using Service.ServerKeyValue.Grpc.Models;
using Service.ServiceBus.Models;

namespace Service.ServerKeyValue.Jobs
{
	public class ClearEducationProgressNotificator
	{
		private readonly IServerKeyValueService _serverKeyValueService;
		private readonly ILogger<ClearEducationProgressNotificator> _logger;

		public ClearEducationProgressNotificator(ILogger<ClearEducationProgressNotificator> logger,
			ISubscriber<IReadOnlyList<ClearEducationProgressServiceBusModel>> subscriber,
			IServerKeyValueService serverKeyValueService)
		{
			_logger = logger;
			_serverKeyValueService = serverKeyValueService;
			subscriber.Subscribe(HandleEvent);
		}

		private async ValueTask HandleEvent(IReadOnlyList<ClearEducationProgressServiceBusModel> events)
		{
			foreach (ClearEducationProgressServiceBusModel message in events)
			{
				string userId = message.UserId;

				_logger.LogInformation("Clear full education progress for user: {userId}", userId);

				await _serverKeyValueService.ClearProgressValues(ClearProgressGrpcRequest.ClearAll(userId));
			}
		}
	}
}