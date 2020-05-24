using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StatauIrPristatau.Models
{
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Užsakymo numeris")]
        [Column("order_id")]
        [Key]
        public int Id { get; set; }
        [DisplayName("Užsakymo data")]
        public DateTime Date { get; set; }
        [DisplayName("Užsakymo būsena")]
        [Column("state")]
        public int State { get; set; }
        [DisplayName("Viso užsakymo dydis")]
        [Column("total_size")]
        public double TotalSize { get; set; }
        [DisplayName("Užsakymo kaina")]
        [Column("total_price")]
        public double TotalPrice { get; set; }
        [DisplayName("Užsakymo kaina")]
        [Column("total_weigth")]
        public double TotalWeigth { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Parcel> Parcels { get; set; }
    }
}