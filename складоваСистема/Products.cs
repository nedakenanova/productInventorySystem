using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace складоваСистема
{
    public class Products
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price {  get; set; }

        public int Quantity {  get; set; }
        public string Category {  get; set; }
        public Products(int id, string name, double price, int quantity,string c)
        {
            Id = id;
            Name = name;
            Price = price;
            Quantity = quantity;
                
            Category = c;
        }
        public override string ToString()
        {
            return $"id = {Id}, name = {Name}, price = {Price:f2}, quantity = {Quantity}, category = {Category}";
        }
    }
}
