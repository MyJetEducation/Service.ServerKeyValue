using System;
using System.Threading.Tasks;

namespace Service.ServerKeyValue.Domain.Models
{
	public interface IServerKeyValueRepository
	{
		ValueTask<ServerKeyValueEntity[]> GetEntities(Guid? userId, params string[] keys);

		ValueTask<bool> SaveEntities(Guid? userId, ServerKeyValueEntity[] entities);
		
		ValueTask<bool> DeleteEntities(Guid? userId, string[] keys);

		ValueTask<string[]> GetKeys(Guid? userId);
	}
}