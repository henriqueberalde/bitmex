using System;
using BitMEX;
using System.Collections.Generic;
using System.Linq;

namespace BitMex
{
    class Program
    {

        private static string bitmexKey = "cMmdhQrPgkX4-ByfGSJPFZbs";
        private static string bitmexSecret = "UBByyE7pjnGbcYo93ZH9ZCvLk0IXjiJSnFKCkvkgrFJXQR9n";

        static void Main(string[] args)
        {
            Program p = new Program();
            p.Run(args);
        }

        private void Run(string[] args)
        {
            BitMEXApi bitmex = new BitMEXApi(bitmexKey, bitmexSecret);

            var trades = bitmex.GetUserWalletHistory(false);

            WalletHistoryItem lastTrade = null;
            for (int i = 0; i < trades.Count; i++)
            {
                if (lastTrade == null
                    || trades[i].transactType != "RealisedPNL")
                {
                    lastTrade = trades[i];
                    continue;
                }

                trades[i].PercentGain = (trades[i].Amount * 100) / lastTrade.WalletBalance;
                lastTrade = trades[i];
            }

            Show(trades);
            Console.Read();
        }

        private static void Show(List<WalletHistoryItem> trades)
        {
            Console.WriteLine("Trades");

            foreach (var t in trades)
            {
                Console.Write($"{t.TransactTime} - ");

                if (t.PercentGain != 0)
                    Console.ForegroundColor = t.PercentGain > 0 ? ConsoleColor.Green : ConsoleColor.Red;
                
                Console.Write($"{t.PercentGain.ToString("F2")}%\n");
                Console.ResetColor();
            }

            var sum = trades.Sum(t => t.Amount) / 100000000;
            var sumPercent = trades.Sum(t => t.PercentGain);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Total: XBT {sum.ToString("F8")}");
            Console.WriteLine($"Total Percent: {sumPercent.ToString("F2")}%");
            Console.ResetColor();
        }
    }
}