using DiplomaSeminar.Core.Interfaces;
using SQLite.Net.Attributes;

namespace DiplomaSeminar.Core.BusinessLayer.Contracts
{
    public abstract class BusinessEntityBase : IBusinessEntity
    {
        public BusinessEntityBase()
        {
        }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }



    }
}

