using PipelinesTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace client
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

        public TimerTrackingStateChangedArgs(TimeSpan duration, AppManager.State before, AppManager.State after) {
            this.Duration = duration;
            this.Before = before;
            this.After = after;
        }
    }
}
