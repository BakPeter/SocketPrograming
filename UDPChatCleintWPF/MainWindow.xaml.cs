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

namespace UDPChatCleintWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UDPAsynchronousChat.UDPAsynchronousChatClient mChatCient;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_sendBroadcast_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Send Broadcast Clicked");
            if (mChatCient == null)
            {
                _ = int.TryParse(tbox_localPort.Text, out var nLocalPort);
                _ = int.TryParse(tbox_remotePort.Text, out var nRemotePort);

                mChatCient = new UDPAsynchronousChat.UDPAsynchronousChatClient(nLocalPort, nRemotePort);
            }

            mChatCient.SendBroadcast(tbox_broadcastText.Text);
        }
    }
}
