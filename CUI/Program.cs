using ParkBusinessLayer.Beheerders;
using ParkBusinessLayer.Exceptions;
using ParkBusinessLayer.Interfaces;
using ParkBusinessLayer.Model;
using ParkDataLayer.Context;
using ParkDataLayer.Repositories;
using System.Configuration;

namespace CUI {
	public class Program {
		private static string _connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
		private static readonly IHuizenRepository _huizenRepo = new HuizenRepositoryEF(_connectionString);
		private static readonly IContractenRepository _contractenRepo = new ContractenRepositoryEF(_connectionString);
		private static readonly IHuurderRepository _huurderRepo = new HuurderRepositoryEF(_connectionString);

		private static readonly BeheerHuizen _huizenManager = new(_huizenRepo);
		private static readonly BeheerContracten _contractenManager = new(_contractenRepo);
		private static readonly BeheerHuurders _huurderManager = new(_huurderRepo);

		static void WriteValid(string message) {
			Console.ResetColor();
			Console.Write($"{message} ");
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("[\u221A]");
			Console.ResetColor();
			Console.WriteLine();
		}

		static void WriteError(string message) {
			Console.ResetColor();
			Console.Write($"{message} ");
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("[?]");
			Console.ResetColor();
			Console.WriteLine();
		}

		static void Main(string[] args) {
			var db = new DatabaseContext(_connectionString);
			db.Database.EnsureDeleted();
			db.Database.EnsureCreated();

			Park park = new Park("Park1", "Naam1", "Locatie1");
			Park park2 = new Park("Park2", "Naam2", "Locatie2");
			Huis huis = new Huis("Huis1", 1, park);
			Huis huis2 = new Huis("Huis2", 2, park2);

			//Voeg nieuw huis toe
			Console.WriteLine("#TEST - VoegNieuwHuisToe");
			huis = _huizenManager.VoegNieuwHuisToe(huis.Straat, huis.Nr, huis.Park);
			if (huis.Id != 0) {
				WriteValid("Huis toegevoegd");
			} else {
				WriteError("Huis toegevoegd");
			}

			//Voeg huis die al bestaat toe
			Console.WriteLine("#TEST - VoegNieuwHuisToe - [Die al bestaat]");

			try {
				huis = _huizenManager.VoegNieuwHuisToe(huis.Straat, huis.Nr, huis.Park);
			} catch (BeheerderException ex) {
				if (ex.Message == "Huis bestaat al") {
					WriteValid("Huis toevoegen die al bestaat");
				} else {
					WriteError("Huis toevoegen die al bestaat");
				}
			}

			//Update huis
			Huis? updatedHuis = _huizenManager.GeefHuis(huis.Id);

			Console.WriteLine("#TEST - GeefHuis");
			if (updatedHuis is not null) {
				WriteValid("Geef huis");
			} else {
				WriteError("Geef huis");
			}

			Console.WriteLine("#TEST - UpdateHuis");
			string updatedStraat = Guid.NewGuid().ToString().Substring(0, 15);
			updatedHuis.ZetStraat(updatedStraat);

			_huizenManager.UpdateHuis(updatedHuis);
			if (updatedStraat == updatedHuis.Straat) {
				WriteValid("Huis updaten");
			} else {
				WriteError("Huis updaten");
			}


			//Verwijder huis
			Console.WriteLine("#TEST ArchiveerHuis");
			huis = new Huis("Huis2", 2, park);

			huis = _huizenManager.VoegNieuwHuisToe(huis.Straat, huis.Nr, huis.Park);

			_huizenManager.ArchiveerHuis(huis);
			if (!huis.Actief) {
				WriteValid("Huis archiveren");
			} else {
				WriteError("Huis archiveren");
			}

			Console.WriteLine("\n--- Huurder ---\n\n");

			//Huurder toevoegen
			Console.WriteLine("#TEST - VoegNieuweHuurderToe");
			ContactGegevens contactgegevens = new("Email1", "Telefoon1", "Adres1");
			Huurder huurder = new Huurder("Naam1", contactgegevens);
			huurder = _huurderManager.VoegNieuweHuurderToe(huurder.Naam, huurder.Contactgegevens);
			if (huurder.Id != 0) {
				WriteValid("Huurder toegevoegd");
			} else {
				WriteError("Huurder toegevoegd");
			}
			Console.WriteLine("#TEST - VoegNieuweHuurderToe - [Huurder bestaat al]");
			try {
				huurder = _huurderManager.VoegNieuweHuurderToe(huurder.Naam, huurder.Contactgegevens);
			} catch (BeheerderException ex) {
				if (ex.Message == "Huurder bestaat al") {
					WriteValid("Huurder toevoegen die al bestaat");
				} else {
					WriteError($"Huurder toevoegen die al bestaat");
				}
			}

			//Huurder updaten
			Console.WriteLine("#TEST - UpdateHuurder");
			string nieuweNaam = Guid.NewGuid().ToString().Substring(0, 4);
			huurder.ZetNaam(nieuweNaam);
			_huurderManager.UpdateHuurder(huurder);
			var dbHuurder = _huurderManager.GeefHuurder(huurder.Id);
			if (dbHuurder.Equals(huurder)) {
				WriteValid("Huurder updaten");
			} else {
				WriteError("Huurder updaten");
			}

			//Huurders tonen
			Console.WriteLine("#TEST - GeefHuurders");
			ContactGegevens contactgegevens2 = new("Email2", "Telefoon2", "Adres2");
			ContactGegevens contactgegevens3 = new("Email4", "Telefoon4", "Adres4");

			Huurder huurder2 = new Huurder("Naam1", contactgegevens2);
			Huurder huurder3 = new Huurder("Naam1", contactgegevens3);
			Huurder huurder4 = new Huurder("Naam4", contactgegevens2);
			Huurder huurder5 = new Huurder("Naam5", contactgegevens2);
			Huurder huurder6 = new Huurder("Naam6", contactgegevens2);

			_huurderManager.VoegNieuweHuurderToe(huurder2.Naam, huurder2.Contactgegevens);
			_huurderManager.VoegNieuweHuurderToe(huurder3.Naam, huurder3.Contactgegevens);
			_huurderManager.VoegNieuweHuurderToe(huurder4.Naam, huurder4.Contactgegevens);
			_huurderManager.VoegNieuweHuurderToe(huurder5.Naam, huurder5.Contactgegevens);
			_huurderManager.VoegNieuweHuurderToe(huurder6.Naam, huurder6.Contactgegevens);

			var huurders = _huurderManager.GeefHuurders("Naam1");
			if (huurders.Count == 2) {
				WriteValid("Huurders tonen");
			} else {
				WriteError("Huurders tonen");
			}

			Console.WriteLine("\n--- Contracten ---\n\n");

			//Contract toevoegen
			Console.WriteLine("#TEST - VoegContactToe");
			HuurPeriode huurPeriode = new(DateTime.Now, 5);
			HuurContract huurContract = new("Huur1", huurPeriode, huurder, huis);
			_contractenManager.MaakContract(huurContract.Id, huurContract.Huurperiode, huurContract.Huurder, huurContract.Huis);
			var dbHuurContract = _contractenManager.GeefContract(huurContract.Id);
			if (dbHuurContract is not null) {
				WriteValid("HuurContract maken");
			} else {
				WriteError("HuurContract maken");
			}

			//Contract updaten
			Console.WriteLine("#TEST - UpdateContract");
			var huis3 = new Huis("Huis3", 3, park);
			huis3 = _huizenManager.VoegNieuwHuisToe(huis3.Straat, huis3.Nr, huis3.Park);
			huurContract.ZetHuis(huis3);
			_contractenManager.UpdateContract(huurContract);
			HuurContract? updatedHuurContract = _contractenManager.GeefContract(huurContract.Id);

			Console.WriteLine("#TEST - GeefContract");
			if (updatedHuurContract is not null) {
				WriteValid("Geef contract");
			} else {
				WriteError("Geef contract [?]\n");
			}

			if (updatedHuurContract.Huis.Id == huurContract.Huis.Id) {
				WriteValid("HuurContract updaten");
			} else {
				WriteError("HuurContract updaten");
			}

			//Contract toevoegen met zelfde huis
			Console.WriteLine("#TEST - Contract toevoegen met zelfde huis");
			_contractenManager.MaakContract("Huur2", new HuurPeriode(DateTime.Now.AddDays(6), 5), huurContract.Huurder, _huizenManager.GeefHuis(huis3.Id));
			if (_huizenManager.GeefHuis(huis3.Id).Huurcontracten().Count() == 2) {
				WriteValid("HuurContract toevoegen met zelfde huis");
			} else {
				WriteError("HuurContract toevoegen met zelfde huis");
			}

			//Contract toevoegen die al bestaat
			Console.WriteLine("#TEST - Contract toevoegen maar bestaat al");
			try {
				_contractenManager.MaakContract(huurContract.Id, huurContract.Huurperiode, huurContract.Huurder, huurContract.Huis);
			} catch (BeheerderException ex) {
				if (ex.Message == "Contract bestaat al") {
					WriteValid("HuurContract bestaat al");
				} else {
					WriteError("HuurContract bestaat al");
				}
			}

			//Contracten voor een huis tonen
			Console.WriteLine("#TEST - Huizen.HuurContracten()");
			var huurContracten = _huizenManager.GeefHuis(huis3.Id).Huurcontracten();
			foreach (HuurContract huisHuurContract in huurContracten) {
				Console.WriteLine($"- {huisHuurContract}");
			}
			if (_huizenManager.GeefHuis(huis3.Id).Huurcontracten().Count() == 2) {
				WriteValid("Toon huurContracten met zelfde huis");
			} else {
				WriteError("Toon huurContracten met zelfde huis");
			}

			//Contract annuleren
			Console.WriteLine("#Test - AnnuleerContract");
			_contractenManager.AnnuleerContract(huurContract);
			if (_huizenManager.GeefHuis(huis3.Id).Huurcontracten().Count() == 1) {
				WriteValid("HuurContract annuleren");
			} else {
				WriteError("HuurContract annuleren");
			}
		}
	}
}