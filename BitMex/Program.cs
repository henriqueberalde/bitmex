using System;
using BitMEX;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading;
using Google.Apis.Sheets.v4;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Util.Store;
using Google.Apis.Services;
using Google.Apis.Sheets.v4.Data;

namespace BitMex
{
    public class Program
    {
        private static string bitmexKey = "w5vzkWezq3LeL3XDeJ_LSR_c";
        private static string bitmexSecret = "js2eVaZRghZZ1ZIZpvWGx4AKrKTQ1OV_5YQhLXamyzu2WXxg";

        static void Main(string[] args)
        {
            Program p = new Program();
            p.Run(args);

            //TestGoopgleSheets();
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

        private static void TestGoopgleSheets()
        {
            // If modifying these scopes, delete your previously saved credentials
            // at ~/.credentials/sheets.googleapis.com-dotnet-quickstart.json
            string[] Scopes = { SheetsService.Scope.SpreadsheetsReadonly };
            string ApplicationName = "Google Sheets API .NET Quickstart";
            UserCredential credential;

            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Google Sheets API service.
            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Define request parameters.
            var spreadsheetId = "1zpnsYoItPsxa7fpDMcRsCx9nJBJZFre-GaqBq9DTHuQ";
            var range = "Geral!A4:F17";
            SpreadsheetsResource.ValuesResource.GetRequest request =
                    service.Spreadsheets.Values.Get(spreadsheetId, range);

            // Prints the names and majors of students in a sample spreadsheet:
            // https://docs.google.com/spreadsheets/d/1BxiMVs0XRA5nFMdKvBdBZjgmUUqptlbs74OgvE2upms/edit
            ValueRange response = request.Execute();
            IList<IList<Object>> values = response.Values;
            if (values != null && values.Count > 0)
            {
                Console.WriteLine("Name, Major");
                foreach (var row in values)
                {
                    // Print columns A and E, which correspond to indices 0 and 4.
                    Console.WriteLine("{0}, {1}", row[0], row[4]);
                }
            }
            else
            {
                Console.WriteLine("No data found.");
            }
            Console.Read();
        }

        //private static void TestGoogleDrive()
        //{
        //    // If modifying these scopes, delete your previously saved credentials
        //    // at ~/.credentials/drive-dotnet-quickstart.json
        //    string[] Scopes = { DriveService.Scope.DriveReadonly };
        //    string ApplicationName = "Drive API .NET Quickstart";


        //    UserCredential credential;

        //    using (var stream =
        //        new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
        //    {
        //        // The file token.json stores the user's access and refresh tokens, and is created
        //        // automatically when the authorization flow completes for the first time.
        //        string credPath = "token.json";
        //        credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
        //            GoogleClientSecrets.Load(stream).Secrets,
        //            Scopes,
        //            "user",
        //            CancellationToken.None,
        //            new FileDataStore(credPath, true)).Result;
        //        Console.WriteLine("Credential file saved to: " + credPath);
        //    }

        //    // Create Drive API service.
        //    var service = new DriveService(new BaseClientService.Initializer()
        //    {
        //        HttpClientInitializer = credential,
        //        ApplicationName = ApplicationName,
        //    });

        //    // Define parameters of request.
        //    FilesResource.ListRequest listRequest = service.Files.List();
        //    listRequest.PageSize = 10;
        //    listRequest.Fields = "nextPageToken, files(id, name)";

        //    // List files.
        //    IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute()
        //        .Files;
        //    Console.WriteLine("Files:");
        //    if (files != null && files.Count > 0)
        //    {
        //        foreach (var file in files)
        //        {
        //            Console.WriteLine("{0} ({1})", file.Name, file.Id);
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("No files found.");
        //    }
        //    Console.Read();
        //}

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

        public class DownloadedFile
        {
            public string Name { get; set; }
            public MemoryStream File { get; set; }
        }
    }
}