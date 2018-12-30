using MajesticAnalyzer.Domain;

namespace MajesticAnalyzer.IO
{
    public interface IReffContentOutputService
    {
        void WriteContent(ReffResource reffResource);
        void WriteContent(University university);
    }
}