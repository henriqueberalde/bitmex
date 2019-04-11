//using Google.Apis.Auth.OAuth2;
//using Google.Apis.Download;
//using Google.Apis.Drive.v3;
//using Google.Apis.Services;
//using Google.Apis.Util.Store;
//using System;
//using System.IO;
//using System.Threading;
//using static BitMex.Program;

//namespace Foxbit.FinancialReport.Job
//{
//    public static class GoogleDriveUtil
//    {
//        public static DownloadedFile DownloadFile(string driveId)
//        {
//            MemoryStream result = new MemoryStream();
//            var Scopes = new string[] { DriveService.Scope.DriveReadonly };
//            var ApplicationName = "Drive API .NET Quickstart";
//            UserCredential credential;

//            using (var stream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
//            {
//                string credPath = "token.json";
//                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
//                    GoogleClientSecrets.Load(stream).Secrets,
//                    Scopes,
//                    "user",
//                    CancellationToken.None,
//                    new FileDataStore(credPath, true)).Result;
//                Console.WriteLine("Credential file saved to: " + credPath);
//            }

//            var service = new DriveService(new BaseClientService.Initializer()
//            {
//                HttpClientInitializer = credential,
//                ApplicationName = ApplicationName,
//            });

//            var request = service.Files.Get(driveId);

//            var response = request.DownloadWithStatus(result);

//            if (response.Status != DownloadStatus.Completed)
//                throw new DownloadException($"Erro ao fazer o download do arquivo: {driveId}. Erro: {response.Exception.Message}", response.Exception);

//            result.Seek(0, SeekOrigin.Begin);

//            return new DownloadedFile { Name = service.Files.Get(driveId).Execute().Name, File = result };
//        }

//        private static bool TimeOut(DateTime date, TimeSpan timeOut, string driveId)
//        {
//            var now = DateTime.Now;

//            if ((now - date) > timeOut)
//                throw new Exception($"Timeout na chamada de download para o arquivo: {driveId}");

//            return false;
//        }
//    }

//    public class DownloadException : Exception
//    {
//        public DownloadException(string message, Exception inner) : base(message, inner)
//        {
//        }
//    }
//}
