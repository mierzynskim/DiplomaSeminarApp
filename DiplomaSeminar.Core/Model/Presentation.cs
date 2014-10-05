using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomaSeminar.Core.BusinessLayer.Contracts;
using DiplomaSeminar.Core.Interfaces;

namespace DiplomaSeminar.Core.Model
{
    public class Presentation : BusinessEntityBase
    {
        public String SpeakerName { get; set; }
        public String SpeakerLastName { get; set; }
        public String Subject { get; set; }
        public DateTime Date { get; set; }
    }
}
