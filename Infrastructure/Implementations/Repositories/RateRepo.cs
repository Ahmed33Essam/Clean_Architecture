namespace EduQuest.Repositorys
{
    public class RateRepo : IRate
    {
        private readonly EduQuestContext _context;

        public RateRepo(EduQuestContext context)
        {
            _context = context;
        }

        public void AddRating(Rate rating)
        {
            _context.Rates.Add(rating);
        }

        public float GetAverageRating(int targetId, string targetType)
        {
            var ratings = _context.Rates
                .Where(r => r.TargetId == targetId && r.TargetType == targetType)
                .ToList();

            if (ratings.Count == 0) return 0;
            return (float)Math.Round(ratings.Average(r => r.Value), 2);
        }

        public List<Rate> GetRatingsForTarget(int targetId, string targetType)
        {
            return _context.Rates
                .Where(r => r.TargetId == targetId && r.TargetType == targetType)
                .ToList();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
