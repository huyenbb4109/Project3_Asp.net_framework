//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Project3_CookingRecipe.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class CookingRecipe
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CookingRecipe()
        {
            this.Comments = new HashSet<Comment>();
            this.DetailsEvents = new HashSet<DetailsEvent>();
        }
    
        public int IdRecipe { get; set; }

        [Required(ErrorMessage = "Name Food is required!")]
        [RegularExpression(@"\S+.*", ErrorMessage = "Name food must not contain a leading space!")]
        public string NameFood { get; set; }
        public string Img { get; set; }
        public string Describe { get; set; }

        [Required(ErrorMessage = "Ingredients is required!")]
        [RegularExpression(@"\S+.*", ErrorMessage = "Ingredients must not contain a leading space!")]
        public string Ingredients { get; set; }
        public int IdUser { get; set; }
        public int IdEvent { get; set; }
        public System.DateTime DateSubmit { get; set; }

        [Required(ErrorMessage = "Steps is required!")]
        //[RegularExpression(@"\S+.*", ErrorMessage = "Steps must not contain a leading space!")]
        [StringLength(65535, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 1)]
        public string Steps { get; set; }
        public bool Status { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual Event Event { get; set; }
        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetailsEvent> DetailsEvents { get; set; }
    }
}
