using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestApp.DomainModel.Entity
{
    public class BankAccount
    {
        [Key]
        [Required]
        public string UserName { get; set; }

        [NotMapped]
        public string Password { get; set; }

        public double Balance { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }

    }
}
