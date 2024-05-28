using Aardvark.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10th
{
    public interface ICountable
    {
        public int Count();

        public int Count(double mark);

        public int Percentage(double from, double to);
    }
}
