namespace ConsoleDialogLib
{
    public partial class ConsoleDialog
    {
        private const string DefaultWelcomeMessage = "Выберите опцию:";
        private const string SelectedOptionPrefix = "->";
        private const string UnselectedOptionPrefix = "  ";
        private const string NavigationMessage = "Навигация:"
                + "\n1. Стрелочки (верх/вниз) - перемещение по меню"
                + "\n2. Enter - выбор пункта меню"
                + "\n3. Backspace - назад/выход"
                + "\n\n";
        private const string DialogShownErrorMessage = "Dialog#{0} is already shown";
        private const string EmptyDialogErrorMessage = "Dialog#{0} has no options";
        private const string PressAnyKeyToContinueMessage = "\nНажмите любую клавишу для продолжения...";
    }
}