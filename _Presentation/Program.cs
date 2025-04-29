using Microsoft.EntityFrameworkCore;

namespace EduQuest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<EduQuestContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection")));

            builder.Services.AddScoped<ICourse, CourseRepo>();
            builder.Services.AddScoped<IUser<Admin>, AdminRepo>();
            builder.Services.AddScoped<IUser<Instructor>, InstractorRepo>();
            builder.Services.AddScoped<IUser<Student>, StudentRepo>();
            builder.Services.AddScoped<IQuestionBank, QuestionBankRepo>();
            builder.Services.AddScoped<IQuiz, QuizRepo>();
            builder.Services.AddScoped<IRate, RateRepo>();


            builder.Services.AddAutoMapper(typeof(Program));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
