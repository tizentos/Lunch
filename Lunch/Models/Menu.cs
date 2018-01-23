using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lunch.Models
{
    public class Menu
    {
        public int  Id  { get; set; }
        public  virtual Food FirstChoice { get; set; }
      //  public int FirstChoiceId { get; set; }
        public virtual Food SecondChoice { get; set;}
        public virtual int FirstChoiceId { get; set; }
        public virtual int SecondChoiceId { get; set; }
                                           //   public int SecondChoiceId { get; set; }
        public DateTime Date { get; set; }

    }
    public class AddMenuViewModel
    {
        [Display(Name="First Meal")]
        public virtual int FirstChoiceId { get; set; }
        [Display(Name ="Second Meal")]
        public virtual int SecondChoiceId { get; set; }

    }
}