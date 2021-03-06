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

    public partial class Users
    {
        public int UserID { get; set; }

        
        public string FirstName { get; set; }

        
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string UserPassword { get; set; }

      
        public int RoleID { get; set; }

       
        public int StatusID { get; set; }
    
        public virtual RoleType RoleType { get; set; }
        public virtual UserStatus UserStatus { get; set; }
    }
}
