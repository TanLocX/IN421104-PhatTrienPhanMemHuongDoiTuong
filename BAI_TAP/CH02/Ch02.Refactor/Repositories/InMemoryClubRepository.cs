using EduHub.Ch02.Domain;

namespace EduHub.Ch02.Repositories;

public class InMemoryClubRepository : IClubRepository
{
    private readonly List<Club> _clubs = new();

    public IReadOnlyList<Club> GetAll()
    {
        return _clubs.AsReadOnly();
    }
    public Club? GetById(Guid id)
    {
        return _clubs.FirstOrDefault(c => c.Id == id);
    }
    public void Add(Club club)
    {
        _clubs.Add(club);
    }
}
