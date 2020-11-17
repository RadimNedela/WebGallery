using Domain.Elements;

namespace Domain.Services
{
    public interface IContentElementRepository
    {
        void Add(ContentElement element);
        ContentElement Get(string hash);
    }
}