using ParkBusinessLayer.Exceptions;
using ParkBusinessLayer.Interfaces;
using ParkBusinessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkBusinessLayer.Beheerders
{
    public class BeheerHuurders
    {
        private IHuurderRepository repo;

        public BeheerHuurders(IHuurderRepository repo)
        {
            this.repo = repo;
        }

        public Huurder VoegNieuweHuurderToe(string naam, ContactGegevens contact)
        {
            try
            {
                if (repo.HeeftHuurder(naam, contact)) throw new BeheerderException("Huurder bestaat al");
                Huurder h = new Huurder(naam, contact);
                return repo.VoegHuurderToe(h);
            }
            catch(Exception ex)
            {
                throw new BeheerderException(ex.Message);
            }
        }
        public void UpdateHuurder(Huurder huurder)
        {
            try
            {
                if (!repo.HeeftHuurder(huurder.Id)) throw new BeheerderException("Huurder bestaat niet");
                repo.UpdateHuurder(huurder);
            }
            catch (Exception ex)
            {
				throw new BeheerderException(ex.Message);
			}
        }
        public Huurder GeefHuurder(int id)
        {
            try
            {
                return repo.GeefHuurder(id);
            }
            catch (Exception ex)
            {
                throw new BeheerderException(ex.Message);
            }
        }
        public List<Huurder> GeefHuurders(string naam)
        {
            try
            {
                return repo.GeefHuurders(naam);
            }
            catch (Exception ex)
            {
                throw new BeheerderException(ex.Message);
            }
        }
    }
}
