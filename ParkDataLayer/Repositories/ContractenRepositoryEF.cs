using Microsoft.EntityFrameworkCore;
using ParkBusinessLayer.Interfaces;
using ParkBusinessLayer.Model;
using ParkDataLayer.Context;
using ParkDataLayer.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ParkDataLayer.Repositories
{
    public class ContractenRepositoryEF : IContractenRepository
    {
		private readonly DatabaseContext _database;

		public ContractenRepositoryEF(string connectionString) {
			_database = new(connectionString);
		}
		
        public void AnnuleerContract(HuurContract contract)
        {
			HuurContractDb huurContractModel = HuurContractMapper.MapToHuurContractDb(contract);
			_database.HuurContracten.Remove(huurContractModel);
			SaveAndClear();
		}

        public HuurContract GeefContract(string id)
        {
			return HuurContractMapper.MapToHuurContract(_database.HuurContracten.Include(huurContracten => huurContracten.Huurder).Include(huurContracten => huurContracten.Huis).AsNoTracking().FirstOrDefault(huurContract => huurContract.Id == id));
		}

		public List<HuurContract> GeefContracten(DateTime dtBegin, DateTime? dtEinde)
        {
			List<HuurContractDb> huurContractModels;
			if (dtEinde.HasValue) {
				huurContractModels = _database.HuurContracten.Include(huurContracten => huurContracten.Huurder).Include(huurContracten => huurContracten.Huis).AsNoTracking().Where(hc => hc.StartDatum >= dtBegin && hc.EindDatum <= dtEinde.Value).ToList();
			} else {
				huurContractModels = _database.HuurContracten.Include(huurContracten => huurContracten.Huurder).Include(huurContracten => huurContracten.Huis).AsNoTracking().Where(hc => hc.StartDatum >= dtBegin).ToList();
			}
			List<HuurContract> contracten = new();
			foreach (HuurContractDb huurContractModel in huurContractModels) {
				contracten.Add(HuurContractMapper.MapToHuurContract(huurContractModel));
			}
			return contracten;
		}

        public bool HeeftContract(DateTime startDatum, int huurderid, int huisid)
        {
			return _database.HuurContracten.Include(huurContracten => huurContracten.Huurder).Include(huurContracten => huurContracten.Huis).AsNoTracking().Any(contract => contract.Huurder.Id == huurderid && contract.Huis.Id == huisid && contract.StartDatum == startDatum);
		}

		public bool HeeftContract(string id)
        {
			return _database.HuurContracten.Include(huurContracten => huurContracten.Huurder).Include(huurContracten => huurContracten.Huis).AsNoTracking().Any(contract => contract.Id == id);
		}

		public void UpdateContract(HuurContract contract)
        {
			HuurContractDb huurContractModel = _database.HuurContracten.Find(contract.Id);
			huurContractModel.Huurder = HuurderMapper.MapToHuurderDb(contract.Huurder);
			//huurContractModel.Huis = HuisMapper.MapToHuisDb(contract.Huis);
			huurContractModel.Aantaldagen = contract.Huurperiode.Aantaldagen;
			huurContractModel.StartDatum = contract.Huurperiode.StartDatum;
			huurContractModel.EindDatum = contract.Huurperiode.EindDatum;
			SaveAndClear();
		}

        public void VoegContractToe(HuurContract contract)
        {
			HuurContractDb huurContractModel = HuurContractMapper.MapToHuurContractDb(contract);
			_database.Add(huurContractModel);
			SaveAndClear();
		}

		private void SaveAndClear() {
			_database.SaveChanges();
			_database.ChangeTracker.Clear();
		}
	}
}
