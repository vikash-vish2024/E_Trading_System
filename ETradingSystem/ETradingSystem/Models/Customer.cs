//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ETradingSystem.Models
{
    public partial class Customer
    {
        [Key] // Specify the primary key
        public decimal Customer_Id { get; set; }

        [Required(ErrorMessage = "Customer Name is required")]
        public string Customer_Name { get; set; }

        [Required(ErrorMessage = "Customer Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Invalid Email Address")]
        public string Customer_Email { get; set; }

        [Required(ErrorMessage = "Date of Birth is required")]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime Date_Of_Birth { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [Display(Name = "Balance")]
        [Range(0, double.MaxValue, ErrorMessage = "Balance must be a positive number")]
        public double? Balance { get; set; }

        [Display(Name = "Mobile Number")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid Mobile Number")]
        public decimal? Mobile_Number { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; set; }

        [Display(Name = "Hint")]
        public Nullable<int> Hint_Id { get; set; }

        [Display(Name = "Hint Answer")]
        [Required(ErrorMessage = "Hint Answer is required")]
        public string Hint_Answer { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            this.BucketLists = new HashSet<BucketList>();
            this.Orders = new HashSet<Order>();
            this.Wallets = new HashSet<Wallet>();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BucketList> BucketLists { get; set; }
        public virtual Hint Hint { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Wallet> Wallets { get; set; }
    }
}

