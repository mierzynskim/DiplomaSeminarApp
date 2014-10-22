using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiplomaSeminar.Core.BusinessLayer.Model;
using DiplomaSeminar.Core.Interfaces;
using SQLite.Net;

namespace DiplomaSeminar.Core.DataLayer
{
    public class PresentationDatabase
    {
        static readonly object Locker = new object();

        private readonly SQLiteConnection database;

        public PresentationDatabase(SQLiteConnection database)
        {
            this.database = database;
            database.CreateTable<Presentation>();
        }

        public IEnumerable<T> GetItems<T>() where T : IBusinessEntity, new()
        {
            lock (Locker)
            {
                var items = (from i in database.Table<T>() select i).ToList();
                return items;
            }
        }

        public T GetItem<T>(int id) where T : IBusinessEntity, new()
        {
            lock (Locker)
            {
                return database.Table<T>().FirstOrDefault(x => x.Id == id);
            }
        }

        public int SaveItem<T>(T item) where T : IBusinessEntity
        {
            lock (Locker)
            {
                if (item.Id != 0)
                {
                    database.Update(item);
                    return item.Id;
                }
                else
                {
                    return database.Insert(item);
                }
            }
        }

        public int DeleteItem<T>(int id) where T : IBusinessEntity, new()
        {
            lock (Locker)
            {
                return database.Delete<T>(id);
            }
        }       
    }
}
