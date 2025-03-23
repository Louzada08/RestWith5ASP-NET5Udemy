using System.ComponentModel.DataAnnotations.Schema;

namespace RestWithASPNET.Domain.Messages;

public abstract class Entity
{
    public Entity()
    {
        _events = new List<Event>();
    }

    [NotMapped]
    private List<Event> _events;

    [NotMapped]
    public IReadOnlyCollection<Event> Events => _events.AsReadOnly();

    public void AddEvent(Event ev)
    {
        _events.Add(ev);
    }

    public void RemoveEvent(Event eventItem)
    {
        _events?.Remove(eventItem);
    }

    public void ClearEvents()
    {
        _events?.Clear();
    }
}
