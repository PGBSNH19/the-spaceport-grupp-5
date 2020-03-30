namespace TheSpaceport
{
    public interface IAddStarship : IConfigDatabase
    {
        IAddStarship StarshipControl();

        IAddStarship Charge();
    }
}