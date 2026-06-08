using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2OPP.Models
{
    public class OrderItem
    {
        public int Id { get; set; } // ID

        // Принадлежит заказу
        public int OrderId { get; set; }
        public Order? Order { get; set; }

        // Ссылается на товар
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; } // Количество
    }
}