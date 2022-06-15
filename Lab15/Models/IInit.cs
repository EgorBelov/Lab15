using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB_15.Models
{
    internal interface IInit
    {
        int Age { get; set; }
        void Init();
    }
}