namespace EduQuest.Repositorys
{
    public class VideoRepo
    {
        private readonly EduQuestContext context;

        public VideoRepo(EduQuestContext context)
        {
            this.context = context;
        }

        public void Add(Video video)
        {
            context.Videos.Add(video);
        }
        public void Delete(int id)
        {
            var video = context.Videos.Find(id);
            context.Videos.Remove(video);
        }
        public void save()
        {
            context.SaveChanges();
        }
    }
}
