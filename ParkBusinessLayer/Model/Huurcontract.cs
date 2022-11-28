﻿using ParkBusinessLayer.Exceptions;
using System;
using System.Collections.Generic;

namespace ParkBusinessLayer.Model
{
    public class HuurContract
    {
        public HuurContract(string id, HuurPeriode huurperiode, Huurder huurder, Huis huis)
        {
            ZetId(id);
            ZetHuurperiode(huurperiode);
            ZetHuurder(huurder);
            ZetHuis(huis);
        }
        public string Id { get; private set; }
        public HuurPeriode Huurperiode { get; private set; }
        public Huurder Huurder { get; private set; }
        public Huis Huis { get; private set; }
        public void ZetId(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new ParkException("Park zetid");
            Id = id;
        }
        public void ZetHuis(Huis huis)
        {
            if (huis == null) throw new ParkException("contract zethuis");
            Huis = huis;
        }
        public void ZetHuurperiode(HuurPeriode huurperiode)
        {
            if (huurperiode == null) throw new ParkException("contract zethuurperiode");
            Huurperiode = huurperiode;
        }
        public void ZetHuurder(Huurder huurder)
        {
            if (huurder == null) throw new ParkException("contract zethuurder");
            Huurder = huurder;
        }
        public override bool Equals(object obj)
        {
            return obj is HuurContract huurcontract &&
                   EqualityComparer<HuurPeriode>.Default.Equals(Huurperiode, huurcontract.Huurperiode) &&
                   EqualityComparer<Huurder>.Default.Equals(Huurder, huurcontract.Huurder) &&
                   EqualityComparer<Huis>.Default.Equals(Huis, huurcontract.Huis);
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Huurperiode, Huurder, Huis);
        }

		public override string ToString() {
			return $"{Huurperiode.StartDatum} - {Huurperiode.EindDatum} - {Huurder.Naam} - Huis: ({Huis.Id})";
		}
	}
}
