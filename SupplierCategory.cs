
namespace foodcorner.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using System.Linq;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class SupplierCategory
    {
        public DB22Entities3 db = new DB22Entities3();
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SupplierCategory()
        {
            this.SupplierItems = new HashSet<SupplierItem>();
        }
    
        public int CatId { get; set; }
        public string Name { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SupplierItem> SupplierItems { get; set; }
        public IEnumerable<SupplierCategory> doctorsspec(int spec)
        {
            var ide = db.SupplierCategories.Where(p => (p.CatId == spec));
            return ide;
        }
    }
}