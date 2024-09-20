namespace GLRenderer
{
    static class Extension
    {
        public static string ToRelativePath(string path)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\", path);
        }
    }
}

