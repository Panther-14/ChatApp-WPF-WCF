using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Cliente.ChatService;

namespace Cliente
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IChatServiceCallback
    {
        bool isConnected = false;
        ChatService.ChatServiceClient client;
        int ID;
        public MainWindow()
        {
            InitializeComponent();
        }

        void ConnectUser()
        {
            if (!isConnected)
            {
                client = new ChatServiceClient(new System.ServiceModel.InstanceContext(this));
                ID = client.Connect(textBoxUsername.Text);
                textBoxUsername.IsEnabled = false;

                bConnectDisconnect.Content = "Disconnect";
                isConnected = true;
            }
        }

        void DisconnectUser()
        {
            if (isConnected)
            {
                client.Disconnect(ID);
                client = null;
                textBoxUsername.IsEnabled = true;

                bConnectDisconnect.Content = "Connect";
                isConnected = false;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (isConnected)
            {
                DisconnectUser();
            }
            else
            {
                ConnectUser();
            }
        }

        public void MessageCallBack(string message)
        {
            listBoxChat.Items.Add(message);
            listBoxChat.ScrollIntoView(listBoxChat.Items[listBoxChat.Items.Count - 1]);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DisconnectUser();
        }

        private void TextBoxMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (client != null)
                    client.SendMessage(textBoxMessage.Text, ID);
                textBoxMessage.Text = string.Empty;
            }
        }
    }
}
