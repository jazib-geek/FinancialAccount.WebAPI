using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2Soft.Domain.Entities
{
    public sealed class FinancialAccount
    {
        [Key]
        public int Id { get; set; }
        public string? AccountNumber { get; set; }
        public string? AccountType { get; set; }
        public decimal? Balance { get; set; }

        public int? OwnerId { get; set; }
        public User? Owner { get; set; }
    }
}
