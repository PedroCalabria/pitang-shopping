using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PitangBoosterVendas.Utils.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class TransactionRequiredAttribute : Attribute
    {
        public IsolationLevel IsolationLevel { get; set; } = IsolationLevel.ReadCommitted;

        public TransactionRequiredAttribute() { }

        public TransactionRequiredAttribute(IsolationLevel isolationLevel)
        {
            IsolationLevel = isolationLevel;
        }
    }
}
