namespace Archspace2
{
    public interface IBuildings
    {
        int Factory { get; }
        int ResearchLab { get; }
        int MilitaryBase { get; }

        int Total();
    }
}
