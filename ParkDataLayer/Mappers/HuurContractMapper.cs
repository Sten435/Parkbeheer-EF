using ParkBusinessLayer.Model;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkDataLayer.DbModel {
	public static class HuurContractMapper {
		public static HuurContractDb MapToHuurContractDb(HuurContract contract) {
			HuurContractDb huurContractDb = new();
			huurContractDb.Id = contract.Id;
			huurContractDb.Huurder = HuurderMapper.MapToHuurderDb(contract.Huurder);
			huurContractDb.Huis = HuisMapper.MapToHuisDb(contract.Huis,Context.DatabaseContext.Instance);
			huurContractDb.StartDatum = contract.Huurperiode.StartDatum;
			huurContractDb.EindDatum = contract.Huurperiode.EindDatum;
			huurContractDb.Aantaldagen = contract.Huurperiode.Aantaldagen;
			return huurContractDb;
		}

		public static HuurContract MapToHuurContract(HuurContractDb huurContractDb) {
			HuurContract contract = new(huurContractDb.Id, new HuurPeriode(huurContractDb.StartDatum, huurContractDb.Aantaldagen), HuurderMapper.MapToHuurder(huurContractDb.Huurder), HuisMapper.MapToHuis(huurContractDb.Huis));
			return contract;
		}
	}
}
