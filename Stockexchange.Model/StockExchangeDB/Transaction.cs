using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stockexchange.Model.StockExchangeDB
{
    [Table("Transaction")]
    public partial class Transaction
    {
        [Key]
        [Column("TransactionID")]
        public long TransactionId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? TransactionDateTime { get; set; }
        public long? UserId { get; set; }
        [StringLength(20)]
        public string? TransactionType { get; set; }
        [Column(TypeName = "decimal(8, 2)")]
        public decimal? TransactionPrice { get; set; }
    }
}
