﻿using ParkBusinessLayer.Exceptions;
using System.Collections.Generic;

namespace ParkBusinessLayer.Model
{
    public class Huurder
    {
        public int Id { get; private set; }
        public string Naam { get; private set; }
        public ContactGegevens Contactgegevens { get; private set; }

        public Huurder(int id, string naam, ContactGegevens contactgegevens)
        {
            ZetId(id);
            ZetNaam(naam);
            ZetContactgegevens(contactgegevens);
        }
        public Huurder(string naam, ContactGegevens contactgegevens)
        {
            ZetNaam(naam);
            ZetContactgegevens(contactgegevens);
        }
        public void ZetId(int id)
        {
            if (id <= 0) throw new ParkException("huurder - zetid");
            Id = id;
        }
        public void ZetNaam(string naam)
        {
            if (string.IsNullOrWhiteSpace(naam)) throw new ParkException("huurder zetnaam");
            Naam = naam;
        }
        public void ZetContactgegevens(ContactGegevens contactgegevens)
        {
            if (contactgegevens == null) throw new ParkException("Huurder zetcontactgegevens");
            Contactgegevens = contactgegevens;
        }
    }
}