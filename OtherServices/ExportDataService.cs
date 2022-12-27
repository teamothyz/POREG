using POREG.Models;

namespace POREG.OtherServices
{
    public class ExportDataService
    {
        private static readonly object lockobj = new();
        private static readonly object lockobjErrorInput = new();
        private static readonly object lockobjErrorRunning = new();
        private static readonly string OutputFolderName = "Output";

        private static string GetExportFolder()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var folderDestination = $"{currentDirectory}/{OutputFolderName}";
            if (!Directory.Exists(folderDestination))
            {
                Directory.CreateDirectory(folderDestination);
            }
            return folderDestination;
        }

        public static void ExportData(POInfor regInfor)
        {
            try
            {
                lock (lockobj)
                {
                    var time = DateTime.Now;
                    var root = GetExportFolder();
                    using var file = new StreamWriter($"{root}/SUCCESS_{time.Day}_{time.Month}.txt", append: true);
                    var data = $"{regInfor.Email}|{regInfor.Password}|" +
                    $"{regInfor.Name}|{regInfor.Birthday}|{regInfor.IDCard}" +
                    $"|{regInfor.Address}|{regInfor.ZipCode}|{regInfor.Phone}" +
                    $"|{regInfor.GetBankBranchName()}|{regInfor.GetBankCode()}|{regInfor.GetBankNumber()}" +
                    $"|{regInfor.Status}";
                    file.WriteLine(data);
                    file.Flush();
                    file.Close();
                }
            }
            catch (Exception) { }
        }

        public static void ExportErrorData(string line)
        {
            try
            {
                lock (lockobjErrorInput)
                {
                    var time = DateTime.Now;
                    var root = GetExportFolder();
                    using var file = new StreamWriter($"{root}/INVALID_{time.Day}_{time.Month}.txt", append: true);
                    file.WriteLine(line);
                    file.Flush();
                    file.Close();
                }
            }
            catch (Exception) { }
        }

        public static void ExportErrorData(POInfor regInfor)
        {
            try
            {
                lock (lockobjErrorRunning)
                {
                    var time = DateTime.Now;
                    var root = GetExportFolder();
                    using var file = new StreamWriter($"{root}/FAILED_{time.Day}_{time.Month}.txt", append: true);
                    var data = $"{regInfor.Email}|{regInfor.Password}|" +
                    $"{regInfor.Name}|{regInfor.Birthday}|{regInfor.IDCard}" +
                    $"|{regInfor.Address}|{regInfor.ZipCode}|{regInfor.Status}";
                    file.WriteLine(data);
                    file.Flush();
                    file.Close();
                }
            }
            catch (Exception) { }
        }
    }
}
