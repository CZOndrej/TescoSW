using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TescoSW
{
    class Manager
    {
        public ObservableCollection<Car> Cars { get; set; }
        public ObservableCollection<WeekendSale> WeekendSales { get; set; }


        public Manager()
        {
            Cars = new ObservableCollection<Car>();
            WeekendSales = new ObservableCollection<WeekendSale>();
        }

        public void LoadXml (string path)
        {
            ValidateFile(path);

            string element = "";
            string modelName = "";
            DateTime dateOfSale = DateTime.MinValue;
            double price = 0;
            double vat = 0;

            if (Cars.Count > 0)
            {
                Cars.Clear();
            }

            using (XmlReader xmlReader = XmlReader.Create(path))
            {
                
                while (xmlReader.Read())
                {
                    if (xmlReader.NodeType == XmlNodeType.Element)
                    {
                        element = xmlReader.Name;
                    }
                    else if (xmlReader.NodeType == XmlNodeType.Text)
                    {
                        switch (element)
                        {
                            case "modelName":
                                modelName = xmlReader.Value;
                                break;
                            case "dateOfSale":
                                dateOfSale = DateTime.Parse(xmlReader.Value);
                                break;
                            case "price":
                                price = double.Parse(xmlReader.Value);
                                break;
                            case "vat":
                                vat = double.Parse(xmlReader.Value);
                                break;
                        }
                    }
                    else if (xmlReader.NodeType == XmlNodeType.EndElement && xmlReader.Name == "car")
                    {
                        Cars.Add(new Car(modelName, dateOfSale, price, vat));
                    }
                }
                SetWeekendSales();
            }
        }

        public void SetWeekendSales()
        {
            foreach (Car car in Cars)
            {
                if (!HasModel(WeekendSales, car.ModelName))
                {
                    WeekendSales.Add(new WeekendSale(car.ModelName));
                    AddPrice(WeekendSales.Last());
                }
            }
        }


        
        private bool HasModel(ObservableCollection<WeekendSale> collection, string modelName)
        {
            foreach (WeekendSale sale in collection)
            {
                if (modelName == sale.ModelName)
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsWeekend(DateTime date)
        {
            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
            {
                return true;
            }
            return false;
        }

        private void AddPrice(WeekendSale sale)
        {
            foreach (Car car in Cars)
            {
                if (sale.ModelName == car.ModelName && IsWeekend(car.DateOfSale))
                {
                    sale.Price += car.Price;
                    sale.TotalPrice += car.Price + ((car.VAT * car.Price) / 100);
                }
            }
        }

        private void ValidateFile(string path)
        {
            if(!File.Exists(path))
            {
                throw new FileNotFoundException("Zadaný soubor neexistuje!", path);
            }
            else if (Path.GetExtension(path) != ".xml")
            {
                throw new FileFormatException(new Uri(path), "Špatný typ souboru.");
            }
        }
    }
}
