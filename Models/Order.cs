using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2OPP.Models
{
    public class Order
    {
        public int Id { get; set; } // ID

        // Владелец заказа (пользователь)
        public int UserId { get; set; }
        public User? User { get; set; }

        public DateTime Date { get; set; } = DateTime.Now; // Дата

        [Required]
        public string Status { get; set; } = "Новый"; // Статус

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; } // Количество (если хочешь хранить общее кол-во)

        [Required]
        public string DeliveryAddress { get; set; } = string.Empty; // Адрес доставки

        // 1 заказ -> много товаров в заказе
        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}