namespace PipelinesTest
{
    /// <summary>
    /// Project definition.
    /// </summary>
    public class Recording
    {
        public Recording(Project project, TimeSpan ts, string comment)
        {
            this.Project = project.Title;
            this.Tag = project.Tag;
            this.Duration = ts;
            this.Comment= comment;
            this.Date = DateTimeOffset.Now;
        }

        public string Project { get; set; }

        public string Tag { get; set; }
        
        public DateTimeOffset Date {get;set;}
        public string Comment { get; set; }

        public TimeSpan Duration { get; set; }

        public override string ToString()
        {
            return $"{this.Project};{this.Tag};{this.Duration.Hours}:{this.Duration.Minutes};{Date.ToString("dd.MM.yyyy")};{Comment}";
        }
    }
}