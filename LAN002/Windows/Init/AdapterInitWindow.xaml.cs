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

namespace LAN002.Windows.Init
{
    /// <summary>
    /// AdapterInitWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AdapterInitWindow : Window
    {

        ArrayList _devDespList = new ArrayList();
        DeviceInfo _deviceInfo;

        //service
        private DevicesService _devicesService;

        public AdapterInitWindow()
        {
            InitializeComponent();
            initData();
        }

        private void initData()
        {
            _devicesService = new DevicesService();
            //_devicesService = devicesService;
            _devDespList = _devicesService.DevicesDescriptions;
            //foreach(var desp in _devDespList)
            //{
            //    ListBoxItem listBoxItem = new ListBoxItem();
            //    listBoxItem.
            //    interlist.Items.Add()
            //}
            interlist.ItemsSource = _devDespList;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBoxItem listBoxItem = (sender as ListBox).SelectedItem as ListBoxItem;
            int index = interlist.SelectedIndex;
            if (index != -1)
            {
                _deviceInfo = _devicesService.getDeviceInfoBYIndex(index);
                string s = "Name:" + _deviceInfo.Name + @"\n" +
                    "FriendlyName:" + _deviceInfo.FriendlyName + @"\n" +
                    "IP:" + _deviceInfo.IpV4 + @"\n" +
                    "Mac:" + _deviceInfo.MacAddress + @"\n" +
                    "网关：" + _deviceInfo.GatewayAddress + @"\n" +
                    "子网掩码：" + _deviceInfo.Netmask;
                Label label = new Label();
                label.Content = s;
                ToolTip toolTip = new ToolTip();
                toolTip.Content = label;
                interlist.ToolTip = toolTip;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Globals.Device = _devicesService.Device;
            Globals.DeviceInfo = _devicesService.DeviceInfo;
            Globals.DevicesService = _devicesService;
            Globals.Index = interlist.SelectedIndex;
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void interlist_MouseMove(object sender, MouseEventArgs e)
        {
            //int idx = IndexFromPoint(e.Location);// 获取鼠标所在的项索引  
            //if (idx == -1) //鼠标所在位置没有 项  
            //{
            //    SetTipMessage(""); //设置提示信息为空  
            //    return;
            //}
            //string txt = GetItemText(this.Items[idx]); //获取项文本  
            //SetTipMessage(txt); //设置提示信息  
            ListBoxItem listBoxItem = sender as ListBoxItem;
            int index = interlist.ItemContainerGenerator.IndexFromContainer(listBoxItem);
            if (index == -1) //鼠标所在位置没有 项  
            {
                //SetTipMessage(""); //设置提示信息为空  
                return;
            }
            DeviceInfo info = _devicesService.getDeviceInfoBYIndex(index);
            string s = "Name:" + info.Name + @"\n"  +
                "FriendlyName:" +info.FriendlyName+ @"\n" +
                "IP:" +info.IpV4+ @"\n" +
                "Mac:"+info.MacAddress + @"\n" +
                "网关："+info.GatewayAddress+ @"\n" +
                "子网掩码："+info.Netmask ;
            Label label = new Label();
            label.Content = s;
            ToolTip toolTip = new ToolTip();
            toolTip.Content = label;
            listBoxItem.ToolTip = toolTip;
        }
    }
}
