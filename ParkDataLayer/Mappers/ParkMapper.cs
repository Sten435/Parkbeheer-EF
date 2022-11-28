using Microsoft.EntityFrameworkCore;
using ParkBusinessLayer.Model;
using ParkDataLayer.Context;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;
using System.Linq;

namespace ParkDataLayer.DbModel {
	public static class ParkMapper {
		public static ParkDb MapToParkDb(Park park) {
			ParkDb paDb = DatabaseContext.Instance.Parken.Include(park => park.Huizen).AsNoTracking().FirstOrDefault(p => p.Id == park.Id);

			if (paDb is not null) return paDb;

			ParkDb parkDb = new();
			parkDb.Id = park.Id;
			parkDb.Naam = park.Naam;
			parkDb.Locatie = park.Locatie;
			parkDb.Huizen = park.Huizen().Select(huis => HuisMapper.MapToHuisDb(huis,DatabaseContext.Instance)).ToList();

			return parkDb;
		}

		public static Park MapToPark(ParkDb parkDb) {
			Park park = new Park(parkDb.Id, parkDb.Naam, parkDb.Locatie);

			List<Huis> huizen = new();
			if (parkDb.Huizen.Count > 0) {
				foreach (HuisDb huis in parkDb.Huizen) {
					Huis h = new Huis(huis.Straat, huis.Nr, park);
					h.ZetId(huis.Id);
					huizen.Add(h);
				}
			}

			huizen.ForEach(park.VoegHuisToe);

			return park;
		}
	}
}
