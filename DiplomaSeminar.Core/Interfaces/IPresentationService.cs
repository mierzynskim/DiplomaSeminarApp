using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomaSeminar.Core.Model;

namespace DiplomaSeminar.Core.Interfaces
{
    public interface IPresentationService
    {
        Task<Presentation> GetPresentation(int id);
        Task<IEnumerable<Presentation>> GetPresentations();
        Task<Presentation> SavePresentation(Presentation presentation);
        Task<Presentation> DeletePresentation(int id);
    }
}
