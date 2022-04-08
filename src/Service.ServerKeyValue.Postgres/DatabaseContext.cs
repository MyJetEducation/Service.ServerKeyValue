using Microsoft.EntityFrameworkCore;
using MyJetWallet.Sdk.Postgres;
using MyJetWallet.Sdk.Service;
using Service.ServerKeyValue.Postgres.Models;

namespace Service.ServerKeyValue.Postgres
{
	public class DatabaseContext : MyDbContext
	{
		public const string Schema = "education";
		private const string ServerKeyValueTableName = "serverkeyvalue";

		public DatabaseContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<ServerKeyValueEntity> ServerKeyValues { get; set; }

		public static DatabaseContext Create(DbContextOptionsBuilder<DatabaseContext> options)
		{
			MyTelemetry.StartActivity($"Database context {Schema}")?.AddTag("db-schema", Schema);

			return new DatabaseContext(options.Options);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.HasDefaultSchema(Schema);

			SetUserInfoEntityEntry(modelBuilder);

			base.OnModelCreating(modelBuilder);
		}

		private static void SetUserInfoEntityEntry(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ServerKeyValueEntity>().ToTable(ServerKeyValueTableName);
			modelBuilder.Entity<ServerKeyValueEntity>().HasKey(e => e.Id);
			modelBuilder.Entity<ServerKeyValueEntity>().Property(e => e.Key).IsRequired();
			modelBuilder.Entity<ServerKeyValueEntity>().Property(e => e.UserId).IsRequired();
			modelBuilder.Entity<ServerKeyValueEntity>().Property(e => e.Value).IsRequired();
			modelBuilder.Entity<ServerKeyValueEntity>().HasIndex(e => new { e.UserId, e.Key }).IsUnique();
		}
	}
}