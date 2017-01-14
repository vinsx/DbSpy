using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
using RepositoryInterface;

namespace Repository
{
    public class FileRepository : IFileRepository
    {
        private readonly List<FileInfo> _fileInfoList;
        private readonly List<string> _extentions;
        private readonly string[] _filterNames;// = { "value" };
        private readonly string[] _excludeDirectories; // = {"\\bin", "\\obj", "\\debug"};
        private readonly string[] _excludeFilesContaining;
        private readonly string _excludeKeyWords;

        public FileRepository()
        {
            var solutionPath = ConfigurationManager.AppSettings["SolutionPath"];
            var searchFilePattern = ConfigurationManager.AppSettings["SearchFilePattern"];
            _filterNames = ConfigurationManager.AppSettings["FilterNames"].Split(';');
            _excludeFilesContaining = ConfigurationManager.AppSettings["ExcludeFilesContaining"].Split(';');
            _excludeDirectories = ConfigurationManager.AppSettings["ExcludeDirectories"].Split(';');
            _excludeKeyWords = ConfigurationManager.AppSettings["ExcludeKeyWords"];

            var dir = new DirectoryInfo(solutionPath);
            _extentions = searchFilePattern.Split(';').ToList();
            _fileInfoList = dir.GetFiles("*.*", SearchOption.AllDirectories)
                               .Where(f => _extentions.Contains(f.Extension)
                                        && !_excludeDirectories.Any(d => f.DirectoryName.Contains(d))   //Exlude directories
                                        && !_excludeFilesContaining.Any(n => f.Name.Contains(n))        //Exclude files containing predefined text
                                        && _filterNames.All(n => f.Name != n))                          //Exlude file names
                               .ToList();

            //Directory.GetFiles(solutionPath, "*.*", SearchOption.AllDirectories).ToList();
        }

        public List<string> GetReferencinFiles(string objectName)
        {
            if (FilterOut(objectName))
                return null;

            var queryMatchingFiles = from file in _fileInfoList.AsParallel()

                                     from line in File.ReadLines(file.FullName)

                                     where !Regex.IsMatch(line, @"^\s*['/#]")   // Don't include commented out referances
                                            && !Regex.IsMatch(
                                                    line,
                                                    string.Format(@"{0}|(?:where[\s\w]*\:)|(?:set\s*{{)[\s\w]*{1}", _excludeKeyWords, objectName))
                                            && !Regex.IsMatch(line, string.Format(@"\<{0}\>", objectName))
                                            && Regex.IsMatch(line, string.Format(@"(?<![\'\/][\s\w]*)(?<![\w\d]){0}(?![\w\d])", objectName))
                                     select file.FullName ;

            return queryMatchingFiles.ToList();
        }

        private bool FilterOut(string objectName)
        {
            return _filterNames.Any(f => f == objectName);
        }
    }
}
