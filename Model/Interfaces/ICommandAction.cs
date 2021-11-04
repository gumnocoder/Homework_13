using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_13.Model.Interfaces
{
    interface ICommandAction
    {
        void Execute();
        public bool Executed { get; }
    }
}
