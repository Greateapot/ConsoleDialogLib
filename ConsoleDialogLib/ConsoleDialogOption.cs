namespace ConsoleDialogLib
{
    public class ConsoleDialogOption
    {

        public delegate void VoidCallback(ConsoleDialog dialog);
        public delegate object? NullableCallback(ConsoleDialog dialog);


        public ConsoleDialogOption(
            string message,
            VoidCallback callback,
            bool clearAfterExecuted = true,
            bool pauseAfterExecuted = false,
            bool exitAfterExecuted = false
        )
        {
            Message = message;
            Callback = ConvertVoidCallbackToNullableCallback(callback);
            ClearAfterExecuted = clearAfterExecuted;
            PauseAfterExecuted = pauseAfterExecuted;
            ExitAfterExecuted = exitAfterExecuted;
        }

        public ConsoleDialogOption(
            string message,
            NullableCallback callback,
            bool clearAfterExecuted = true,
            bool pauseAfterExecuted = false,
            bool exitAfterExecuted = false
        )
        {
            Message = message;
            Callback = callback;
            ClearAfterExecuted = clearAfterExecuted;
            PauseAfterExecuted = pauseAfterExecuted;
            ExitAfterExecuted = exitAfterExecuted;
        }

        public string Message { get; private set; }

        public NullableCallback Callback { get; private set; }

        public bool PauseAfterExecuted { get; private set; }

        public bool ClearAfterExecuted { get; private set; }

        public bool ExitAfterExecuted { get; private set; }

        private static NullableCallback ConvertVoidCallbackToNullableCallback(
            VoidCallback callback
        ) =>
            (dialog) =>
            {
                callback(dialog);
                return null;
            };
    }
}