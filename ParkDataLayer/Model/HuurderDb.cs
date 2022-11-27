using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkDataLayer.DbModel {
	[Table("Huurder")]
	public class HuurderDb
    {
		public HuurderDb() {
			
		}

		[Key]
		public int Id { get; set; }

		[MaxLength(100)]
		public string Naam { get; set; }

		[MaxLength(100)]
		public string? Email { get; set; }

		[MaxLength(100)]
		public string? Tel { get; set; }

		[MaxLength(100)]
		public string? Adres { get; set; }

		public override bool Equals(object obj) {
			return obj is HuurderDb db &&
				   Id == db.Id &&
				   Naam == db.Naam &&
				   Email == db.Email &&
				   Tel == db.Tel &&
				   Adres == db.Adres;
		}

		public override int GetHashCode() {
			return HashCode.Combine(Id, Naam, Email, Tel, Adres);
		}
	}
}