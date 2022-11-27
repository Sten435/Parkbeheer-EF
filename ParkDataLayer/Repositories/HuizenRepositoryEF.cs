using Microsoft.EntityFrameworkCore;
using ParkBusinessLayer.Interfaces;
using ParkBusinessLayer.Model;
using ParkDataLayer.Context;
using ParkDataLayer.DbModel;
using System;
using System.Linq;

namespace ParkDataLayer.Repositories {
	public class HuizenRepositoryEF : IHuizenRepository {
		private DatabaseContext _database;

		public HuizenRepositoryEF(string connectionString) {
			_database = new(connectionString);
		}

		public Huis? GeefHuis(int id) {
			HuisDb? huis = _database.Huizen.Include(huis => huis.Park).Include(huis => huis.HuurContracten).AsNoTracking().FirstOrDefault(huis => huis.Id == id);
			if (huis is not null)
				return HuisMapper.MapToHuis(huis);
			throw new Exception($"Huis met id: {id} bestaat niet");
		}

		public bool HeeftHuis(string straat, int nummer, Park park) {
			return _database.Huizen.Any(huis => huis.Straat == straat && huis.Nr == nummer && huis.Park.Id == park.Id);
		}

		public bool HeeftHuis(int id) {
			return _database.Huizen.Any(huis => huis.Id == id);
		}

		public void UpdateHuis(Huis huis) {
			HuisDb huisModel = HuisMapper.MapToHuisDb(GeefHuis(huis.Id));
			huisModel.Straat = huis.Straat;
			huisModel.Actief = huis.Actief;
			huisModel.Nr = huis.Nr;
			huisModel.Park = ParkMapper.MapToParkDb(huis.Park);

			_database.Entry(huisModel).State = EntityState.Modified;

			_database.Update(huisModel);
			SaveAndClear();
		}

		private void SaveAndClear() {
			_database.SaveChanges();
			_database.ChangeTracker.Clear();
		}

		public Huis VoegHuisToe(Huis h) {
			HuisDb huisModel = HuisMapper.MapToHuisDb(h);
			_database.Huizen.Add(huisModel);
			SaveAndClear();
			return HuisMapper.MapToHuis(huisModel);
		}
	}
}
