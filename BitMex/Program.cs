using System;
using BitMEX;
using System.Collections.Generic;
using System.Linq;

namespace BitMex
{
    public class Program
    {
        private static string bitmexKey = Environment.GetEnvironmentVariable("bitmex-apiKey");
        private static string bitmexSecret = Environment.GetEnvironmentVariable("bitmex-apiSecret");

        static void Main(string[] args)
        {
            Program p = new Program();
            p.Run(args);
        }

        private void Run(string[] args)
        {
            BitMEXApi bitmex = new BitMEXApi(bitmexKey, bitmexSecret);
            var result = bitmex.GetExecutionTradeHistory();

            //var walletHistory = bitmex.GetUserWalletHistory(false);

            //WalletHistoryItem lastTrade = null;
            //for (int i = 0; i < walletHistory.Count; i++)
            //{
            //    if (lastTrade == null
            //        || walletHistory[i].transactType != "RealisedPNL")
            //    {
            //        lastTrade = walletHistory[i];
            //        continue;
            //    }

            //    walletHistory[i].PercentGain = (walletHistory[i].Amount * 100) / lastTrade.WalletBalance;
            //    lastTrade = walletHistory[i];
            //}

            //Show(walletHistory);
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