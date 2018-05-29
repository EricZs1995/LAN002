using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using LAN002.DAL;
using LAN002.DTO;
namespace LAN002.Windows.Statistics
{
    /// <summary>
    /// ConversationStatisticsWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ConversationStatisticsWindow : Window
    {
        ObservableCollection<ConvStatMacORIpResult> ethernetList = new ObservableCollection<ConvStatMacORIpResult>();
        ObservableCollection<ConvStatMacORIpResult> ipv4List = new ObservableCollection<ConvStatMacORIpResult>();
        ObservableCollection<ConvStatMacORIpResult> ipv6List = new ObservableCollection<ConvStatMacORIpResult>();
        ObservableCollection<ConvStatTranResult> tcpList = new ObservableCollection<ConvStatTranResult>();
        ObservableCollection<ConvStatTranResult> udpList = new ObservableCollection<ConvStatTranResult>();
        private ServiceStackDBHelper serviceStackDBHelper = null;
        Thread thread = null;
        public ConversationStatisticsWindow()
        {
            InitializeComponent();
            initView();
            initData();
            
            thread = new Thread(new ThreadStart(ThreadFunction));
            thread.IsBackground = true;
            thread.Start();
        }

        private void initView()
        {
        }
        private void initData()
        {
            serviceStackDBHelper = ServiceStackDBHelper.getInstance();
            ConvStatMacORIpResult convStatMacORIpResult = new ConvStatMacORIpResult();
            ConvStatTranResult convStatTranResult = new ConvStatTranResult();
            ethernetList.Add(convStatMacORIpResult);
            ipv4List.Add(convStatMacORIpResult);
            ipv6List.Add(convStatMacORIpResult);
            tcpList.Add(convStatTranResult);
            udpList.Add(convStatTranResult);
        }


        private void ThreadFunction()
        {
            while (true)
            {
                ethernetList = new ObservableCollection<ConvStatMacORIpResult>(serviceStackDBHelper.ConversationStatisticsByMac());
                ipv4List = new ObservableCollection<ConvStatMacORIpResult>(serviceStackDBHelper.ConversationStatisticsByIpv4());
                ipv6List = new ObservableCollection<ConvStatMacORIpResult>(serviceStackDBHelper.ConversationStatisticsByIpv6());
                tcpList = new ObservableCollection<ConvStatTranResult>(serviceStackDBHelper.ConversationStatisticsByIpTcp());
                udpList = new ObservableCollection<ConvStatTranResult>(serviceStackDBHelper.ConversationStatisticsByIpUdp());

                ThreadPool.QueueUserWorkItem(delegate
                {
                    this.Dispatcher.Invoke(new System.Action(() =>
                    {
                        ((TabItem)(conversationStatTab.Items[0])).Header = "Ethernet · " + ethernetList.Count;
                        ((TabItem)(conversationStatTab.Items[1])).Header = "IPv4 · " + ipv4List.Count;
                        ((TabItem)(conversationStatTab.Items[2])).Header = "IPv6 · " + ipv6List.Count;
                        ((TabItem)(conversationStatTab.Items[3])).Header = "TCP · " + tcpList.Count;
                        ((TabItem)(conversationStatTab.Items[4])).Header = "UDP · " + udpList.Count;
                    }), null);
                });
                Thread.Sleep(5000);
            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (conversationStatTab.SelectedIndex)
            {
                case 0:
                    conversationStatList.ItemsSource = ethernetList;
                    break;
                case 1:
                    conversationStatList.ItemsSource = ipv4List;
                    break;
                case 2:
                    conversationStatList.ItemsSource = ipv6List;
                    break;
                case 3:
                    conversationStatList.ItemsSource = tcpList;
                    break;
                case 4:
                    conversationStatList.ItemsSource = udpList;
                    break;
                default:
                    conversationStatList.ItemsSource = ethernetList;
                    break;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //thread.Abort();
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            try
            {
                thread.Abort();
            }
            catch (Exception)
            {

            }
        }
    }
}
