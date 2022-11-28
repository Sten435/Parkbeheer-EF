using ParkBusinessLayer.Exceptions;
using ParkBusinessLayer.Interfaces;
using ParkBusinessLayer.Model;
using System;
using System.Collections.Generic;

namespace ParkBusinessLayer.Beheerders {
	public class BeheerContracten {
		private IContractenRepository repo;

		public BeheerContracten(IContractenRepository repo) {
			this.repo = repo;
		}

		public void MaakContract(string id, HuurPeriode huurperiode, Huurder huurder, Huis huis) {
			try {
				HuurContract contract = new HuurContract(id, huurperiode, huurder, huis);
				if (repo.HeeftContract(huurperiode.StartDatum, huurder.Id, huis.Id))
					throw new BeheerderException("Contract bestaat al");
				repo.VoegContractToe(contract);
			} catch (Exception ex) {
				throw new BeheerderException(ex.Message);
			}
		}
		public void AnnuleerContract(HuurContract contract) {
			try {
				repo.AnnuleerContract(contract);
			} catch (Exception ex) {
				throw new BeheerderException(ex.Message);
			}
		}
		public void UpdateContract(HuurContract contract) {
			try {
				if (!repo.HeeftContract(contract.Id)) throw new BeheerderException("Contract bestaat niet");
				repo.UpdateContract(contract);
			} catch (Exception ex) {
				throw new BeheerderException(ex.Message);
			}
		}
		public HuurContract GeefContract(string id) {
			try {
				return repo.GeefContract(id);
			} catch (Exception ex) {
				throw new BeheerderException(ex.Message);
			}
		}
		public List<HuurContract> GeefContracten(DateTime dtBegin, DateTime? dtEinde) {
			try {
				return repo.GeefContracten(dtBegin, dtEinde);
			} catch (Exception ex) {
				throw new BeheerderException(ex.Message);
			}
		}
	}
}
