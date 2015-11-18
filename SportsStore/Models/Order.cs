using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace SportsStore.Models
{
    public class Order  //attention to serialize model might appear circular references. solve this in webapiconfig.cs
    {
        [HttpBindNever]
        public int Id { get; set; }
        [Required]
        public string Customer { get; set; }
        [HttpBindNever]
        [Required]
        public decimal TotalCost { get; set; }  //customer cannot set the totalcost!use httpbindnever
        public ICollection<OrderLine> Lines { get; set; }
    }
}