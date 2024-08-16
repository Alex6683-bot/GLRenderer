namespace GLRenderer
{
    static class Utilities
    {
        public static string ToRelativePath(string path)
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\", path);
        }
    }
}

