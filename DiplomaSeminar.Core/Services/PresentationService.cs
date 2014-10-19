using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomaSeminar.Core.DataLayer;
using DiplomaSeminar.Core.Interfaces;
using DiplomaSeminar.Core.Model;
using SQLite.Net;

namespace DiplomaSeminar.Core.Services
{
    public class PresentationService: IPresentationService
    {
        private readonly PresentationDatabase database;

        public PresentationService(SQLiteConnection connection)
        {
            database = new PresentationDatabase(connection);
        }

        public Task<Presentation> GetPresentation(int id)
        {
            return Task.Factory.StartNew(() => database.GetItem<Presentation>(id));
        }

        public Task<IEnumerable<Presentation>> GetPresentations()
        {
            return Task.Factory.StartNew(() => database.GetItems<Presentation>());
        }

        public async Task<Presentation> SavePresentation(Presentation presentation)
        {
            return await Task.Factory.StartNew(() =>
            {
                var id = database.SaveItem(presentation);
                presentation.Id = id;
                return presentation;
            });
        }

        public async Task DeletePresentation(int id)
        {
            await Task.Factory.StartNew(() =>
            {
                database.DeleteItem<Presentation>(id);
            });
        }
    }
}
