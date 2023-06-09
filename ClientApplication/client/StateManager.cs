using System.Diagnostics;


namespace Hardware.Driver
{



    public class AppManager
    {
        public enum State
        {
            Running,
            Selecting,
            Stopped
        }

        private enum InputType
        {
            ButtonPressed,
            Next,
            Previous,
            None
        }

        public Stopwatch sw { get; private set; }
        private int CurrentSelectedPosition = 0;
        public TimeSpan ElapsedTime { get; private set; }

        public State CurrentState { get; private set; }

        public EventArgs Next;

        public event EventHandler<int> PreviousElement;
        public event EventHandler<int> NextElement;

        public event EventHandler<StateArgs> ButtonPressed;


        protected void OnNext(int index)
        {
            EventHandler<int> handler = NextElement;
            if (handler != null)
            {
                handler(this, index);
            }
        }

        protected void OnSelected(State before, State after)
        {
            EventHandler<StateArgs> handler = ButtonPressed;
            if (handler != null)
            {
                handler(this, new StateArgs(before, after));
            }
        }

        protected void OnPrevious(int index)
        {
            EventHandler<int> handler = PreviousElement;
            if (handler != null)
            {
                handler(this, index);
            }
        }



        public AppManager()
        {
            sw = new Stopwatch();
            CurrentState = State.Stopped;
        }


        public void DoRead(string input)
        {
            InputType inputType = ParseData(input);
            // Console.WriteLine($"Current State {this.CurrentState} Input Type{inputType}  Data {input}");

            switch (CurrentState)
            {
                case State.Stopped:
                    if (inputType == InputType.ButtonPressed)
                    {
                        sw.Start();
                        OnSelected(CurrentState, State.Selecting);
                        CurrentState = State.Selecting;
                    }

                    break;
                case State.Selecting:
                    switch (inputType)
                    {
                        case InputType.Next:
                            OnNext(CurrentSelectedPosition);
                            break;
                        case InputType.Previous:
                            OnPrevious(CurrentSelectedPosition);
                            break;
                        case InputType.ButtonPressed:
                            sw.Start();
                            OnSelected(CurrentState, State.Running);
                            CurrentState = State.Running;
                            break;

                    }
                    break;
                case State.Running:
                    if (inputType == InputType.ButtonPressed)
                    {
                        ElapsedTime = sw.Elapsed;
                        sw.Stop();
                        OnSelected(CurrentState, State.Stopped);
                        CurrentState = State.Stopped;
                    }
                    break;

            }
        }

        private InputType ParseData(string input)
        {

            if (string.Compare(input, "pressed", StringComparison.CurrentCultureIgnoreCase) > 0)
            {
                return InputType.ButtonPressed;
            }
            else
            {

                int i = int.Parse(input);
                if (i > CurrentSelectedPosition)
                {
                    CurrentSelectedPosition = i;
                    return InputType.Next;
                }
                else
                {
                    CurrentSelectedPosition = i;
                    return InputType.Previous;
                }
            }
            return InputType.None;
        }


    }
}