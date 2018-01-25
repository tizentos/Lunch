using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lunch.Models
{
    public class Booking
    {
        public int Id { get; set; }
        [Display(Name = "Food Booked")]
        public virtual Food FoodChoice { get; set; }
        [Required]
        public virtual int FoodChoiceId { get; set; }
        public ApplicationUser User { get; set; }
        public DateTime Date { get; set; }
        public string DeliveryLocation { get; set; }
        public string CustomMessage { get; set; }
    }
    public class AddBookingViewModel
    {
        [Required]
        [Display(Name ="Food Booked")]
        public int FoodChoiceId { get; set; }
        [Required]
        [Display(Name ="Delivery Location")]
        public string  DeliveryLocation { get; set; }
        [Display(Name ="Additional Message")]
        public string  CustomMessage { get; set; }
    }
}