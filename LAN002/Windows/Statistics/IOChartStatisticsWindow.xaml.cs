
using LAN002.DAL;
using LAN002.DTO;
using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using ServiceStack.Text;
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
using System.Windows.Threading;

namespace LAN002.Windows.Statistics
{
    /// <summary>
    /// IOChartStatisticsWindow.xaml 的交互逻辑
    /// </summary>
    public partial class IOChartStatisticsWindow : Window
    {
        private ServiceStackDBHelper serviceStackDBHelper = null;
        private DispatcherTimer dispatcherTimer = null;
        private static Dictionary<int, RealChartItem> realTimeInfos = new Dictionary<int, RealChartItem>();
        ObservableCollection<Point> _pts = new ObservableCollection<Point>();
        ObservableDataSource<Point> _plist = new ObservableDataSource<Point>();


        ObservableCollection<Point> _bts = new ObservableCollection<Point>();
        ObservableDataSource<Point> _blist = new ObservableDataSource<Point>();

        Thread thread = null;
        private int _max = 0;
        private int _min = 0;
        internal static Dictionary<int, RealChartItem> RealTimeInfos { get => realTimeInfos; set => realTimeInfos = value; }

        public IOChartStatisticsWindow()
        {
            InitializeComponent();
            InitView();
            InitData();
        }

        private void InitView()
        {

        }

        private void InitData()
        {
            serviceStackDBHelper = ServiceStackDBHelper.getInstance();
            DateTime now = DateTime.Now;
            if (now.CompareTo(Globals.StartTime) > 0)//比开始时间晚
            {
                List<Dictionary<string, object>> beforeList = serviceStackDBHelper.GetBeforePacketStatistics(now);
                foreach (Dictionary<string, object> item in beforeList)
                {
                    item.PrintDump();
                    //foreach (KeyValuePair<string,object> kv in item.ToList())
                    //Console.WriteLine("key:{0}  value:{1}", kv.Key, kv.Value);
                }
            }
            int seconds = (int)((now - Globals.StartTime).TotalSeconds);
            Point countPoint = new Point();
            for (int i = 0; i < seconds - 4; i++)
            {
                if (RealTimeInfos.ContainsKey(i))
                {
                    countPoint.X = i;
                    countPoint.Y = RealTimeInfos[i].Count;
                    _max = Math.Max(_max, RealTimeInfos[i].Count);
                }
                else
                {
                    countPoint.X = i;
                    countPoint.Y = 0;
                }
                _pts.Add(countPoint);
            }
            _plist.AppendMany(_pts);
            Point sumlenPoint = new Point();
            for (int i = 0; i < seconds-4; i++)
            {
                if (RealTimeInfos.ContainsKey(i))
                {
                    sumlenPoint.X = i;
                    sumlenPoint.Y = RealTimeInfos[i].SumLen;
                }
                else
                {
                    sumlenPoint.X = i;
                    sumlenPoint.Y = 0;
                }
                _bts.Add(sumlenPoint);
            }
            _blist.AppendMany(_bts);
            plotterCD.AddLineGraph(_plist, Colors.Green, 2,"Packet");
            plotterBY.AddLineGraph(_blist, Colors.Red, 2, "Bytes");
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            dispatcherTimer.Tick += new EventHandler(AnimatedPlot);
            dispatcherTimer.IsEnabled = true;
        }

        private void AnimatedPlot(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now.AddSeconds(-3);
            int seconds = (int)((now - Globals.StartTime).TotalSeconds);
            Point countPoint = new Point();
            if (RealTimeInfos.ContainsKey(seconds))
            {
                countPoint.X = seconds;
                countPoint.Y = RealTimeInfos[seconds].Count;
                _max = Math.Max(_max, RealTimeInfos[seconds].Count);
            }
            else
            {
                countPoint.X = seconds;
                countPoint.Y = 0;
            }
            _plist.AppendAsync(base.Dispatcher, countPoint);
            Point lenPoint = new Point();
            if (RealTimeInfos.ContainsKey(seconds))
            {
                lenPoint.X = seconds;
                lenPoint.Y = RealTimeInfos[seconds].SumLen;
                _max = Math.Max(_max, RealTimeInfos[seconds].SumLen);
            }
            else
            {
                lenPoint.X = seconds;
                lenPoint.Y = 0;
            }
            _blist.AppendAsync(base.Dispatcher, lenPoint);
        }
        
        private void plotterCD_MouseMove(object sender, MouseEventArgs e)
        {
            ChartPlotter chart = sender as ChartPlotter;
            Point p = e.GetPosition(this).ScreenToData(chart.Transform);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            try
            {
                thread.Abort();
                dispatcherTimer.Stop();
            }
            catch (Exception)
            {

            }
        }
    }
}
