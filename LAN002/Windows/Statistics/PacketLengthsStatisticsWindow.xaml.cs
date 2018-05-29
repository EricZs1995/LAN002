using LAN002.DAL;
using LAN002.DTO;
using LAN002.Windows.ViewModel;
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
namespace LAN002.Windows.Statistics
{
    /// <summary>
    /// PacketLengthsStatisticsWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PacketLengthsStatisticsWindow : Window
    {
        private static ServiceStackDBHelper serviceStackDBHelper = null;
        PacketLengthsStatisticsTreeViewModel packetLengthsStatisticsTreeViewModel = null;
        Thread thread = null;
        public PacketLengthsStatisticsWindow()
        {
            InitializeComponent();
            packetLengthsStatisticsTreeViewModel = new PacketLengthsStatisticsTreeViewModel();
            this.DataContext = packetLengthsStatisticsTreeViewModel;

            thread = new Thread(new ThreadStart(() =>
            {
                while (true)
                {
                    packetLengthsStatisticsTreeViewModel.LoadData();
                    Thread.Sleep(6000);
                }
            }));
            thread.Start();
        }

        public static PacketLengthsStatisticsTreeModel getPacketLengthStatisticsByTime(int i)
        {
            serviceStackDBHelper = ServiceStackDBHelper.getInstance();
            Tuple<int, int> tuple = GetStartEnd(i);
            PacketStatByLength packetStatByLength = serviceStackDBHelper.PacketStatisticsByLength(tuple.Item1, tuple.Item2);
            if (packetStatByLength != null)
            {
                return new PacketLengthsStatisticsTreeModel(packetStatByLength);
            }
            return null;
        }

        public static List<PacketLengthsStatisticsTreeModel> getPacketLengthStatisticsAll()
        {
            serviceStackDBHelper = ServiceStackDBHelper.getInstance();
            List<PacketLengthsStatisticsTreeModel> packetLengthsStatisticsTreeModels = new List<PacketLengthsStatisticsTreeModel>();

            PacketStatByLength packetStatByLength = null;
            for (int i = 0; i <= 10; i++)
            {
                Tuple<int, int> tuple = GetStartEnd(i);
                packetStatByLength = serviceStackDBHelper.PacketStatisticsByLength(tuple.Item1, tuple.Item2);
                packetLengthsStatisticsTreeModels.Add(new PacketLengthsStatisticsTreeModel(packetStatByLength));
            }
            return packetLengthsStatisticsTreeModels;
        }

        private static Tuple<int, int> GetStartEnd(int i)
        {
            switch (i)
            {
                case 0:
                    return new Tuple<int, int>(0, int.MaxValue);
                case 1:
                    return new Tuple<int, int>(0, 19);
                case 2:
                    return new Tuple<int, int>(20, 39);
                case 3:
                    return new Tuple<int, int>(40, 79);
                case 4:
                    return new Tuple<int, int>(80, 159);
                case 5:
                    return new Tuple<int, int>(160, 319);
                case 6:
                    return new Tuple<int, int>(320, 639);
                case 7:
                    return new Tuple<int, int>(640, 1279);
                case 8:
                    return new Tuple<int, int>(1280, 2559);
                case 9:
                    return new Tuple<int, int>(2560, 5119);
                case 10:
                    return new Tuple<int, int>(5120, int.MaxValue);
            }
            return new Tuple<int, int>(5120, int.MaxValue);
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

