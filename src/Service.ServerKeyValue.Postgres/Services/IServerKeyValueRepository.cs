using Service.ServerKeyValue.Postgres.Models;

namespace Service.ServerKeyValue.Postgres.Services
{
	public interface IServerKeyValueRepository
	{
		ValueTask<ServerKeyValueEntity[]> GetEntities(Guid? userId, params string[] keys);

		ValueTask<bool> SaveEntities(Guid? userId, ServerKeyValueEntity[] entities);
		
		ValueTask<bool> DeleteEntities(Guid? userId, string[] keys);

		ValueTask<string[]> GetKeys(Guid? userId);
	}
}