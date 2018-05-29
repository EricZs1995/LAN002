using LAN002.DAL;
using LAN002.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace LAN002.Windows.LogManager
{
    /// <summary>
    /// CaptureLogWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CaptureLogWindow : Window
    {
        private static ServiceStackDBHelper serviceStackDBHelper = null;
        List<CaptureLog> captureLogs = new List<CaptureLog>();

        public CaptureLogWindow()
        {
            InitializeComponent();
            serviceStackDBHelper = ServiceStackDBHelper.getInstance();
            captureLogs = serviceStackDBHelper.GetCaptureLogs();
            caploglist.ItemsSource = captureLogs;
        }
    }
}
