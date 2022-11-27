using Microsoft.EntityFrameworkCore;
using ParkBusinessLayer.Model;
using ParkDataLayer.Context;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;

namespace ParkDataLayer.DbModel {
	public static class HuisMapper {
		public static HuisDb MapToHuisDb(Huis huis) {
			return new HuisDb(huis.Id, huis.Straat, huis.Nr, huis.Actief, ParkMapper.MapToParkDb(huis.Park));
		}

		public static Huis MapToHuis(HuisDb huisDb) {
			Huis huis = new Huis(huisDb.Id, huisDb.Straat, huisDb.Nr, huisDb.Actief, ParkMapper.MapToPark(huisDb.Park));
			foreach (HuurContractDb huurContractDb in huisDb?.HuurContracten) {
				huis.VoegHuurcontractToe(HuurContractMapper.MapToHuurContract(huurContractDb));
			}

			return huis;
		}
	}
}