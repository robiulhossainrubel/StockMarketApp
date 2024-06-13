using System.ComponentModel.DataAnnotations;

namespace StockMarketApp.Models
{
    public class StockMarket
    {
        [Key]
        public int Id { get; set; }
        public DateOnly Date { get; set; }  
        public string Trade_Code {  get; set; }
        public double High {  get; set; }
        public double Low { get; set; }
        public double Open {  get; set; }
        public double Close { get; set; }
        public string Volume { get; set; }
    }
}
