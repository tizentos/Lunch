using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Lunch.Models
{
    public class Food
    {
        public int Id { get; set; }
        [Display(Name ="Meal", Description ="Enter the meal")]
        public string Name { get; set; }
        public DateTime Date { get; set; }
        [InverseProperty("FirstChoice")]
        public virtual ICollection<Menu> FirstChoices { get; set; }
        [InverseProperty("SecondChoice")]
        public virtual ICollection<Menu> SecondChoices { get; set; }
    }
    public class AddFoodViewModel
    {
        [Display(Name = "Meal", Description = "Enter the meal")]
        [StringLength(50,ErrorMessage ="Text is too long")]
        public string Name { get; set; }
    }
}