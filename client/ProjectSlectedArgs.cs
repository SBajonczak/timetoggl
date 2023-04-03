using PipelinesTest;

namespace client
{
    /// <summary>
    /// Project was selected
    /// </summary>
    public class ProjectSelectedArgs
    {
        /// <summary>
        /// The current selected project.
        /// </summary>
        public Project SelectedProject { get; private set; }

        public ProjectSelectedArgs(Project selectedproject) => this.SelectedProject= selectedproject;

    }
}
