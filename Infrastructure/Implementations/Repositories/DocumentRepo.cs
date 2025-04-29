namespace EduQuest.Repositorys
{
    public class DocumentRepo
    {
        private readonly EduQuestContext context;

        public DocumentRepo(EduQuestContext context)
        {
            this.context = context;
        }

        public void Add(Document document)
        {
            context.Documents.Add(document);
        }
        public void Delete(int id)
        {
            Document document = context.Documents.Find(id);
            context.Documents.Remove(document);
        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
