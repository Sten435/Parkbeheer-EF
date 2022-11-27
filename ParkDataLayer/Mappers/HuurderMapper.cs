using ParkBusinessLayer.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ParkDataLayer.DbModel {
	public static class HuurderMapper
    {
		public static HuurderDb MapToHuurderDb(Huurder huurder) {
			HuurderDb huurderDb = new();
			huurderDb.Id = huurder.Id;
			huurderDb.Naam = huurder.Naam;
			huurderDb.Adres = huurder.Contactgegevens.Adres;
			huurderDb.Email = huurder.Contactgegevens.Email;
			huurderDb.Tel = huurder.Contactgegevens.Tel;
			return huurderDb;
		}

		public static Huurder MapToHuurder(HuurderDb huurderDb) {
			Huurder huurder = new Huurder(huurderDb.Id, huurderDb.Naam, new ContactGegevens(huurderDb.Email, huurderDb.Tel, huurderDb.Adres));
			return huurder;
		}
	}
}