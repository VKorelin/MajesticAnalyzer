namespace MajesticAnalyzer.Domain
{
    public class Backlink
    {
        // ReSharper disable once MemberCanBePrivate.Global
        public string Url { get; set; }
        
        public Backlink() { }

        public Backlink(string url)
        {
            Url = url;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Backlink backlink))
            {
                return false;
            }

            return string.Equals(Url, backlink.Url);
        }
        
        public override int GetHashCode() => Url.GetHashCode();
    }
}