//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace foodcorner.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PassOrder
    {
        public int ChefId { get; set; }
        public int OrderId { get; set; }
        public int ItemId { get; set; }
        public string Status { get; set; }
        public double PreparationTime { get; set; }
    
        public virtual Chef Chef { get; set; }
        public virtual ItemsDetail ItemsDetail { get; set; }
    }
}
