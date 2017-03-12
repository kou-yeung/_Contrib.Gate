using System;
using System.IO;

namespace IO
{
    class FileLoader : IFileLoader
    {
        string basePath = "";
        public FileLoader(string basePath = "")
        {
            this.basePath = basePath;
        }

        public byte[] LoadAllBytes(string path)
        {
            return File.ReadAllBytes(Path.Combine(basePath, path));
        }

        public string LoadAllText(string path)
        {
            return File.ReadAllText(Path.Combine(basePath, path));
        }
    }
}
