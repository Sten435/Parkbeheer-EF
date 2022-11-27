using ParkBusinessLayer.Beheerders;
using ParkBusinessLayer.Interfaces;
using ParkBusinessLayer.Model;
using ParkDataLayer.Context;
using ParkDataLayer.Repositories;

namespace CUI {
	public class Program {
		//private static readonly string _connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
		private static readonly string _connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=ParkingBeheer;Integrated Security=True";
		private static readonly IHuizenRepository _huizenRepo = new HuizenRepositoryEF(_connectionString);
		private static readonly IContractenRepository _contractenRepo = new ContractenRepositoryEF(_connectionString);
		private static readonly IHuurderRepository _huurderRepo = new HuurderRepositoryEF(_connectionString);

		private static readonly BeheerHuizen _huizenManager = new(_huizenRepo);
		private static readonly BeheerContracten _contractenManager = new(_contractenRepo);
		private static readonly BeheerHuurders _huurderManager = new(_huurderRepo);

		static void Main(string[] args) {
			DatabaseContext.Instance.Database.EnsureDeleted();
			DatabaseContext.Instance.Database.EnsureCreated();

			Park park = new Park("Park1", "1", "1");
			Park park2 = new Park("Park2", "2", "2");
			Huis huis = new Huis("Nieuwstraat", 2, park);

			#region Voeg Nieuw Huis Toe
			huis = _huizenManager.VoegNieuwHuisToe(huis.Straat, huis.Nr, huis.Park);
			#endregion

			#region Update Huis
			Huis updatedHuis = _huizenManager.GeefHuis(huis.Id);
			string updatedStraat = Guid.NewGuid().ToString().Substring(15);
			
			updatedHuis.ZetStraat(updatedStraat);
			
			_huizenManager.UpdateHuis(updatedHuis);
			Console.WriteLine($"Nieuwe straat: {updatedStraat} == huis straat: {updatedHuis.Straat}");
			#endregion

			#region Archiveer Huis Error
			//huis = new Huis("huis1", 2, park);

			//var insertedHuis = _huizenManager.VoegNieuwHuisToe(huis.Straat, huis.Nr, huis.Park);
			//Console.WriteLine($"Actief: {insertedHuis.Straat} ({insertedHuis.Id}) -> {_huizenManager.GeefHuis(insertedHuis.Id).Actief}");
			//Console.WriteLine($"Achiveer huis: {insertedHuis.Straat} ({insertedHuis.Id})");
			//_huizenManager.ArchiveerHuis(insertedHuis);
			//Console.WriteLine($"Actief: {insertedHuis.Straat} ({insertedHuis.Id}) -> {_huizenManager.GeefHuis(insertedHuis.Id).Actief}");
			#endregion
		}
	}
}