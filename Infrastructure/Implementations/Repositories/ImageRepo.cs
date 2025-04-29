namespace EduQuest.Repositorys
{
    public class ImageRepo
    {
        private readonly EduQuestContext context;

        public ImageRepo(EduQuestContext context)
        {
            this.context = context;
        }

        public void Add(Image image)
        {
            context.Images.Add(image);
        }
        public void Delete(int id)
        {
            var img = context.Images.Find(id);
            context.Images.Remove(img);
        }
        public void save()
        {
            context.SaveChanges();
        }
    }
}
