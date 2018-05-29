using LAN002.DTO;
using LAN002.Service;
using System;
using System.Collections;
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
using System.Windows.Shapes;

namespace LAN002
{
    /// <summary>
    /// DeviceChoose.xaml 的交互逻辑
    /// </summary>
    public partial class DeviceChoose : Window
    {



        //
        ArrayList _devDespList = new ArrayList();
        DeviceInfo _deviceInfo;

        //service
        readonly DevicesService _devicesService;
        //window

        public DeviceChoose()
        {
            InitializeComponent();
            _devicesService = new DevicesService();
            //_devicesService = devicesService;
            _devDespList = _devicesService.DevicesDescriptions;
            devsDespList.ItemsSource = _devDespList;
        }

        private void devsDespList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string desp = devsDespList.SelectedItem as string;
            int index = devsDespList.SelectedIndex;
            if (index != -1)
            {
                _deviceInfo = _devicesService.getDeviceInfoBYIndex(index);
                devName.Text = _deviceInfo.Name;
                devFriendlyName.Text = _deviceInfo.FriendlyName;
                devIp.Text = _deviceInfo.IpV4;
                devMac.Text = _deviceInfo.MacAddress;
                devGateway.Text = _deviceInfo.GatewayAddress;
                devNetmask.Text = _deviceInfo.Netmask;
            }
        }

        private void chooseDevice_Click(object sender, RoutedEventArgs e)
        {
            Globals.Device = _devicesService.Device;
            Globals.DeviceInfo = _devicesService.DeviceInfo;
            Globals.DevicesService = _devicesService;
            Globals.Index = devsDespList.SelectedIndex;
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void interfaceListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
