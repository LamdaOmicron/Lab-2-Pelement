using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2OPP.Models
{
    public class Product
    {
        public int Id { get; set; } // ID

        [Required]
        public string Name { get; set; } = string.Empty; // Название

        public string Description { get; set; } = string.Empty; // Описание

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; } // Цена

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; } // Количество

        public string Category { get; set; } = string.Empty; // Категория

        // 1 товар -> может встречаться во многих позициях заказа
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
