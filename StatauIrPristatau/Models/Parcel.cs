using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StatauIrPristatau.Models
{
    public class Parcel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("Siuntos numeris")]
        [Column("parcel_id")]
        [Key]
        public int Parcel_Id { get; set; }
        [DisplayName("Siuntos pristatymo kaina")]
        [Column("price")]
        [DefaultValue(0)]
        public double Price { get; set; }
        [Required(ErrorMessage = "Siuntos paėmimo adresas yra būtinas laukas!")]
        [DisplayName("Siuntos paėmimo adresas")]
        [Column("pickup_address")]
        public string Pickup_Address { get; set; }
        [Required(ErrorMessage = "Siuntos pristatymo adresas yra būtinas laukas!")]
        [DisplayName("Siuntos pristatymo adresas")]
        [Column("delivery_address")]
        public string DeliveryAddress { get; set; }
        [DisplayName("Siuntos būsena")]
        [Column("state")]
        [DefaultValue(0)]
        public int State { get; set; }
        [DisplayName("Plotis")]
        [Column("width")]
        [DefaultValue(0)]
        [Required(ErrorMessage = "Siuntos matmenys yra būtini!")]
        public double Width { get; set; }
        [DisplayName("Aukštis")]
        [Column("height")]
        [DefaultValue(0)]
        [Required(ErrorMessage = "Siuntos matmenys yra būtini!")]
        public double Height { get; set; }
        [DisplayName("Ilgis")]
        [Column("length")]
        [DefaultValue(0)]
        [Required(ErrorMessage = "Siuntos matmenys yra būtini!")]
        public double Length { get; set; }
        [DisplayName("Svoris")]
        [Column("weigth")]
        [DefaultValue(0)]
        [Required(ErrorMessage = "Siuntos svoris yra būtinas!")]
        public double Weigth { get; set; }
        [DisplayName("Ar siunta dūžtanti")]
        [Column("scattering_flag")]
        [DefaultValue(false)]
        public bool Scattering { get; set; }
        [DisplayName("Automobilio tipas")]
        [Column("car")]
        public string Car { get; set; }
        [DisplayName("Siuntos dydis ")]
        [Column("size")]
        [DefaultValue("S")]
        public string Size { get; set; }

    }
}