using LAN002.DAL;
using LAN002.DTO;
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

namespace LAN002.Windows.Statistics
{
    /// <summary>
    /// EndpointsStatisticsWindow.xaml 的交互逻辑
    /// </summary>
    public partial class EndpointsStatisticsWindow : Window
    {
        ObservableCollection<PointStatMacORIpResult> ethernetList = new ObservableCollection<PointStatMacORIpResult>();
        ObservableCollection<PointStatMacORIpResult> ipv4List = new ObservableCollection<PointStatMacORIpResult>();
        ObservableCollection<PointStatMacORIpResult> ipv6List = new ObservableCollection<PointStatMacORIpResult>();
        ObservableCollection<PointStatTranResult> tcpList = new ObservableCollection<PointStatTranResult>();
        ObservableCollection<PointStatTranResult> udpList = new ObservableCollection<PointStatTranResult>();
        private ServiceStackDBHelper serviceStackDBHelper = null;
        Thread thread = null;
        public EndpointsStatisticsWindow()
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
            PointStatMacORIpResult pointStatMacORIpResult = new PointStatMacORIpResult();
            PointStatTranResult pointStatTranResult = new PointStatTranResult();
            ethernetList.Add(pointStatMacORIpResult);
            ipv4List.Add(pointStatMacORIpResult);
            ipv6List.Add(pointStatMacORIpResult);
            tcpList.Add(pointStatTranResult);
            udpList.Add(pointStatTranResult);
        }


        private void ThreadFunction()
        {
            while (true)
            {
                ethernetList = new ObservableCollection<PointStatMacORIpResult>(serviceStackDBHelper.EndPointStatisticsByMac());
                ipv4List = new ObservableCollection<PointStatMacORIpResult>(serviceStackDBHelper.EndPointStatisticsByIpv4());
                ipv6List = new ObservableCollection<PointStatMacORIpResult>(serviceStackDBHelper.EndPointStatisticsByIpv6());
                tcpList = new ObservableCollection<PointStatTranResult>(serviceStackDBHelper.EndPointStatisticsByIpTcp());
                udpList = new ObservableCollection<PointStatTranResult>(serviceStackDBHelper.EndPointStatisticsByIpUdp());

                ThreadPool.QueueUserWorkItem(delegate
                {
                    this.Dispatcher.Invoke(new System.Action(() =>
                    {
                        ((TabItem)(endpointStatTab.Items[0])).Header = "Ethernet · " + ethernetList.Count;
                        ((TabItem)(endpointStatTab.Items[1])).Header = "IPv4 · " + ipv4List.Count;
                        ((TabItem)(endpointStatTab.Items[2])).Header = "IPv6 · " + ipv6List.Count;
                        ((TabItem)(endpointStatTab.Items[3])).Header = "TCP · " + tcpList.Count;
                        ((TabItem)(endpointStatTab.Items[4])).Header = "UDP · " + udpList.Count;
                    }), null);
                });
                Thread.Sleep(5000);
            }
        }


        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (endpointStatTab.SelectedIndex)
            {
                case 0:
                    endPointStatList.ItemsSource = ethernetList;
                    break;
                case 1:
                    endPointStatList.ItemsSource = ipv4List;
                    break;
                case 2:
                    endPointStatList.ItemsSource = ipv6List;
                    break;
                case 3:
                    endPointStatList.ItemsSource = tcpList;
                    break;
                case 4:
                    endPointStatList.ItemsSource = udpList;
                    break;
                default:
                    endPointStatList.ItemsSource = ethernetList;
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
