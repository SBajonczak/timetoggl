namespace PipelinesTest
{
    /// <summary>
    /// Project definition.
    /// </summary>
    public class Project
    {

        public string Title { get; set; }

        public string Tag { get; set; }

        public override string ToString()
        {
            return $"{this.Title} ({this.Tag})";
        }
    }
}