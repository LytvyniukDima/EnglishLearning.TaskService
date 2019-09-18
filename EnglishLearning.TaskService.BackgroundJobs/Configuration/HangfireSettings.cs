namespace EnglishLearning.TaskService.BackgroundJobs.Configuration
{
    public class HangfireSettings
    {
        public string ConnectionString { get; set; }
        public string Interval { get; set; }
    }
}