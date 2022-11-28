using ParkBusinessLayer.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkDataLayer.DbModel {
	[Table("Park")]
	public class ParkDb {
		public ParkDb() {
			
		}

		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[MaxLength(20)]
		public string? Id { get; set; } = null;

		[Required]
		[MaxLength(250)]
		public string Naam { get; set; }

		[MaxLength(500)]
		public string? Locatie { get; set; }

		public ICollection<HuisDb> Huizen { get; set; } = new List<HuisDb>();
	}
}
