using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Service.ServerKeyValue.Domain.Models;
using Service.ServerKeyValue.Postgres;

namespace Service.ServerKeyValue.Domain
{
	public class ServerKeyValueRepository : IServerKeyValueRepository
	{
		private readonly ILogger<ServerKeyValueRepository> _logger;
		private readonly DbContextOptionsBuilder<DatabaseContext> _dbContextOptionsBuilder;

		public ServerKeyValueRepository(ILogger<ServerKeyValueRepository> logger, DbContextOptionsBuilder<DatabaseContext> dbContextOptionsBuilder)
		{
			_logger = logger;
			_dbContextOptionsBuilder = dbContextOptionsBuilder;
		}

		public async ValueTask<ServerKeyValueEntity[]> GetEntities(Guid? userId, params string[] keys)
		{
			try
			{
				return await GetContext()
					.ServerKeyValues
					.Where(entity => entity.UserId == userId)
					.Where(entity => keys.Contains(entity.Key))
					.ToArrayAsync();
			}
			catch (Exception exception)
			{
				_logger.LogError(exception, exception.Message);
			}

			return await ValueTask.FromResult<ServerKeyValueEntity[]>(null);
		}

		public async ValueTask<bool> SaveEntities(Guid? userId, ServerKeyValueEntity[] entities)
		{
			string[] keys = entities.Select(model => model.Key).ToArray();
			List<ServerKeyValueEntity> newEntitiesList = entities.ToList();

			DatabaseContext context = GetContext();
			DbSet<ServerKeyValueEntity> dbSet = context.ServerKeyValues;

			try
			{
				ServerKeyValueEntity[] existingEntities = await dbSet
					.Where(entity => entity.UserId == userId)
					.Where(entity => keys.Contains(entity.Key))
					.ToArrayAsync();

				if (existingEntities.Any())
				{
					UpdateExistingEntities(existingEntities, newEntitiesList);
					dbSet.UpdateRange(existingEntities);
				}

				if (newEntitiesList.Any())
					await dbSet.AddRangeAsync(newEntitiesList);

				await context.SaveChangesAsync();

				return true;
			}
			catch (Exception exception)
			{
				_logger.LogError(exception, exception.Message);
			}

			return false;
		}

		private static void UpdateExistingEntities(IEnumerable<ServerKeyValueEntity> existingEntities, List<ServerKeyValueEntity> newEntitiesList)
		{
			foreach (ServerKeyValueEntity entity in existingEntities)
			{
				ServerKeyValueEntity requestItem = newEntitiesList.First(model => model.Key == entity.Key);
				entity.Value = requestItem.Value;
				newEntitiesList.Remove(requestItem);
			}
		}

		public async ValueTask<bool> DeleteEntities(Guid? userId, string[] keys)
		{
			DatabaseContext context = GetContext();
			DbSet<ServerKeyValueEntity> dbSet = context.ServerKeyValues;

			try
			{
				ServerKeyValueEntity[] items = await dbSet
					.Where(entity => entity.UserId == userId)
					.Where(entity => keys.Contains(entity.Key))
					.ToArrayAsync();

				if (!items.Any())
					return false;

				dbSet.RemoveRange(items);

				await context.SaveChangesAsync();

				return true;
			}
			catch (Exception exception)
			{
				_logger.LogError(exception, exception.Message);
			}

			return false;
		}

		public async ValueTask<string[]> GetKeys(Guid? userId)
		{
			try
			{
				string[] items = await GetContext()
					.ServerKeyValues
					.Where(entity => entity.UserId == userId)
					.Select(entity => entity.Key)
					.ToArrayAsync();

				return items;
			}
			catch (Exception exception)
			{
				_logger.LogError(exception, exception.Message);
			}

			return await ValueTask.FromResult<string[]>(null);
		}

		private DatabaseContext GetContext() => DatabaseContext.Create(_dbContextOptionsBuilder);
	}
}