using static Hardware.Driver.AppManager;

namespace Hardware.Driver
{
    public class StateArgs
    {
        public State Before { get; set; }
        public State After { get; set; }

        public StateArgs(State before, State after)
        {
            Before = before;
            After = after;
        }
    }
}