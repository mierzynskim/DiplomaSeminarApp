using DiplomaSeminar.Core.Interfaces;
using SQLite.Net.Attributes;

namespace DiplomaSeminar.Core.BusinessLayer.Contracts
{
    public abstract class BusinessEntityBase : IBusinessEntity
    {
        public BusinessEntityBase()
        {
        }

        /// <summary>
        /// Gets or sets the Database ID.
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }



    }
}

