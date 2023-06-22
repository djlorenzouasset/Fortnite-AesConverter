using System.Windows;

namespace FortniteKeyChain.ViewModels;

public partial class ConvertedView
{
    public ConvertedView(string aesKey, bool is_keychain = false)
    {
        InitializeComponent();
        DataContext = new ViewModel();
        
        ConvertType.Text = is_keychain ? "Aes Key converted to KeyChain" : "KeyChain converted to Aes Key";
        AesKey.Text = aesKey;
    }

    private void Quit(object sender, RoutedEventArgs e)
    {
        Close();
    }
}
