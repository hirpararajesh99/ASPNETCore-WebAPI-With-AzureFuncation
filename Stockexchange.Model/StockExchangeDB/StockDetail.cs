using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stockexchange.Model.StockExchangeDB
{
    public partial class StockDetail
    {
        [Key]
        public long StockdetailId { get; set; }
        [StringLength(50)]
        public string? TickerSymbol { get; set; }
        [StringLength(50)]
        public string? CompanyAliasName { get; set; }
        public long? Stock { get; set; }
        [Column(TypeName = "decimal(8, 2)")]
        public decimal? CurrentPrice { get; set; }
    }
}
