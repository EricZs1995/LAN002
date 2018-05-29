//using C1.WPF.C1Chart;
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
    /// RealTimeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class RealTimeWindow : Window
    {

        //private ServiceStackDBHelper serviceStackDBHelper = null;
        //private DispatcherTimer dispatcherTimer = null;
        //private static Dictionary<int, RealChartItem> realTimeInfos = new Dictionary<int, RealChartItem>();
        //ObservableCollection<Point> _pts = new ObservableCollection<Point>();
        //Thread thread = null;
        //private int _max = 0;
        //private int _min = 0;
        //internal static Dictionary<int, RealChartItem> RealTimeInfos { get => realTimeInfos; set => realTimeInfos = value; }

        //public RealTimeWindow()
        //{
        //    InitializeComponent();
        //    InitView();
        //    InitData();
        //}

        //private void InitView()
        //{

        //}

        //private void InitData()
        //{
        //    //iochart.Data.ItemsSource = _pts;
        //    //iochart.ChartType = ChartType.Line;
        //    //iochart.View.AxisY.Min = 0;
        //    //iochart.View.AxisY.Max = 10;

        //    //XYDataSeries ds = new XYDataSeries()
        //    //{
        //    //    XValueBinding = new Binding("X"),
        //    //    ValueBinding = new Binding("Y"),
        //    //    ConnectionStrokeThickness = 2,
        //    //    Label = "raw",
        //    //    //        ConnectionStroke = new SolidColorBrush(Colors.DarkGray)
        //    //};

        //    //iochart.Data.Children.Add(ds);

        //    //var _tlmin = new TrendLine()
        //    //{
        //    //    FitType = FitType.MinY,
        //    //    XValueBinding = new Binding("X"),
        //    //    ValueBinding = new Binding("Y"),
        //    //    Label = "min",
        //    //    //        ConnectionStroke = new SolidColorBrush(Colors.Blue)
        //    //};

        //    //_tlmax = new TrendLine()
        //    //{
        //    //    FitType = FitType.MaxY,
        //    //    XValueBinding = new Binding("X"),
        //    //    ValueBinding = new Binding("Y"),
        //    //    Label = "max",
        //    //    //        ConnectionStroke = new SolidColorBrush(Colors.Red)
        //    //};

        //    //_tlavg = new TrendLine()
        //    //{
        //    //    FitType = FitType.AverageY,
        //    //    XValueBinding = new Binding("X"),
        //    //    ValueBinding = new Binding("Y"),
        //    //    Label = "avg",
        //    //    //        ConnectionStroke = new SolidColorBrush(Colors.Green)
        //    //};




        //    serviceStackDBHelper = ServiceStackDBHelper.getInstance();
        //    DateTime now = DateTime.Now;
        //    if (now.CompareTo(Globals.StartTime) > 0)//比开始时间晚
        //    {
        //        List<Dictionary<string, object>> beforeList = serviceStackDBHelper.GetBeforePacketStatistics(now);
        //        foreach (Dictionary<string, object> item in beforeList)
        //        {
        //            item.PrintDump();
        //            //foreach (KeyValuePair<string,object> kv in item.ToList())
        //            //Console.WriteLine("key:{0}  value:{1}", kv.Key, kv.Value);
        //        }
        //    }

        //    Console.WriteLine("----------------------------------------------------------");
        //    //dispatcherTimer = new DispatcherTimer()
        //    //{
        //    //    Interval = TimeSpan.FromSeconds(1)
        //    //};
        //    //dispatcherTimer.Tick += (s, e) => Update();
        //    //dispatcherTimer.Start();

        //    //FlushClient fc = new FlushClient(ThreadFunction);

        //    //fc.BeginInvoke(null, null);

        //    //iochart.BeginUpdate();

        //    int seconds = (int)((now - Globals.StartTime).TotalSeconds);
        //    Point countPoint = new Point();
        //    for (int i = 0; i < seconds - 5; i++)
        //    {
        //        if (RealTimeInfos.ContainsKey(i))
        //        {
        //            countPoint.X = i;
        //            countPoint.Y = RealTimeInfos[i].Count;
        //            _max = Math.Max(_max, RealTimeInfos[i].Count);
        //        }
        //        else
        //        {
        //            countPoint.X = i;
        //            countPoint.Y = 0;
        //        }
        //        //if (_max > 0.8 * iochart.View.AxisY.Max)
        //        //{
        //        //    iochart.View.AxisY.Max = _max * 3 / 2;
        //        //}
        //        _pts.Add(countPoint);
        //        Console.WriteLine("countPoint:{0}", countPoint.X.ToString() + countPoint.Y.ToString());
        //    }
        //    Point sumlenPoint = new Point();
        //    Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
        //    for (int i = 0; i < seconds; i++)
        //    {
        //        if (RealTimeInfos.ContainsKey(i))
        //        {
        //            sumlenPoint.X = i;
        //            sumlenPoint.Y = RealTimeInfos[i].SumLen;
        //        }
        //        else
        //        {
        //            sumlenPoint.X = i;
        //            sumlenPoint.Y = 0;
        //        }
        //        Console.WriteLine("sumlenPoint:{0}", sumlenPoint.X.ToString() + "->" + sumlenPoint.Y.ToString());
        //    }
        //    Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");


        //    //iochart.EndUpdate();

        //    thread = new Thread(new ThreadStart(ThreadFunction));
        //    thread.IsBackground = true;
        //    thread.Start();

        //}

        //private delegate void FlushClient(Dictionary<string, object> result);//代理

        //private void ThreadFunction()
        //{
        //    while (true)
        //    {


        //        DateTime now = DateTime.Now.AddSeconds(-3);
        //        int seconds = (int)((now - Globals.StartTime).TotalSeconds);
        //        Console.WriteLine("time:{0}   second:{1}", now.ToString(), seconds);

        //        //数据库读取
        //        //Dictionary<string, object> result = serviceStackDBHelper.GetDataByDate(new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second));
        //        //result.PrintDump();
        //        //FlushClient fc = new FlushClient(UpdateWindow);
        //        //this.Dispatcher.BeginInvoke(fc,result);

        //        //本地读取
        //        Point countPoint = new Point();
        //        if (RealTimeInfos.ContainsKey(seconds))
        //        {
        //            countPoint.X = seconds;
        //            countPoint.Y = RealTimeInfos[seconds].Count;
        //            _max = Math.Max(_max, RealTimeInfos[seconds].Count);
        //        }
        //        else
        //        {
        //            countPoint.X = seconds;
        //            countPoint.Y = 0;
        //        }


        //        ThreadPool.QueueUserWorkItem(delegate
        //        {
        //            this.Dispatcher.Invoke(new System.Action(() =>
        //            {
        //                //iochart.BeginUpdate();
        //                //if (_max > 0.8 * iochart.View.AxisY.Max)
        //                //{
        //                //    iochart.View.AxisY.Max = _max * 3 / 2;
        //                //}
        //                //_pts.Add(countPoint);
        //                //iochart.EndUpdate();
        //            }), null);
        //        });

        //        Console.WriteLine("countpoint:{0}", countPoint.X.ToString() + "->" + countPoint.Y.ToString());
        //        Point sumlenPoint = new Point();
        //        if (RealTimeInfos.ContainsKey(seconds))
        //        {
        //            sumlenPoint.X = seconds;
        //            sumlenPoint.Y = RealTimeInfos[seconds].SumLen;
        //        }
        //        else
        //        {
        //            sumlenPoint.X = seconds;
        //            sumlenPoint.Y = 0;
        //        }
        //        Console.WriteLine("sumlenPoint:{0}", sumlenPoint.X.ToString() + "->" + sumlenPoint.Y.ToString());




        //        Thread.Sleep(1000);
        //    }

        //}
        //public void UpdateWindow(Dictionary<string, object> result)
        //{
        //    //result.PrintDump();
        //    //this.test.Content = DateTime.Now.ToString() + result.Values.ToString();
        //}



        //private void CheckBox_Checked(object sender, RoutedEventArgs e)
        //{
        //    //CheckBox cb = (CheckBox)sender;
        //    //if (cb.Content as string == "Minimum")
        //    //    chart.Data.Children.Add(_tlmin);
        //    //else if (cb.Content as string == "Maximum")
        //    //    chart.Data.Children.Add(_tlmax);
        //    //else if (cb.Content as string == "Average")
        //    //    chart.Data.Children.Add(_tlavg);
        //}

        //private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        //{
        //    //CheckBox cb = (CheckBox)sender;
        //    //if (cb.Content as string == "Minimum")
        //    //    chart.Data.Children.Remove(_tlmin);
        //    //else if (cb.Content as string == "Maximum")
        //    //    chart.Data.Children.Remove(_tlmax);
        //    //else if (cb.Content as string == "Average")
        //    //    chart.Data.Children.Remove(_tlavg);
        //}



        //private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        //{
        //    thread.Abort();
        //}

            
        private DispatcherTimer dispatcherTimer = null;
        private static Dictionary<int, RealChartItem> realTimeInfos = new Dictionary<int, RealChartItem>();
        //ObservableCollection<Point> _pts = new ObservableCollection<Point>();
        public static ObservableDataSource<Point> _plist = new ObservableDataSource<Point>();


        //private static ObservableCollection<Point> _bts = new ObservableCollection<Point>();
        public static ObservableDataSource<Point> _blist = new ObservableDataSource<Point>();
        
        private int _max = 0;
        private int _min = 0;
        internal static Dictionary<int, RealChartItem> RealTimeInfos { get => realTimeInfos; set => realTimeInfos = value; }

        public RealTimeWindow()
        {
            InitializeComponent();
            tcp_p.AddLineGraph(_plist, Colors.Green, 2, "Packet");
            tcp_b.AddLineGraph(_blist, Colors.Red, 2, "Bytes");
            //InitView();
            //InitData();
        }

        private void InitView()
        {

        }

        private void InitData()
        {
            //DateTime now = DateTime.Now;
            //int seconds = (int)((now - Globals.StartTime).TotalSeconds);
            //Point countPoint = new Point();
            //for (int i = 0; i < seconds-1; i++)
            //{
            //    if (RealTimeInfos.ContainsKey(i))
            //    {
            //        countPoint.X = i;
            //        countPoint.Y = RealTimeInfos[i].Count;
            //        _max = Math.Max(_max, RealTimeInfos[i].Count);
            //    }
            //    else
            //    {
            //        countPoint.X = i;
            //        countPoint.Y = 0;
            //    }
            //    _pts.Add(countPoint);
            //}
            //_plist.AppendMany(_pts);
            //Point sumlenPoint = new Point();
            //for (int i = 0; i < seconds - 1; i++)
            //{
            //    if (RealTimeInfos.ContainsKey(i))
            //    {
            //        sumlenPoint.X = i;
            //        sumlenPoint.Y = RealTimeInfos[i].SumLen;
            //    }
            //    else
            //    {
            //        sumlenPoint.X = i;
            //        sumlenPoint.Y = 0;
            //    }
            //    _bts.Add(sumlenPoint);
            //}
            //_blist.AppendMany(_bts);
            tcp_p.AddLineGraph(_plist, Colors.Green, 2, "Packet");
            tcp_b.AddLineGraph(_blist, Colors.Red, 2, "Bytes");
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
                dispatcherTimer.Stop();
            }
            catch (Exception)
            {

            }
        }

    }



}
