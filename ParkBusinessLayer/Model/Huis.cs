using ParkBusinessLayer.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkBusinessLayer.Model
{
    public class Huis
    {
        public int Id { get; private set; }
        public string Straat { get; private set; }
        public int Nr { get; private set; }
        public bool Actief { get; set; }
        public Park Park { get; private set; }
        private Dictionary<Huurder,List<HuurContract>> _huurcontracten = new  Dictionary<Huurder, List<HuurContract>>();

        public Huis(int id, string straat, int nr, bool actief, Park park, Dictionary<Huurder, List<HuurContract>> huurcontracten)
            : this(id, straat, nr, actief, park)
        {
            _huurcontracten = huurcontracten;
        }
        public Huis(string straat, int nr, Park park)
        {
            ZetStraat(straat);
            ZetNr(nr);
            Park = park;
            Actief = true;
        }
        public Huis(int id, string straat, int nr, bool actief, Park park) : this(straat, nr, park)
        {
            ZetId(id);
            Actief = actief;
        }

        public IReadOnlyList<HuurContract> Huurcontracten()
        {
            return _huurcontracten.Values.SelectMany(x=>x).ToList();
        }
        public void VoegHuurcontractToe(HuurContract huurcontract)
        {
            if (huurcontract == null) throw new ParkException("voeghuurcontracttoe");
            if (_huurcontracten.ContainsKey(huurcontract.Huurder))
            {
                if (_huurcontracten[huurcontract.Huurder].Contains(huurcontract)) throw new ParkException("voegcontracttoe");
                _huurcontracten[huurcontract.Huurder].Add(huurcontract);
            }
            else
            {
                _huurcontracten.Add(huurcontract.Huurder, new List<HuurContract>() { huurcontract});
            }
        }
        public void VerwijderHuurcontract(HuurContract huurcontract)
        {
            if (huurcontract == null) throw new ParkException("verwijderhuurcontract");           
            if (_huurcontracten.ContainsKey(huurcontract.Huurder))
            {
                if (!_huurcontracten[huurcontract.Huurder].Contains(huurcontract)) throw new ParkException("verwijderhuurcontract"); 
                _huurcontracten[huurcontract.Huurder].Remove(huurcontract);
            }
            else
            {
                throw new ParkException("verwijderhuurcontract");
            }
        }
        public IReadOnlyList<HuurContract> Huurcontracten(Huurder huurder)
        {
            if (huurder==null) throw new ParkException("huurder is null");
            if (!_huurcontracten.ContainsKey(huurder)) throw new ParkException("huurder bestaat niet");
            return _huurcontracten[huurder].AsReadOnly();
        }
        public void ZetStraat(string straat)
        {
            if (string.IsNullOrEmpty(straat)) throw new ParkException("zetstraat");
            Straat = straat;
        }
        public void ZetNr(int nr)
        {
            if (nr <= 0) throw new ParkException("zetnr");
            Nr = nr;
        }
        public void ZetContracten(Dictionary<Huurder, List<HuurContract>> huurcontracten)
        {
            if (huurcontracten == null) throw new ParkException("zetcontracten");
            _huurcontracten = huurcontracten;
        }
        public void ZetId(int id)
        {
            if (id <= 0) throw new ParkException("zetid");
            Id = id;
        }
    }
}
