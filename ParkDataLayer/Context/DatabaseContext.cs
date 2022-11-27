using Microsoft.EntityFrameworkCore;
using ParkDataLayer.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkDataLayer.Context {
	public class DatabaseContext : DbContext {
		private readonly string _connectionString;

		public DbSet<HuurderDb> Huurders { get; set; }
		public DbSet<HuurContractDb> HuurContracten { get; set; }
		public DbSet<HuisDb> Huizen { get; set; }
		public DbSet<ParkDb> Parken { get; set; }

		public DatabaseContext() {
		}

		public DatabaseContext(string connectionString) {
			_connectionString = connectionString;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
			//optionsBuilder.UseSqlServer(_connectionString);
			optionsBuilder.UseSqlServer(@"Data Source=.\SQLEXPRESS;Initial Catalog=ParkingBeheer;Integrated Security=True;TrustServerCertificate=True");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			modelBuilder.Entity<ParkDb>().HasMany(park => park.Huizen).WithOne(p => p.Park);
			modelBuilder.Entity<HuisDb>().HasMany(huis => huis.HuurContracten).WithOne(h => h.Huis);

			foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) {
				relationship.DeleteBehavior = DeleteBehavior.Restrict;
			}
		}

		private static DatabaseContext instance = null;
		private static readonly object padlock = new object();

		public static DatabaseContext Instance {
			get {
				lock (padlock) {
					if (instance == null) {
						instance = new DatabaseContext();
					}
					return instance;
				}
			}
		}
	}

}
