using Domain.Elements.Maintenance;
using System;

namespace Domain.Services
{
    public interface IDatabaseInfoElementRepository
    {
        DatabaseInfoElement First(Func<DatabaseInfoElement, bool> predicate);
    }
}