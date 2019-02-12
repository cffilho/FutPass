//------------------------------------------------------------------------------
// <auto-generated>
//    O código foi gerado a partir de um modelo.
//
//    Alterações manuais neste arquivo podem provocar comportamento inesperado no aplicativo.
//    Alterações manuais neste arquivo serão substituídas se o código for gerado novamente.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Teste_de_aplicação.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Manager
    {
        public int ManagerID { get; set; }

        [Required]
        [Display(Name = "First Name:")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name:")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Full Name:")]
        public string NickName { get; set; }

        [Required]
        [Display(Name = "Date Of Birth:")]
        [RegularExpression(@"[0-9]{2}[\/]?[0-9]{2}[\/]?[0-9]{4}", ErrorMessage = "Invalid Date")]
        public string DateOfBirth { get; set; }

        [Display(Name = "Country:")]
        public int CountryID { get; set; }

        [Display(Name = "Club:")]
        public int ClubID { get; set; }

        [Display(Name = "Photo:")]
        public byte[] Photo { get; set; }

        [Display(Name = "Club:")]
        public virtual Club Club { get; set; }

        [Display(Name = "Country:")]
        public virtual Country Country { get; set; }

        [NotMapped]
        public int Age { get; set; }

        [NotMapped]
        public string GetImage { get; set; }

        [NotMapped]
        public string GetFlagCountry { get; set; }

    }
}
