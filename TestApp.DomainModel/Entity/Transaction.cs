

using System.ComponentModel.DataAnnotations;
using TestApp.Utils.enums;

namespace TestApp.DomainModel.Entity
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public double Amount { get; set; }

        [Required]
        public TransactionType Type { get; set; }

        [Required]
        public double BalanceAfterTransaction { get; set; }

    }
}
