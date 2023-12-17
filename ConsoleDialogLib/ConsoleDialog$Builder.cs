using ConsoleIOLib;

namespace ConsoleDialogLib
{

    public partial class ConsoleDialog
    {
        public static object? ShowDialog(ConsoleDialog dialog)
        {
            if (dialog.options.Count == 0)
            {
                throw new Exception(string.Format(EmptyDialogErrorMessage, dialog.Key));
            }
            else if (dialog.IsShown)
            {
                throw new Exception(string.Format(DialogShownErrorMessage, dialog.Key));
            }
            else
            {
                dialog.IsShown = true;
            }

            int selectedOptionIndex = 0;
            bool isWrongKeyPressed = false;
            object? result = null;

            if (dialog.ClearConsoleOnStartup) ConsoleIO.Clear();

            while (!dialog.IsExitRequired)
            {
                if (!isWrongKeyPressed)
                {
                    if (dialog.ShowNavigation)
                        WriteNavigationMessage();
                    WriteWelcomeMessage(dialog);
                    WriteOptions(dialog, selectedOptionIndex);
                }
                else
                {
                    isWrongKeyPressed = false;
                }

                switch (ConsoleIO.ReadKey(intercept: true).Key)
                {
                    case ConsoleKey.UpArrow:
                        selectedOptionIndex = selectedOptionIndex > 0 ? selectedOptionIndex - 1 : dialog.options.Count - 1;
                        ConsoleIO.Clear();
                        break;
                    case ConsoleKey.DownArrow:
                        selectedOptionIndex = selectedOptionIndex < dialog.options.Count - 1 ? selectedOptionIndex + 1 : 0;
                        ConsoleIO.Clear();
                        break;
                    case ConsoleKey.Enter:
                        var option = dialog.options[selectedOptionIndex];
                        result = option.Callback(dialog);
                        if (option.PauseAfterExecuted)
                        {
                            ConsoleIO.WriteLine(PressAnyKeyToContinueMessage);
                            ConsoleIO.ReadKey(intercept: true);
                        }
                        if (option.ClearAfterExecuted)
                            ConsoleIO.Clear();
                        if (option.ExitAfterExecuted)
                            dialog.IsExitRequired = true;
                        break;
                    case ConsoleKey.Backspace:
                        if (dialog.ExitOnBackspacePressed) dialog.IsExitRequired = true;
                        break;
                    default:
                        isWrongKeyPressed = true;
                        break;
                }
            }

            dialog.IsShown = false;
            return result;
        }

        private static void WriteNavigationMessage() => ConsoleIO.WriteLine(NavigationMessage);
        private static void WriteWelcomeMessage(ConsoleDialog dialog) => ConsoleIO.WriteLine(dialog.WelcomeMessage);
        private static void WriteOptions(ConsoleDialog dialog, int selectedOptionIndex)
        {
            var formatString = $"{{0}} {{1,{dialog.options.Count.ToString().Length}}}. {{2}}";
            for (int index = 0; index < dialog.options.Count; index++)
                ConsoleIO.WriteLine(string.Format(
                    formatString,
                    index == selectedOptionIndex ? SelectedOptionPrefix : UnselectedOptionPrefix,
                    index + 1,
                    dialog.options[index].Message
                ));
        }
    }
}