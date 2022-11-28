using System;

namespace ParkBusinessLayer.Model
{
    public class ContactGegevens
    {
        public ContactGegevens(string email, string tel, string adres)
        {
            Email = email;
            Tel = tel;
            Adres = adres;
        }
        public string Email { get; set; }
        public string Tel { get; set; }
        public string Adres { get; set; }

		public override bool Equals(object obj) {
			return obj is ContactGegevens gegevens &&
				   Email == gegevens.Email &&
				   Tel == gegevens.Tel &&
				   Adres == gegevens.Adres;
		}

		public override int GetHashCode() {
			return HashCode.Combine(Email, Tel, Adres);
		}
	}
}