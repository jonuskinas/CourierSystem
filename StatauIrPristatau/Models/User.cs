using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace StatauIrPristatau.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage ="Vardas yra būtinas laukas!")]
        [DisplayName("Vardas")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Pavardė yra būtinas laukas!")]
        [DisplayName("Pavardė")]
        public string Surname { get; set; }
        [DisplayName("E-pašto adresas")]
        [Required(ErrorMessage = "E-paštas yra būtinas laukas!")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
        [Key]
        public string Email { get; set; }
        [DisplayName("Slaptažodis")]
        [Required(ErrorMessage = "Slaptažodis būtinas!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DisplayName("Pakartokite slaptažodį")]
        [Compare("Password", ErrorMessage ="Prašome patvirtinti jūsų slaptažodį")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [DisplayName("Gimimo data")]
        [Required(ErrorMessage = "Gimimo data būtina!")]
        public DateTime YearOfBirth { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayName("Asmens kodas")]
        [Required(ErrorMessage = "Asmens kodas būtinas!")]
        public string PersonalCode { get; set; }
        [DisplayName("Adresas")]
        [Required(ErrorMessage = "Adresas būtinas!")]
        public string Address { get; set; }

        
    }
}