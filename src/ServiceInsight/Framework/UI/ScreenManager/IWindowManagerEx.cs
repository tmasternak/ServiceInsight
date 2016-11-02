namespace ServiceInsight.Framework.UI.ScreenManager
{
    using System.Windows;
    using Pirac;

    public interface IWindowManagerEx : IWindowManager
    {
        FileDialogResult OpenFileDialog(FileDialogModel model);

        MessageBoxResult ShowMessageBox(string message, string caption = "", MessageBoxButton button = MessageBoxButton.OK, MessageBoxImage image = MessageBoxImage.None, bool enableDontAsk = false, string help = "", MessageChoice defaultChoice = MessageChoice.OK);
    }
}