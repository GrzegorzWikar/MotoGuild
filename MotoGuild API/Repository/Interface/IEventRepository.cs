using Domain;
using MotoGuild_API.Helpers;

namespace MotoGuild_API.Repository.Interface;

public interface IEventRepository : IDisposable
{
    int TotalNumberOfEvents();
    IEnumerable<Event> GetAll(PaginationParams @params);
    Event Get(int eveId);
    void Insert(Event eve);
    void Delete(int eveId);
    void Update(Event eve);
    void Save();
}