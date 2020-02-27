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
using Microsoft.AspNetCore.SignalR.Client;

namespace SignalRClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        protected HubConnection connection;
        public MainWindow()
        {
            InitializeComponent();

            connection = new HubConnectionBuilder().WithUrl("http://150.237.93.13:5000/ChatHub").Build();
            connection.On<string, string>("GetMessage", new Action<string, string>((username, message) => GetMessage(username, message)));
        }

        private void GetMessage(string username, string message)
        {
            this.Dispatcher.Invoke(() =>
            {
                var chat = $"{username}: {message}"; // UNSURE on how this works
                MessagesListBox.Items.Add(chat);
            });
        }

        private async void Send_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await connection.InvokeAsync("BroadcastMessage", UsernameTextBox.Text, MessageTextBox.Text);
            }
            catch (Exception ex)
            {
                MessagesListBox.Items.Add(ex.Message);
            }
        }

        private async void Connect_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await connection.StartAsync();
                MessagesListBox.Items.Add("Connection Opened.");
            }
            catch
            {
                MessagesListBox.Items.Add("Connection Failed.");
            }
        }
    }
}
