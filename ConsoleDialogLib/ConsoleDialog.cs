namespace ConsoleDialogLib
{
    public partial class ConsoleDialog
    {
        public ConsoleDialog(
            string welcomeMessage,
            List<ConsoleDialogOption>? options = null,
            bool exitOnBackspacePressed = true,
            bool clearConsoleOnStartup = true
        )
        {
            WelcomeMessage = welcomeMessage;
            ExitOnBackspacePressed = exitOnBackspacePressed;
            ClearConsoleOnStartup = clearConsoleOnStartup;

            if (options != null)
                foreach (var option in options)
                    AddOption(option);
        }

        private bool IsExitRequired { get; set; }
        private bool IsShown { get; set; }

        private string? key;
        private string Key
        {
            get { return key ??= GetHashCode().ToString(); }
            set { key = value; }
        }

        private string? welcomeMessage;
        private string WelcomeMessage
        {
            get { return welcomeMessage ??= DefaultWelcomeMessage; }
            set { welcomeMessage = value; }
        }

        private bool ClearConsoleOnStartup { get; set; }
        private bool ExitOnBackspacePressed { get; set; }

        private readonly List<ConsoleDialogOption> options = new();

        public void AddOption(ConsoleDialogOption option) => options.Add(option);
        public bool RemoveOption(ConsoleDialogOption option) => options.Remove(option);
    }
}