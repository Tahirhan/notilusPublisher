using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace notilusPublisher
{
    class csServerPublisher
    {
        Dictionary<string, string> pluginBinAndExportPaths = new Dictionary<string, string>();

        public csServerPublisher(Dictionary<string, string> _pluginBinAndExportPaths)
        {
            pluginBinAndExportPaths = _pluginBinAndExportPaths;
        }

        public Result run()
        {
            string dateFolderStr = getDateFolderString();

            foreach (string key in this.pluginBinAndExportPaths.Keys)
            {
                try
                {
                    string pluginName = key.Split(@"\".ToArray()).Last();
                    string pathToCopyBin = deleteAndCreateNewDateFolder(dateFolderStr, this.pluginBinAndExportPaths[key]);
                    CopyBinToPath(key, pathToCopyBin);
                    CreateRhiFile(pathToCopyBin, pluginName);
                    Console.WriteLine($"[{DateTime.Now.ToLongTimeString()} - notilusPublisher] Published {pluginName}.");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"[{DateTime.Now.ToLongTimeString()} - notilusPublisher] Failed to publish {key.Split(@"\".ToArray()).Last()}.");
                }
            }

            return Result.SUCCESS;
        }

        private void CreateRhiFile(string pathToCopyBin, string pluginName)
        {
            ZipFile.CreateFromDirectory(pathToCopyBin, pathToCopyBin.Replace(@"\bin", @"\"+pluginName+".rhi"));
        }

        private void CopyBinToPath(string binPath, string pathToCopyBin)
        {
            binPath = Path.GetFullPath(Path.Combine(binPath, "bin"));
            string[] allDirectories = Directory.GetDirectories(binPath, "*", SearchOption.AllDirectories);
            foreach (string dir in allDirectories)
            {
                string dirToCreate = dir.Replace(binPath, pathToCopyBin);
                DirectoryInfo directoryInfo = Directory.CreateDirectory(dirToCreate);
            }

            var allFiles = Directory.GetFiles(binPath, "*.*", SearchOption.AllDirectories);
            foreach (string newPath in allFiles)
            {
                File.Copy(newPath, newPath.Replace(binPath, pathToCopyBin), true);
            }
        }

        private string deleteAndCreateNewDateFolder(string dateFolderStr, string exportPath)
        {
            string folderPath = Path.GetFullPath(Path.Combine(exportPath, dateFolderStr));
            if (Directory.Exists(folderPath)) Directory.Delete(folderPath, true);
            DirectoryInfo directoryInfo = Directory.CreateDirectory(folderPath);
            folderPath = Path.GetFullPath(Path.Combine(folderPath, "bin"));
            directoryInfo = Directory.CreateDirectory(folderPath);
            return folderPath;
        }

        private string getDateFolderString()
        {
            return DateTime.Now.ToString("yyyy.MM.dd").Substring(2);
        }
    }
}
