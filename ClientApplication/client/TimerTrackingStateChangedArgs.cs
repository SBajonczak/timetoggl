namespace Hardware.Driver
{
    /// <summary>
    /// Argumentclass, when the timetracking was changed
    /// </summary>
    public class TimerTrackingStateChangedArgs
    {

        /// <summary>
        /// State before.
        /// </summary>
        public AppManager.State Before { get; private set; }

        /// <summary>
        /// New state
        /// </summary>
        public AppManager.State After { get; private set; }

        /// <summary>
        /// Measured duration, if exists
        /// </summary>
        public TimeSpan Duration { get; private set; }

        public TimerTrackingStateChangedArgs(TimeSpan duration, AppManager.State before, AppManager.State after)
        {
            Duration = duration;
            Before = before;
            After = after;
        }
    }
}
