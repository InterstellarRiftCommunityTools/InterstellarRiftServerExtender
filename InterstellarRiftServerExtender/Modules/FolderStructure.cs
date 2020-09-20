﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using NLog;
namespace IRSE.Modules
{
    public class FolderStructure
    {
        private const string RootFolderName = "IRSE";
        private Assembly _assembly;
        private static NLog.Logger mainLog; //mainLog.Error

        public FolderStructure()
        {
            mainLog = NLog.LogManager.GetCurrentClassLogger();

            _assembly = Assembly.GetExecutingAssembly();

            List<string> directories = new List<string>();
            directories.Add("bin");
            directories.Add("config");
            directories.Add("localization");
            directories.Add("plugins");
            directories.Add("logs");
            directories.Add("updates");

            if (!Directory.Exists(RootFolderName))
                Directory.CreateDirectory(RootFolderName);

            foreach (string directory in directories)
                if (!Directory.Exists(Path.Combine(RootFolderName, directory)))
                    Directory.CreateDirectory(Path.Combine(RootFolderName, directory));
        }

        public void Build()
        {
            try
            {
                foreach (string resource in _assembly.GetManifestResourceNames())
                {
                    using (Stream stream = _assembly.GetManifestResourceStream(resource))
                    {
                        string fileName = resource.Replace("IRSE.Resources.", "");

                        if (fileName.StartsWith("IRSE."))
                            continue;

                        string path = string.Empty;
                        switch (Path.GetExtension(resource))
                        {
                            case ".ini":
                            case ".config":
                            case ".json":
                                path = Globals.GetFolderPath(IRSEFolderName.Config, false);
                                break;

                            default:
                                break;
                        }

                        if (string.IsNullOrEmpty(path))
                            continue;

                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);


                        //TODO if a config file already exists
                        if (!File.Exists(Path.Combine(path, fileName)))
                        {
                            byte[] data = new byte[stream.Length];
                            stream.Read(data, 0, data.Length);

                            File.WriteAllBytes(Path.Combine(path, fileName), data);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] Hellion Extended Server[{ex.TargetSite}]: {ex.StackTrace}");
            }
        }
    }
}