namespace Application.Interfaces
{
    public interface IRate
    {
        void AddRating(Rate rating);
        float GetAverageRating(int targetId, string targetType);
        List<Rate> GetRatingsForTarget(int targetId, string targetType);
        void Save();
    }
}
