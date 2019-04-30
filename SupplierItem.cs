
namespace foodcorner.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web;

    public partial class SupplierItem
    {
        public DB22Entities3 db = new DB22Entities3();
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SupplierItem()
        {
            this.AdminOrderDetails = new HashSet<AdminOrderDetail>();
        }
    
        public int ItemId { get; set; }
        public int CatId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AdminOrderDetail> AdminOrderDetails { get; set; }
        public virtual SupplierCategory SupplierCategory { get; set; }
        public IEnumerable<SupplierItem> doctorsspec(int spec)
        {
            var ide = db.SupplierItems.Where(p => (p.CatId == spec));
            return ide;
        }
    }
}