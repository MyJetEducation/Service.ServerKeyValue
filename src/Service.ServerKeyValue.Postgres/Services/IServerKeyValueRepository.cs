using Service.ServerKeyValue.Postgres.Models;

namespace Service.ServerKeyValue.Postgres.Services
{
	public interface IServerKeyValueRepository
	{
		ValueTask<ServerKeyValueEntity[]> GetEntities(string userId, params string[] keys);

		ValueTask<bool> SaveEntities(string userId, ServerKeyValueEntity[] entities);

		ValueTask<bool> DeleteEntities(string userId, string[] keys);

		ValueTask<string[]> GetKeys(string userId);
	}
}