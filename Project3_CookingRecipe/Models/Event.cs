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
    using System.ComponentModel.DataAnnotations;

    public partial class Event
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Event()
        {
            this.Achievements = new HashSet<Achievement>();
            this.CookingRecipes = new HashSet<CookingRecipe>();
            this.DetailsEvents = new HashSet<DetailsEvent>();
        }
    
        public int IdEvent { get; set; }

        [Required(ErrorMessage = "Name Event is required!")]
        [RegularExpression(@"\S+.*", ErrorMessage = "Name Event must not contain a leading space!")]
        public string NameEvent { get; set; }
        public string Img { get; set; }

        [Required(ErrorMessage = "Number Of Participants is required!")]
        [Range(0, 255, ErrorMessage = "Number Of Participants must be within a range from 1 to 255! ")]
        public byte NumberOfParticipants { get; set; }

        [Required(ErrorMessage = "StartTime is required!")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime StartTime { get; set; }

        [Required(ErrorMessage = "EndTime is required!")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime EndTime { get; set; }
        public string Prize { get; set; }
        public bool Status { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Achievement> Achievements { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CookingRecipe> CookingRecipes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetailsEvent> DetailsEvents { get; set; }
    }
}
