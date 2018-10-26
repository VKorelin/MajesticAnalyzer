using CsQuery;

namespace MajesticAnalyzer.Html
{
    public class CqHtmlExtractor : IHtmlExtractor
    {
        public string ExtractDescription(string html)
        {
            const string tag = "meta";
            const string nameAttribute = "name";
            const string nameValue = "description";
            const string contentAttribute = "content";

            var cq = CQ.Create(html);
            var result = string.Empty;
            foreach (var obj in cq.Find(tag))
            {
                var attributeVal = obj.GetAttribute(nameAttribute);
                if (attributeVal == nameValue)
                {
                    string content = obj.GetAttribute(contentAttribute);
                    if (string.IsNullOrEmpty(content) && string.IsNullOrEmpty(obj.InnerText))
                    {
                        continue;
                    }

                    result += content ?? obj.InnerText + " ";
                }
            }
            return result;
        }

        public string ExtractTitle(string html)
        {
            const string tagName = "title";
            CQ cq = CQ.Create(html);

            var result = string.Empty;

            foreach (var obj in cq.Find(tagName))
            {
                result += obj.InnerText + " ";
            }

            return result;
        }
    }
}
