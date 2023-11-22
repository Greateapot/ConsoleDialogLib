namespace ConsoleDialogTests
{
    public class ConsoleDialogTest
    {
        private static ConsoleDialog Dialog()
        {
            var dialog = new ConsoleDialog("Main Menu\nOptions:");
            var option = new ConsoleDialogOption("opt1", (_) => null);
            dialog.AddOption(option);
            dialog.RemoveOption(option); // test remove
            dialog.AddOption(option);
            dialog.AddOption(new ConsoleDialogOption(
                "child",
                (_) => ConsoleDialog.ShowDialog(ChildDialog())
            ));
            return dialog;
        }

        private static ConsoleDialog ChildDialog() =>
            new(
                "Child Dialog",
                new(){
                    new (
                        "child opt title",
                        (dialog) => ConsoleIO.WriteLine("Output: child opt executed"),
                        pauseAfterExecuted: true,
                        exitAfterExecuted: true
                    )
                },
                false
            );


        [Fact]
        public void TestConsoleDialog()
        {
            // arrange
            var consoleIO = new TestableConsoleIO();
            var result = false;

            /// enter down arrow (for up arrow test)
            consoleIO.PushKey(ConsoleKey.DownArrow);

            /// enter opt2 (child)
            consoleIO.PushKey(ConsoleKey.Enter);

            /// try back
            consoleIO.PushKey(ConsoleKey.Backspace);

            /// try wrong key
            consoleIO.PushKey(ConsoleKey.P);

            /// enter child opt1
            consoleIO.PushKey(ConsoleKey.Enter);

            /// test output (Output: ,*,$)
            consoleIO.PushTest(
                (output) => result = output != null
                && TestableConsoleIO.MatchOutput(output, "Output: ", "$") == "child opt executed"
            );

            /// pause (auto exit)
            consoleIO.PushKey(ConsoleKey.Enter);

            /// enter up arrow
            consoleIO.PushKey(ConsoleKey.UpArrow);

            /// enter opt1 (no pause, no auto exit)
            consoleIO.PushKey(ConsoleKey.Enter);

            /// enter backspace
            consoleIO.PushKey(ConsoleKey.Backspace);

            // act
            ConsoleDialog.ShowDialog(Dialog());

            // assert
            Assert.True(result);
        }
    }
}