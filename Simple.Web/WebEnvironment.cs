namespace Simple.Web
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    public sealed class WebEnvironment : IWebEnvironment
    {
        private static readonly IDictionary<string,string[]> ContentTypeLookup = new Dictionary<string, string[]>
                                                                                    {
                                                                                        { ".css", new[] {"text/css"}},
                                                                                        { ".js", new[] {"application/javascript","text/javascript"}},
                                                                                    };
        private static readonly string BinBasedAppRoot =
            Path.GetDirectoryName(Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetPath()));

        private readonly IPathUtility _pathUtility = new PathUtility();
        private readonly IFileUtility _fileUtility = new FileUtility();

        public string AppRoot
        {
            get { return BinBasedAppRoot; }
        }

        public IPathUtility PathUtility
        {
            get { return _pathUtility; }
        }

        public IFileUtility FileUtility
        {
            get { return _fileUtility; }
        }

        public string GetContentTypeFromFileExtension(string file, IList<string> acceptedTypes)
        {
            var extension = Path.GetExtension(file);
            if (string.IsNullOrWhiteSpace(extension)) return null;
            return ContentTypeLookup.ContainsKey(extension) ? ContentTypeLookup[extension].FirstOrDefault(acceptedTypes.Contains) : null;
        }
    }
}