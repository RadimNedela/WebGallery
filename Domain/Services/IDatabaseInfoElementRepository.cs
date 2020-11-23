using System;
using Domain.Elements.Maintenance;

namespace Domain.Services
{
    public interface IDatabaseInfoElementRepository
    {
        DatabaseInfoElement First(Func<DatabaseInfoElement, bool> predicate);
    }
}