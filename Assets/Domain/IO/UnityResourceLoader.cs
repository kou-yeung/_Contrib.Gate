//======================
// Unityのリソースからファイルをロードするクラス

using System;
using UnityEngine;
using System.IO;

namespace IO
{
    public class UnityResourceLoader : IFileLoader
    {
        public byte[] LoadAllBytes(string path)
        {
            return Resources.Load<TextAsset>(Path.GetFileNameWithoutExtension(path)).bytes;
        }
        public string LoadAllText(string path)
        {
            return Resources.Load<TextAsset>(Path.GetFileNameWithoutExtension(path)).text;
        }
    }
}