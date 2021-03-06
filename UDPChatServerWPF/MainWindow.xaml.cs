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
using UDPAsynchronousChat;

namespace UDPChatServerWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UDPAsynchronousChatServer mUDPChatServer;

        public MainWindow()
        {
            InitializeComponent();
            mUDPChatServer = new UDPAsynchronousChatServer();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mUDPChatServer.StartReceivingData();
        }
    }
}
