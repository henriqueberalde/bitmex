using System;

namespace BitMex
{
    public class TradeHistoryItem
    {
        public string ExecID { get; set; }
        public string OrderID { get; set; }
        public string ClOrdID { get; set; }
        public string ClOrdLinkID { get; set; }
        public int Account { get; set; }
        public string Symbol { get; set; }
        public string Side { get; set; }
        public decimal LastQty { get; set; }
        public decimal LastPx { get; set; }
        public string UnderlyingLastPx { get; set; }
        public string LastMkt { get; set; }
        public string LastLiquidityInd { get; set; }
        public string SimpleOrderQty { get; set; }
        public decimal OrderQty { get; set; }
        public decimal Price { get; set; }
        public string DisplayQty { get; set; }
        public decimal? StopPx { get; set; }
        public string PegOffsetValue { get; set; }
        public string PegPriceType { get; set; }
        public string Currency { get; set; }
        public string SettlCurrency { get; set; }
        public string ExecType { get; set; }
        public string OrdType { get; set; }
        public string TimeInForce { get; set; }
        public string ExecInst { get; set; }
        public string ContingencyType { get; set; }
        public string ExDestination { get; set; }
        public string OrdStatus { get; set; }
        public string Triggered { get; set; }
        public string WorkingIndicator { get; set; }
        public string OrdRejReason { get; set; }
        public string SimpleLeavesQty { get; set; }
        public decimal LeavesQty { get; set; }
        public string SimpleCumQty { get; set; }
        public decimal cumqty { get; set; }
        public decimal avgpx { get; set; }
        public decimal commission { get; set; }
        public string TradePublishIndicator { get; set; }
        public string MultiLegReportingType { get; set; }
        public string Text { get; set; }
        public string TrdMatchID { get; set; }
        public decimal ExecCost { get; set; }
        public decimal ExecComm { get; set; }
        public decimal HomeNotional { get; set; }
        public decimal ForeignNotional { get; set; }
        public DateTime transactTime { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
