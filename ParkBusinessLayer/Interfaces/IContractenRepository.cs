using ParkBusinessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkBusinessLayer.Interfaces
{
    public interface IContractenRepository
    {
        bool HeeftContract(DateTime startDatum, int huurderid, int huisid);
        void VoegContractToe(HuurContract contract);
        void AnnuleerContract(HuurContract contract);
        bool HeeftContract(string id);
        void UpdateContract(HuurContract contract);
        HuurContract GeefContract(string id);
        List<HuurContract> GeefContracten(DateTime dtBegin, DateTime? dtEinde);
    }
}
