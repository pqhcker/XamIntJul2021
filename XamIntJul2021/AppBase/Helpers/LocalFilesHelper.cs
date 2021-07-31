using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace XamIntJul2021.AppBase.Helpers
{
    public static class LocalFilesHelper
    {
        static readonly string DEFAULT_PATH = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);


        public static async Task<string> ReadFileInPackageAsync(string name)
        {
            string fileContent = null;

            var assembly = typeof(LocalFilesHelper).GetTypeInfo().Assembly;

            var resourceName = assembly.GetManifestResourceNames().Where(r => r.ToLowerInvariant().EndsWith(name.ToLowerInvariant())).FirstOrDefault();

            if (resourceName is not null)
            {
                using Stream fileStream = assembly.GetManifestResourceStream(resourceName);
                fileStream.Seek(0, SeekOrigin.Begin);

                using var fileReader = new StreamReader(fileStream);
                fileContent = await fileReader.ReadToEndAsync();
            }

            return fileContent;
        }

        public static void SaveFile(string name, byte[] content)
        {
            var filePath = Path.Combine(DEFAULT_PATH, name);
            File.WriteAllBytes(filePath, content);
        }

        public static byte[] ReadFile(string name)
        {
            var filePath = Path.Combine(DEFAULT_PATH, name);
            if (!File.Exists(filePath))
                return null;
            return File.ReadAllBytes(filePath);
        }

        public static void DeleteFile(string name)
        {
            var filePath = Path.Combine(DEFAULT_PATH, name);
            if (File.Exists(filePath))
                File.Delete(filePath);
        }
    }
}
