using static PipelinesTest.AppManager;

namespace PipelinesTest
{
    public class StateArgs
    {
        public State Before { get; set; }
        public State After { get; set; }

        public StateArgs(State before, State after)
        {
            this.Before = before;
            this.After = after;
        }
    }
}