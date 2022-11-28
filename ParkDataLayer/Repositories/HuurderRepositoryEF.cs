using Microsoft.EntityFrameworkCore;
using ParkBusinessLayer.Interfaces;
using ParkBusinessLayer.Model;
using ParkDataLayer.Context;
using ParkDataLayer.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ParkDataLayer.Repositories {
	public class HuurderRepositoryEF : IHuurderRepository {
		private readonly DatabaseContext _database;

		public HuurderRepositoryEF(string connectionString) {
			_database = new(connectionString);
		}

		public Huurder GeefHuurder(int id) {
			return HuurderMapper.MapToHuurder(_database.Huurders.AsNoTracking().FirstOrDefault(huurder => huurder.Id == id));
		}

		public List<Huurder> GeefHuurders(string naam) {
			return _database.Huurders.AsNoTracking().Where(huurder => huurder.Naam.ToLower() == naam.ToLower()).Select(huurder => HuurderMapper.MapToHuurder(huurder)).ToList();
		}

		public bool HeeftHuurder(string naam, ContactGegevens contact) {
			return _database.Huurders.Where(huurder => huurder.Naam.ToLower() == naam.ToLower() &&
			huurder.Adres == contact.Adres &&
			huurder.Email == contact.Email &&
			huurder.Tel == contact.Tel).Any();
		}

		public bool HeeftHuurder(int id) {
			return _database.Huurders.Any(huurder => huurder.Id == id);
		}

		public void UpdateHuurder(Huurder huurder) {
			HuurderDb huurderModel = _database.Huurders.Find(huurder.Id);
			huurderModel.Naam = huurder.Naam;
			huurderModel.Adres = huurder.Contactgegevens.Adres;
			huurderModel.Email = huurder.Contactgegevens.Email;
			huurderModel.Tel = huurder.Contactgegevens.Tel;
			SaveAndClear();
		}

		public Huurder VoegHuurderToe(Huurder h) {
			HuurderDb huurderModel = HuurderMapper.MapToHuurderDb(h);
			_database.Huurders.Add(huurderModel);
			SaveAndClear();
			return HuurderMapper.MapToHuurder(huurderModel);
		}

		private void SaveAndClear() {
			_database.SaveChanges();
			_database.ChangeTracker.Clear();
		}
	}
}
