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

            CQ cq = CQ.Create(html);
            string result = string.Empty;
            foreach (IDomObject obj in cq.Find(tag))
            {
                string attributeVal = obj.GetAttribute(nameAttribute);
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

            string result = string.Empty;

            foreach (IDomObject obj in cq.Find(tagName))
            {
                result += obj.InnerText + " ";
            }

            return result;
        }
    }
}
