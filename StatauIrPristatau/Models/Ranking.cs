using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StatauIrPristatau.Models
{
    public class Ranking
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Ivertinimo_id")]
        [Column("ranking_id")]
        [Key]
        public int Ranking_Id { get; set; }
        [DisplayName("Ivertinimas")]
        [Column("overall_star")]
        public int Overall { get; set; }
        [DisplayName("Komentaras")]
        [Column("comment")]
        public string Comment { get; set; }
        [DisplayName("Kurjerio_ivertinimas")]
        [Column("courier_ranking")]
        public int CourierRanking { get; set; }
        [DisplayName("Siuntos_kokybe")]
        [Column("parcel_quality")]
        public int ParcelQuality { get; set; }
        [DisplayName("Pristatymo_laukas")]
        [Column("delivery_time")]
        public double DeliveryTime { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<User> Userss { get; set; }

    }
}