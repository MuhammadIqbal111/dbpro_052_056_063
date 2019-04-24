using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Linq;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace foodcorner.Models
{
    public class ItemsDetails
    {
        public DB22Entities3 db = new DB22Entities3();
        

        public int ItemId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public Nullable<int> Quantity { get; set; }
        public string Image { get; set; }
        
        public IEnumerable<ItemsDetail> doctorsspec(int spec)
        {
            var ide = db.ItemsDetails.Where(p => (p.CategoryId == spec));
            return ide;
        }
    }
}