namespace Hardware.Driver
{
    /// <summary>
    /// Project definition.
    /// </summary>
    public class Recording
    {
        public Recording(Project project, TimeSpan ts, string comment)
        {
            Project = project.Title;
            Tag = project.ProjectNumber;
            Duration = ts;
            Comment = comment;
            Date = DateTimeOffset.Now;
        }

        public string Project { get; set; }

        public string Tag { get; set; }

        public DateTimeOffset Date { get; set; }
        public string Comment { get; set; }

        public TimeSpan Duration { get; set; }

        public override string ToString()
        {
            return $"{Project};{Tag};{Duration.ToString()};{Date.ToString("dd.MM.yyyy")};{Comment}";
        }
    }
}