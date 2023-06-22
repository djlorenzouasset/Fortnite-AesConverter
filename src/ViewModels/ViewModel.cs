using System.Diagnostics;
using System.Windows.Input;

namespace FortniteKeyChain.ViewModels;

public class ViewModel
{
    private ICommand _command;

    public ICommand ClickedCommand
    {
        get
        {
            return _command ?? (_command = new MenuCommand(OnClicked, () => CanExecute));
        }
    }

    public bool CanExecute
    {
        get
        {
            return true;
        }
    }

    public void OnClicked(object parameter)
    {
        string url = (parameter as string) switch
        {
            "keychain_url" => Globals.KEYCHAINARCHIVE,
            "github_url" => Globals.GITHUB,
            "discord_url" => Globals.DISCORD,
            _ => throw new System.ArgumentException($"Invalid parameter: {parameter as string}")
        };

        OpenLink(url);
    }

    private void OpenLink(string link)
    {
        Process.Start(new ProcessStartInfo { FileName = link, UseShellExecute = true});
    }
}