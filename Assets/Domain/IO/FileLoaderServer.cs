//===================
// ファイルローダーサービスロケータ

namespace IO
{
    interface IFileLoader
    {
        byte[] LoadAllBytes(string path);
        string LoadAllText(string path);
    }
    class FileLoaderServer : ServiceLocator<IFileLoader>
    {
    }
}