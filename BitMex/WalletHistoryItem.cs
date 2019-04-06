using System;

namespace BitMex
{
    public class WalletHistoryItem
    {
        public string TransactID { get; set; }
        public int Account { get; set; }
        public string Currency { get; set; }
        public string transactType { get; set; }
        public decimal Amount { get; set; }
        public decimal? Fee { get; set; }
        public string TransactStatus { get; set; }
        public string Address { get; set; }
        public string Tx { get; set; }
        public string Text { get; set; }
        public DateTime TransactTime { get; set; }
        public decimal WalletBalance { get; set; }
        public string marginBalance { get; set; }
        public DateTime Timestamp { get; set; }

        public decimal PercentGain { get; set; }
    }
}
