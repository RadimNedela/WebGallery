namespace WebGalery.Domain.Warehouses
{
    public interface ILocation
    {
        string Name { get; }

        /// <summary>
        /// Convert the given specific string (for example full name of the location like fileName in directory structure)
        /// to list of strings that could be used to create "journey" to get to the given location (like for fileName create Directory Binders)
        /// </summary>
        IEnumerable<string> SplitJourneyToLegs(string concreteLocationName);
    }
}