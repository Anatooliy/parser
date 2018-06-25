using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Parser.Models
{
    public class Product
    {        
        public int Id { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; }

        [Display(Name = "Автор")]
        public string Author { get; set; }

        [Display(Name = "Описаниe")]
        public string Description { get; set; }

        [Display(Name = "Цена")]
        public int Price { get; set; }

        public string ImgUrl { get; set; }

        public string ImgName { get; set; }
    }
}