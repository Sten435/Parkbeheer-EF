using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkDataLayer.DbModel {
	[Table("Huis")]
	public class HuisDb
    {
		public HuisDb() {
			
		}

		public HuisDb(int id, string? straat, int nr, bool actief) {
			Id = id;
			Straat = straat;
			Nr = nr;
			Actief = actief;
		}
		
		public HuisDb(int id, string? straat, int nr, bool actief, ParkDb parkDb) {
			Id = id;
			Straat = straat;
			Nr = nr;
			Actief = actief;
			Park = parkDb;
		}

		[Key]
		public int Id { get; set; }

		[MaxLength(250)]
		public string? Straat { get; set; }

		[Required]
		public int Nr { get; set; }

		[Required]
		public bool Actief { get; set; }

		[Required]
		public ParkDb Park { get; set; }

		public ICollection<HuurContractDb> HuurContracten { get; set; } = new List<HuurContractDb>();
	}
}