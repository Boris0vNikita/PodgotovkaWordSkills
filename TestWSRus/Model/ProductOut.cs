using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWSRus.Model
{
    public class ProductOut
    {
        public ProductOut(string name, int quantity, int iD)
        {
            Name = name;
            Quantity = quantity;
            ID = iD;
        }

        public string Name { get; set; }
        public int Quantity { get; set; }
        public int ID { get; set; }
        public override string ToString()
        {
            return Name.ToString();
        }

    }
}
