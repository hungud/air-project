using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;


namespace App
{
    /// <summary>
    /// Developed By:Rakesh Pal
    /// Developed On:02/09/2015
    /// Description:For GET SET File and Directory.
    /// </summary>
    public class FileDirectory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DirectoryPath"></param>
        public void CreateValidateDirectory(string DirectoryPath)
        {
            string[] pathParts = DirectoryPath.Split('\\');
            IList<string> PathList = pathParts;
            List<String> list = new List<string>(pathParts);
            for (int i = 0; i < pathParts.Length; i++)
            {
                if (i > 0)
                    pathParts[i] = Path.Combine(pathParts[i - 1], pathParts[i]);

                if (!Directory.Exists(pathParts[i]))
                    Directory.CreateDirectory(pathParts[i]);
            }

            string DirectoryPathName = string.Empty;
            foreach (var item in DirectoryPath.Split('\\').ToList())
            {
                if (item.Contains(":")){ DirectoryPathName = Path.Combine(DirectoryPathName, item); }
                else if (!item.Contains(":"))
                {
                    DirectoryPathName = Path.Combine(DirectoryPathName, item);
                    if (!Directory.Exists(DirectoryPathName))
                        Directory.CreateDirectory(DirectoryPathName);
                }
            }
        }
        public void CreateAndValidateDirectory(string DirectoryPath)
        {
            string[] pathParts = DirectoryPath.Split('\\');
            List<String> list = pathParts.ToList();
            string DirectoryPathName = string.Empty;
            foreach (var item in DirectoryPath.Split('\\').ToList())
            {
                if (item.Contains(":"))
                { DirectoryPathName = Path.Combine(DirectoryPathName, item); }
                else if (!item.Contains(":"))
                {
                    DirectoryPathName = Path.Combine(DirectoryPathName, item);
                    if (!Directory.Exists(DirectoryPathName))
                        Directory.CreateDirectory(DirectoryPathName);
                }
            }
        }

        public void CreateValidateDirectoryAndSaveFile(string DirectoryPath, string DirectoryFileName, System.Web.HttpPostedFileBase HttpFile)
        {
            string[] pathParts = DirectoryPath.Split('\\');
            List<String> list = pathParts.ToList();
            string DirectoryPathName = string.Empty;
            foreach (var item in DirectoryPath.Split('\\').ToList())
            {
                if (item.Contains(":"))
                { DirectoryPathName = Path.Combine(DirectoryPathName, item); }
                else if (!item.Contains(":"))
                {
                    DirectoryPathName = Path.Combine(DirectoryPathName, item);
                    if (!Directory.Exists(DirectoryPathName))
                        Directory.CreateDirectory(DirectoryPathName);
                }
            }
            if (!string.IsNullOrEmpty(HttpFile.FileName))
            {
                HttpFile.SaveAs(Path.Combine(DirectoryPath, DirectoryFileName));
            }
        }
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="indent"></param>
        public void ShowAllFolderSubFolder(string path, int indent)
        {
            foreach (string folder in Directory.GetDirectories(path))
            {
                Console.WriteLine("{0}{1}", new string(' ', indent), Path.GetFileName(folder));
                ShowAllFolderSubFolder(folder, indent + 2);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sDir"></param>
        /// <param name="path"></param>
        public void DirSearch(string sDir, string path)
        {
            try
            {
                System.Collections.Generic.List<string> MyData = new System.Collections.Generic.List<string>();
                foreach (string d in Directory.GetDirectories(sDir))
                {
                    foreach (string f in Directory.GetFiles(d, path))
                    {
                        MyData.Add(f);
                    }
                    DirSearch(d, path);
                }
            }
            catch (System.Exception excpt)
            {
                Console.WriteLine(excpt.Message);
            }
        }

        public static string CurrentDirName
        {
            get {
                string dirName = string.Empty;

                try
                {
                    if (System.IO.Directory.Exists(App.Common.ConfigValues.CurrentDirFilePath))
                    {
                        string[] files = System.IO.Directory.GetFiles(App.Common.ConfigValues.CurrentDirFilePath);
                        if (files.Length > 0)
                        {
                            dirName = (new System.IO.FileInfo(files[files.Length - 1])).Name;
                        }
                    }
                }
                catch
                {

                }

                return dirName;
            }
        }
    }
}
