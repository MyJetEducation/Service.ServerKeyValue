using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Service.ServerKeyValue.Postgres.Services;
using Service.ServerKeyValue.Settings;

namespace Service.ServerKeyValue.Services
{
	public class ClearProgressService : IClearProgressService
	{
		private readonly IServerKeyValueRepository _serverKeyValueRepository;

		public ClearProgressService(IServerKeyValueRepository serverKeyValueRepository) => _serverKeyValueRepository = serverKeyValueRepository;

		public async ValueTask<bool> ClearProgress(string userId, bool progress, bool achievements, bool statuses, bool habits, bool skills, bool knowledge, bool userTime, bool retry)
		{
			var keysList = new List<string>();
			void AddKeys(Func<ProgressKeysSettingsModel, string> value) => keysList.AddRange(value.Invoke(Program.Settings.ProgressKeys).Split(","));

			if (progress)
				AddKeys(k => k.EducationProgressKeys);
			if (achievements)
				AddKeys(k => k.AchievementKeys);
			if (statuses)
				AddKeys(k => k.StatusKeys);
			if (habits)
				AddKeys(k => k.HabitKeys);
			if (skills)
				AddKeys(k => k.SkillKeys);
			if (knowledge)
				AddKeys(k => k.KnowledgeKeys);
			if (userTime)
				AddKeys(k => k.UserTimeKeys);
			if (retry)
				AddKeys(k => k.RetryKeys);

			return await _serverKeyValueRepository.DeleteEntities(userId, keysList.ToArray());
		}
	}
}