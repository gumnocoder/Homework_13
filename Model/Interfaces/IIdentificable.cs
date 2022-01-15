using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_13.Model.Interfaces
{
    interface IIdentificable
    {
        long ID { get; set; }

        void SetID();
    }
}
