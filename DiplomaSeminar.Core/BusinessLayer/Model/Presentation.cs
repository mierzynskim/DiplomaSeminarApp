using System;
using DiplomaSeminar.Core.BusinessLayer.Contracts;

namespace DiplomaSeminar.Core.BusinessLayer.Model
{
    public class Presentation : BusinessEntityBase
    {
        public String SpeakerName { get; set; }
        public String SpeakerLastName { get; set; }
        public String Subject { get; set; }
        public DateTime Date { get; set; }
    }
}
