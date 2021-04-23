using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TescoSW
{
    public class Car
    {   
        public string ModelName { get; private set; }
        public DateTime DateOfSale { get; private set; }
        public double Price { get; private set; }
        public double VAT { get; private set; }

        public Car(string modelName, DateTime dateOfSale, double price, double vat)
        {
            ModelName = modelName;
            DateOfSale = dateOfSale;
            Price = price;
            VAT = vat;
        }
    }
}
