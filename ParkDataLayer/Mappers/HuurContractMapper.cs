using ParkBusinessLayer.Model;
using ParkDataLayer.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkDataLayer.DbModel {
	public static class HuurContractMapper {
		public static HuurContractDb MapToHuurContractDb(HuurContract contract, DatabaseContext ctx) {
			HuurderDb huurder = ctx.Huurders.Find(contract.Huurder.Id);
			if (huurder is null) {
				huurder = HuurderMapper.MapToHuurderDb(contract.Huurder);
			}

			HuisDb huis = ctx.Huizen.Find(contract.Huis.Id);
			if (huis is null) {
				huis = HuisMapper.MapToHuisDb(contract.Huis, ctx);
			}

			HuurContractDb huurContractDb = new();
			huurContractDb.Id = contract.Id;
			huurContractDb.Huurder = huurder;
			huurContractDb.Huis = huis;
			huurContractDb.StartDatum = contract.Huurperiode.StartDatum;
			huurContractDb.EindDatum = contract.Huurperiode.EindDatum;
			huurContractDb.Aantaldagen = contract.Huurperiode.Aantaldagen;
			return huurContractDb;
		}

		public static HuurContract MapToHuurContract(HuurContractDb huurContractDb, DatabaseContext ctx) {
			Park park = new Park(huurContractDb.Huis.Park.Id, huurContractDb.Huis.Park.Naam, huurContractDb.Huis.Park.Locatie);

			List<Huis> huizen = new();
			if (huurContractDb.Huis.Park.Huizen.Count > 0) {
				foreach (HuisDb huisDb in huurContractDb.Huis.Park.Huizen) {
					Huis h = new Huis(huisDb.Straat, huisDb.Nr, park);
					h.ZetId(huisDb.Id);
					huizen.Add(h);
				}
			}

			huizen.ForEach(park.VoegHuisToe);
			Huis huis = new Huis(huurContractDb.Huis.Straat, huurContractDb.Huis.Nr, park);
			huis.ZetId(huurContractDb.Huis.Id);

			HuurContract contract = new(huurContractDb.Id, new HuurPeriode(huurContractDb.StartDatum, huurContractDb.Aantaldagen), HuurderMapper.MapToHuurder(huurContractDb.Huurder), huis);
			huis.VoegHuurcontractToe(contract);
			contract.ZetHuis(huis);
			
			return contract;
		}
	}
}
