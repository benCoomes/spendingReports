using System;
using System.IO;

namespace Coomes.SpendingReports.IntegrationTest
{
    public static class InitialData
    {
        public static string StoreLocation { get; }
        private static string ReferenceLocation { get;}

        static InitialData()
        {
            StoreLocation = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "storage");
            ReferenceLocation = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testData");
        }

        public static void ResetStorage()
        {
            if(Directory.Exists(StoreLocation))
            {
                Directory.Delete(StoreLocation, true);
            }
            Directory.CreateDirectory(StoreLocation);

            var files = new DirectoryInfo(ReferenceLocation).EnumerateFiles();
            foreach(var file in files)
            {
                File.Copy(file.FullName, Path.Combine(StoreLocation, file.Name));
            }
        }
    }

}