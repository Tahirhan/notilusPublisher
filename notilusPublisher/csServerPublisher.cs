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
            (string plugin, string rhinoVersion, string dbType) = getDetails();

            string dateFolderStr = getDateFolderString(rhinoVersion, dbType, plugin);

            string key = pluginBinAndExportPaths.Keys.Where(r => r.Contains(plugin)).FirstOrDefault();
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

            return Result.SUCCESS;
        }

        private (string plugin, string rhinoVersion, string dbType) getDetails()
        {
            Dictionary<string, string> abbreviations = new Dictionary<string, string>() {
                { "ns", "NotilusSteeler" },
                { "nd", "NotilusDrafting" },
                { "np", "NotilusPiping" },
                { "nt", "NotilusTools" },
                { "nrm", "NotilusRulemaster" },
                { "npid", "NotilusPID" },
                { "nob", "NotilusOnBoard" },
                { "nc", "Notilus_Clipper" },
            };

            Console.WriteLine("Abbreviations");
            foreach (string key in abbreviations.Keys) Console.WriteLine($"{key} : {abbreviations[key]}");
            Console.WriteLine("-------------");

            Console.WriteLine("Enter plugin code:");
            string plugin = Console.ReadLine();
            plugin = abbreviations[plugin];

            Console.WriteLine("Enter Rhino version:");
            string rhinoVersion = Console.ReadLine();

            Console.WriteLine("Enter database type (Local or Cloud):");
            string dbType = Console.ReadLine();

            return (plugin, rhinoVersion, dbType);
        }

        private void CreateRhiFile(string pathToCopyBin, string pluginName)
        {
            ZipFile.CreateFromDirectory(pathToCopyBin, pathToCopyBin.Replace(@"\bin", @"\" + pluginName + ".rhi"));
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

        private string getDateFolderString(string rhinoVersion, string dbType, string plugin)
        {
            return $"{plugin} R{rhinoVersion} {dbType} {DateTime.Now.ToString("yyyy-MM-dd").Substring(2)}";
        }
    }
}
