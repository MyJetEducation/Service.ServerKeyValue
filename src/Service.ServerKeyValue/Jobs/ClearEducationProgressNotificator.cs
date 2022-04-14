using System.Collections.Generic;
using System.Threading.Tasks;
using DotNetCoreDecorators;
using Microsoft.Extensions.Logging;
using Service.ServerKeyValue.Services;
using Service.ServiceBus.Models;

namespace Service.ServerKeyValue.Jobs
{
	public class ClearEducationProgressNotificator
	{
		private readonly IClearProgressService _clearProgressService;
		private readonly ILogger<ClearEducationProgressNotificator> _logger;

		public ClearEducationProgressNotificator(ILogger<ClearEducationProgressNotificator> logger,
			ISubscriber<IReadOnlyList<ClearEducationProgressServiceBusModel>> subscriber,
			IClearProgressService clearProgressService)
		{
			_logger = logger;
			_clearProgressService = clearProgressService;
			subscriber.Subscribe(HandleEvent);
		}

		private async ValueTask HandleEvent(IReadOnlyList<ClearEducationProgressServiceBusModel> events)
		{
			foreach (ClearEducationProgressServiceBusModel message in events)
			{
				string userId = message.UserId;

				_logger.LogInformation("Clear progress for user: {userId}", userId);

				bool response = await _clearProgressService.ClearProgress(userId,
					message.ClearProgress,
					message.ClearAchievements,
					message.ClearStatuses,
					message.ClearHabits,
					message.ClearSkills,
					message.ClearKnowledge,
					message.ClearUserTime,
					message.ClearRetry);

				if (!response)
					_logger.LogError("Can't clear progress for user: {userId}, request: {@request}", userId, message);
			}
		}
	}
}