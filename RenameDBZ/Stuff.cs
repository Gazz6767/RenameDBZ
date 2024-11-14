namespace RenameDBZ
{
    public class Stuff
    {
        public IEnumerable<string> GetFilesInDirectory(string DirPath, bool ScanSubFolders)
        {
            IEnumerable<string> filePaths = filePaths = Directory.EnumerateFiles(DirPath, "*.*", new EnumerationOptions
            {
                IgnoreInaccessible = true,
                RecurseSubdirectories = ScanSubFolders
            });

            return filePaths;
        }
    }
}
