using System;
using System.Windows;
using AdonisUI.Controls;

namespace FortniteKeyChain.ViewModels;

public partial class MainWindow
{ 
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new ViewModel();
    }

    private void OnClickConvert(object sender, RoutedEventArgs e)
    {
        bool? AesToHex = AesToKeyChain.IsChecked;

        if (!IsValid(Key.Text))
        {
            var errorMessageBox = new MessageBoxModel()
            {
                Caption = "Invalid input text",
                Icon = AdonisUI.Controls.MessageBoxImage.Warning,
                Text = "You must have to insert a valid KeyChain or AES Key string for convert it into AES.",
                Buttons = new[] { new MessageBoxButtonModel("Try again", AdonisUI.Controls.MessageBoxResult.Custom), new MessageBoxButtonModel("Close", AdonisUI.Controls.MessageBoxResult.Cancel) }
            };

            AdonisUI.Controls.MessageBox.Show(errorMessageBox);

            if (errorMessageBox.Result == AdonisUI.Controls.MessageBoxResult.Cancel)
            {
                Environment.Exit(0);
            }
            var convertedView = new MainWindow();
            convertedView.InitializeComponent();
        }

        else 
        {
            if (AesToHex is not null && AesToHex is true)
            {
                string keyChain = CreateKeyChain(Key.Text);
                var newWindow = new ConvertedView(keyChain, true);
                newWindow.Show();
                Close();
            }
            else
            {
                string aesKey = CreateAesKey(Key.Text);
                var newWindow = new ConvertedView(aesKey);
                newWindow.Show();
                Close();
            }
        }
    }

    private bool IsValid(string inputText) => !string.IsNullOrEmpty(inputText);

    static string CreateAesKey(string keyChain)
    {
        byte[] bytes = Convert.FromBase64String(keyChain);
        string hex = BitConverter.ToString(bytes);
        return FixAesKey(hex.Replace("-", "").ToLower());
    }

    static string CreateKeyChain(string aesKey)
    {
        string aes = RemoveAesStart(aesKey);

        byte[] hexBytes = new byte[aes.Length / 2];
        for (int i = 0; i < hexBytes.Length; i++)
        {
            hexBytes[i] = Convert.ToByte(aes.Substring(i * 2, 2), 16);
        }
        return  Convert.ToBase64String(hexBytes);
    }

    static string FixAesKey(string aesKey) => "0x" + aesKey;

    static string RemoveAesStart(string aesKey) => aesKey.Replace("0x", "");


}
