using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PitangBoosterVendas.Entity.Entities
{
    public abstract class IdEntity<T> : IEntity
    {
        public required T Id { get; set; }
    }
}
