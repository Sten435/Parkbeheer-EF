using ParkBusinessLayer.Model;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkDataLayer.DbModel {
	[Table("HuurderContract")]
	public class HuurContractDb {
		public HuurContractDb() {

		}

		[Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [MaxLength(25)]
		public string Id { get; set; }

		[Required]
		public DateTime StartDatum { get; set; }

		[Required]
		public DateTime EindDatum { get; set; }

		[Required]
		public int Aantaldagen { get; set; }

		[Required]
		public HuurderDb Huurder { get; set; }

		[Required]
		public HuisDb Huis { get; set; }
	}
}
