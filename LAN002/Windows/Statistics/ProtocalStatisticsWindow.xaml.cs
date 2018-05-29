using LAN002.DAL;
using System;
using System.Collections.Generic;
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
using LAN002.DTO;
using LAN002.Windows.ViewModel;

namespace LAN002.Windows.Statistics
{
    /// <summary>
    /// ProtocalStatisticsWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ProtocalStatisticsWindow : Window
    {

        private static ServiceStackDBHelper serviceStackDBHelper = null;
        ProtocalStatisticsTreeViewModel protocalStatisticsTreeViewModel = null;
        Thread thread = null;


        public ProtocalStatisticsWindow()
        {
            InitializeComponent();
            protocalStatisticsTreeViewModel = new ProtocalStatisticsTreeViewModel();
            this.DataContext = protocalStatisticsTreeViewModel;
            thread = new Thread(new ThreadStart(() =>
            {
                while (true)
                {
                    protocalStatisticsTreeViewModel.LoadData();
                    Console.WriteLine("now:{0}", DateTime.Now.ToString());
                    Thread.Sleep(3000);
                }
            }));
            thread.Start();
        }

        public static ProtocalStatByLayer getProtocalStatByLayerInfo()
        {
            serviceStackDBHelper = ServiceStackDBHelper.getInstance();
            ProtocalStatByLayer protocalStatByLayer = serviceStackDBHelper.ProtocalStatisticsByLayers();
            return protocalStatByLayer;
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
