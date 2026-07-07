namespace AutoSaveStateMaker
{
    internal static class Helper
    {
        public static void ShowError(string text)
        {
            MessageBox.Show(text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
