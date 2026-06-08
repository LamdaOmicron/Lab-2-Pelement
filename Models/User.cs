using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2OPP.Models
{
    public class User
    {
        public int Id { get; set; }  // ID

        [Required]
        public string Name { get; set; } = string.Empty;  // Имя

        [Required]
        public string Email { get; set; } = string.Empty; // Email

        [Required]
        public string Password { get; set; } = string.Empty; // Пароль (в реальных проектах хранят хеш)

        // 1 пользователь -> много заказов
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
