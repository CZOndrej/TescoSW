using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TescoSW
{
    class WeekendSale
    {
        public string ModelName { get; private set; }
        public double Price { get; set; }
        public double TotalPrice { get; set; }

        public WeekendSale(string modelName)
        {
            ModelName = modelName;
            Price = 0;
            TotalPrice = 0;
        }
    }
}
