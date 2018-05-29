using SharpPcap;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using PacketDotNet;
using SharpPcap.LibPcap;
using Microsoft.Win32;
//using Loogn.OrmLite;

using LAN002.DTO;
using System.Threading;
using System.Collections.ObjectModel;
using LAN002.DAL;
using LAN002.Windows.Statistics;
using LAN002.Windows.Init;
using System.IO;
using LAN002.Windows.LogManager;
using SharpPcap.WinPcap;
using Microsoft.Research.DynamicDataDisplay.Charts;
using System.Drawing;
using System.Windows.Media;

namespace LAN002
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        static CaptureFileWriterDevice captureFileWriterDevice = null;
        static int tcp = 0, udp = 0, icmp = 0, igmp = 0, arp = 0, other = 0;

        public static ICaptureDevice _device;
        WinPcapDevice _device_stat;
        List<string[]> trees = new List<string[]>();
        List<string> PacketListInfo = new List<string>();
        List<byte[]> packetsBytes = new List<byte[]>();
        List<RawCapture> packets = new List<RawCapture>();
        ObservableCollection<PacketSimpInfo> packetSimpInfos = new ObservableCollection<PacketSimpInfo>();
        List<PacketCompInfo> packetCompInfos = new List<PacketCompInfo>();
        List<PacketInfo> packetInfos = new List<PacketInfo>();

        private ServiceStackDBHelper serviceStackDBHelper = null;

        //ByteViewer byt = new ByteViewer();

        SaveFileDialog saveFile = new SaveFileDialog();
        OpenFileDialog openFile = new OpenFileDialog();
        bool openfileFlag = false;
        bool done = false;

        string Filt = "";
        //static int ind = 0;
        static int captNum = 0;
        static long captLen = 0;
        bool savedb = false;

        IOChartStatisticsWindow iOChartStatisticsWindow = null;
        ConversationStatisticsWindow conversationStatisticsWindow = null;
        EndpointsStatisticsWindow endpointsStatisticsWindow = null;
        PacketLengthsStatisticsWindow packetLengthsStatisticsWindow = null;
        ProtocalStatisticsWindow protocalStatisticsWindow = null;
        AttackLogWindow attackLogWindow = null;
        CaptureLogWindow captureLogWindow = null;
        RealTimeWindow realTimeWindow = null;

        static ulong oldSec = 0;
        static ulong oldUsec = 0;

        public MainWindow()
        {
            InitializeComponent();
            initView();
            initData();
        }

        private void initView()
        {
            packetList.ItemsSource = null;

            treeviewPacket.Items.Clear();
            TreeViewItem treeitem1 = new TreeViewItem();
            treeitem1.Header = "Frame ";
            treeitem1.Items.Add(new TreeViewItem() { Header = "Interface id:" });
            treeitem1.Items.Add(new TreeViewItem() { Header = "Encapsulation type: Ethernet" });
            treeitem1.Items.Add(new TreeViewItem() { Header = "Arrival Time:" });
            treeitem1.Items.Add(new TreeViewItem() { Header = "Epoch Time:" });
            treeitem1.Items.Add(new TreeViewItem() { Header = "Frame Number:" });
            treeitem1.Items.Add(new TreeViewItem() { Header = "Frame Length:" });
            treeitem1.Items.Add(new TreeViewItem() { Header = "Capture Length:" });
            TreeViewItem treeitem2 = new TreeViewItem();
            treeitem2.Header = "Ethernet II, Src:";
            treeitem2.Items.Add(new TreeViewItem() { Header = "Destination: " });
            treeitem2.Items.Add(new TreeViewItem() { Header = "Source:" });
            treeitem2.Items.Add(new TreeViewItem() { Header = "Type:" });
            TreeViewItem treeitem3 = new TreeViewItem();
            treeitem3.Header = "Internet Protocol Version";
            treeitem3.Items.Add(new TreeViewItem() { Header = "Version:" });
            treeitem3.Items.Add(new TreeViewItem() { Header = "Header Length:" });
            treeitem3.Items.Add(new TreeViewItem() { Header = "Time to live:" });
            treeitem3.Items.Add(new TreeViewItem() { Header = "Protocol:" });
            treeitem3.Items.Add(new TreeViewItem() { Header = "Source:" });
            treeitem3.Items.Add(new TreeViewItem() { Header = "Destination: " });
            TreeViewItem treeitem4 = new TreeViewItem();
            treeitem4.Header = "Transmission Control Protocol, Src Port:";
            treeitem4.Items.Add(new TreeViewItem() { Header = "Source Port: " });
            treeitem4.Items.Add(new TreeViewItem() { Header = "Destination Port: " });
            treeitem4.Items.Add(new TreeViewItem() { Header = "Flags:" });
            TreeViewItem treeitem5 = new TreeViewItem();
            treeitem5.Header = "Data";
            treeviewPacket.Items.Add(treeitem1);
            treeviewPacket.Items.Add(treeitem2);
            treeviewPacket.Items.Add(treeitem3);
            treeviewPacket.Items.Add(treeitem4);
            treeviewPacket.Items.Add(treeitem5);
        }

        private void initData()
        {
            _device_stat = (WinPcapDevice)Globals.Device;
            _device = Globals.Device;
            //packetList.LoadingRow += new EventHandler<C1.WPF.DataGrid.DataGridRowEventArgs>(packetList_loadingRow);
            packetList.ItemsSource = packetSimpInfos;
            serviceStackDBHelper = ServiceStackDBHelper.getInstance();
            serviceStackDBHelper.ConductBefore();
            getPackets();
        }


        private void getPackets()
        {
            //_device.OnPacketArrival +=
            //    new PacketArrivalEventHandler(device_OnPacketArrival);

            if (openfileFlag == true)
            {
                _device.Open();
                _device_stat.Open();
            }
            else
            {
                if (done == true)
                {
                    try
                    {
                        _device.Close();
                        _device_stat.Close();
                        done = false;
                    }
                    catch (Exception er)
                    {
                        MessageBox.Show(er.Message, "ERROR");
                    }

                }
                // Register our handler function to the 'packet arrival' event
                _device.OnPacketArrival +=
                    new PacketArrivalEventHandler(device_OnPacketArrival);
                _device_stat.OnPcapStatistics += new StatisticsModeEventHandler(device_OnPcapStatistics);
                // Open the device for capturing
                int readTimeoutMilliseconds = 1000;
                //_device_stat.Open();
                //_device_stat.Mode = SharpPcap.WinPcap.CaptureMode.Statistics;
                _device.Open(DeviceMode.Promiscuous, readTimeoutMilliseconds);
            }
            // Start the capturing process
            _device.StartCapture();

            Globals.StartTime = DateTime.Now;
        }



        private void device_OnPcapStatistics(object sender, StatisticsModeEventArgs e)
        {
            //ulong delay = (e.Statistics.Timeval.Seconds - oldSec) * 1000000 - oldUsec + e.Statistics.Timeval.MicroSeconds;

            //ulong bps = ((ulong)e.Statistics.RecievedBytes * 8 * 1000000) / delay;
            //ulong pps = ((ulong)e.Statistics.RecievedPackets * 1000000) / delay;

            //var ts = e.Statistics.Timeval.Date;
            int seconds = (int)((DateTime.Now - Globals.StartTime).TotalSeconds);
            System.Windows.Point point = new System.Windows.Point();
            point.X = seconds;
            point.Y = e.Statistics.RecievedPackets;
            RealTimeWindow._plist.AppendAsync(base.Dispatcher, point);
            point.Y = e.Statistics.RecievedBytes;
            RealTimeWindow._blist.AppendAsync(base.Dispatcher, point);
            Console.WriteLine("{0},{1},{2}", seconds, e.Statistics.RecievedPackets, e.Statistics.RecievedBytes);

            //oldSec = e.Statistics.Timeval.Seconds;
            //oldUsec = e.Statistics.Timeval.MicroSeconds;
        }

        private void device_OnPacketArrival(object sender, CaptureEventArgs e)
        {
            doExtract1(e);
        }


        //private void LinkLayerExtract(CaptureEventArgs e){
        //    int capNum = Interlocked.Increment(ref captNum);
        //    Packet p = Packet.ParsePacket(e.Packet.LinkLayerType, e.Packet.Data);
        //    DateTime dateTime = e.Packet.Timeval.Date.AddHours(8);
        //    if (e.Packet.LinkLayerType == LinkLayers.Ethernet){
        //        EthernetPacket ethPacket = (EthernetPacket)Packet.ParsePacket(e.Packet.LinkLayerType, e.Packet.Data);
        //        string sMac = ethPacket.SourceHwAddress.ToString();
        //        string dMac = ethPacket.DestinationHwAddress.ToString();
        //        string ethType = ethPacket.Type.ToString();
        //        EthernetInfo ethernetInfo = new EthernetInfo(capNum,dateTime,sMac,dMac,ethType);
        //        DBHelper.InsertEthernet(ethernetInfo);
        //        switch(ethPacket.Type){
        //            case EthernetPacketType.IpV4:
        //            case EthernetPacketType.IpV6:
        //                IpLayerExtract(capNum,(IpPacket)ethPacket.PayloadPacket);
        //                break;
        //            case EthernetPacketType.Arp:
        //                ARPExtract(capNum,(ARPPacket)ethPacket.PayloadPacket);
        //                break;
        //            default:
        //        }
        //    }
        //}


        private void doExtract1(CaptureEventArgs e)
        {
            // initialvalues();
            int capnum = Interlocked.Increment(ref captNum);
            captLen += e.Packet.Data.Length;
            //define names of packet data

            {
                //int No = 0;//
                //string Tim = " ";
                //int Len = 0;
                //string Typ = " ";
                //string source = " ";
                //string sourceMac = " ";
                //string dest = " ";
                //string destMac = " ";
                //int HeaderLength = 0;
                //int TimeToLive = 0;
                //string protocol = " ";
                //string Flags = " ";
                //int sourcePort = 0;
                //int destPort = 0;
                //int DestPort = 0;
                //int frameNum = 0;
                //int framelentgh = 0;
                //int capturelength = 0;
                //string NextHeader = " ";
            }

            // write the packet to the file
            packets.Add(e.Packet);
            Packet p = Packet.ParsePacket(e.Packet.LinkLayerType, e.Packet.Data);

            //packetsBytes.Add(p.Bytes);
            //String y = " ";
            //int j = 0, k = 1, z = 1, Sq = 1;
            //string temp = " ";

            // retrieving data from packets

            {

                //if (p != null)
                //{
                //    y = p.ToString();

                //    for (int i = 0; i < y.Length; i++)
                //    {
                //        if (y[i] == ','||y[i]==']')
                //            j++;

                //        if (y[i] == '=')
                //            k = i;

                //        if (y[i] == ']')
                //            Sq = i;

                //        if (j < 11)
                //        {
                //            if (y[i] == ',')
                //                z = i;
                //        }
                //        else
                //        {
                //            z = (y.Length - 1);
                //        }
                //        if ((z - k) < y.Length && (z - k) > 0)
                //            temp = y.Substring(k + 1, (z - k) - 1);

                //        if (j == 1) //ethernet source
                //            sourceMac = temp;

                //        if (j == 2) //ethernet destination
                //            destMac = temp;

                //        if (j == 3) //ethernet type
                //        {
                //            if ((Sq - k) < y.Length && (Sq - k) > 0)
                //                temp = y.Substring(k + 1, (Sq - k) - 1);

                //            Typ = temp;
                //        }

                //        if (j == 4) //IP source
                //        {
                //            source = temp;
                //        }
                //        if (j == 5) //IP destination
                //        {
                //            dest = temp;
                //        }
                //        if (j == 6) //IP Header length
                //        {
                //            if (Sq > z)
                //            {
                //                if ((Sq - k) < y.Length && (Sq - k) > 0)
                //                    temp = y.Substring(k + 1, (Sq - k) - 1);
                //                NextHeader = temp;
                //            }
                //            else
                //            {
                //                HeaderLength = int.Parse(temp);
                //            }
                //        }

                //        if (j == 7) //IP Protocol
                //        {
                //            protocol = temp;
                //        }
                //        if (j == 8) //IP Time To Live
                //        {
                //            if (Sq > z)
                //            {
                //                if ((Sq - k) < y.Length && (Sq - k) > 0)
                //                    temp = y.Substring(k + 1, (Sq - k) - 1);
                //                TimeToLive = int.Parse(temp);
                //            }
                //            else
                //            {
                //                TimeToLive = int.Parse(temp);
                //            }
                //        }
                //        if (j == 9) //Transmission source port
                //        {
                //            sourcePort = int.Parse(temp);
                //        }
                //        if (j == 10) //Transmission destination port
                //        {
                //            if (Sq > z)
                //            {
                //                if ((Sq - k) < y.Length && (Sq - k) > 0)
                //                    temp = y.Substring(k + 1, (Sq - k) - 1);
                //                DestPort = int.Parse(temp);
                //            }
                //            else
                //            {
                //                destPort = int.Parse(temp);
                //            }

                //        }
                //        if (j == 11) //Transmission flags
                //        {
                //            Flags = temp;
                //        }
                //        // temp = " ";
                //    }
                //}
            }
            string packetstr = " ";
            try
            {

                packetstr = p.ToString();
            }
            catch (Exception)
            {
                return;
            }
            string linkType = e.Packet.LinkLayerType.ToString();
            string epochTime = e.Packet.Timeval.ToString();
            DateTime dateTime = e.Packet.Timeval.Date.AddHours(8); //加时差
            //dateTime.
            double seconds = (dateTime - Globals.StartTime).TotalSeconds;
            string timeVal = dateTime.ToString() + epochTime.Substring(epochTime.IndexOf("."), 7);
            string testtime = dateTime.ToString();
            int fraLen = e.Packet.Data.Length;
            int linkHeadLen = 0;
            string sMac = " ";
            string dMac = " ";
            string ethType = " ";
            string sIp = " ";
            string dIp = " ";
            int TTL = 0;
            int IpTotalLen = 0;
            int ipHeadLen = 0;
            int ipChecksum = 0;
            int version = 4;
            string ipType = "";
            int sPort = 0;
            int dPort = 0;
            long seqNum = 0;
            long ackNum = 0;
            int tranTotalLen = 0;
            int tranHeadLen = 0;
            int windowSize = 0;
            int tranChecksum = 0;
            int flags = 0;
            int dataLen = 0;
            string info = " ";
            string flag = " ";
            bool compMAC = false;
            bool compIP = false;
            //func2
            if (e.Packet.LinkLayerType == LinkLayers.Ethernet)
            {

                EthernetPacket ethPacket = (EthernetPacket)Packet.ParsePacket(e.Packet.LinkLayerType, e.Packet.Data);

                sMac = ethPacket.SourceHwAddress.ToString();
                dMac = ethPacket.DestinationHwAddress.ToString();
                linkHeadLen = ethPacket.Header.Length;
                int[] indexs = { 10, 8, 6, 4, 2 };
                foreach (var index in indexs)
                {
                    sMac = sMac.Insert(index, ":");
                    dMac = dMac.Insert(index, ":");
                }
                if (sMac.CompareTo(dMac) > 0) compMAC = true;
                ethType = ethPacket.Type.ToString();
                if (ethPacket.Type == EthernetPacketType.IpV4 || ethPacket.Type == EthernetPacketType.IpV6)
                {

                    IpPacket ipPacket = (IpPacket)ethPacket.PayloadPacket;
                    sIp = ipPacket.SourceAddress.ToString();
                    dIp = ipPacket.DestinationAddress.ToString();
                    TTL = ipPacket.TimeToLive;
                    IpTotalLen = ipPacket.TotalLength;
                    ipHeadLen = ipPacket.HeaderLength * 4;

                    //System.Net.IPAddress x = System.Net.IPAddress.Parse(sIp);
                    //var v = x.GetAddressBytes();
                    //var s = BitConverter.ToUInt64(x.GetAddressBytes(), 0);
                    //var d = BitConverter.ToUInt64(ipPacket.DestinationAddress.GetAddressBytes(), 0);
                    //Console.WriteLine("{0}\t{1}\n{2}\t{3}\t", sIp, s, dIp, d);
                    version = ipPacket.Version == IpVersion.IPv4 ? 4 : 6;
                    if (version == 4)
                    {
                        ipChecksum = ((IPv4Packet)ipPacket).Checksum;
                        if (Globals.ipToLong(sIp) > Globals.ipToLong(dIp)) compIP = true;
                    }
                    else
                    {

                    }
                    ipType = ipPacket.Protocol.ToString();
                    tranTotalLen = ipPacket.PayloadLength;
                    if (ipPacket.Protocol == IPProtocolType.TCP)
                    {
                        tcp++;

                        TcpPacket tcpPacket = (TcpPacket)ipPacket.PayloadPacket;

                        sPort = tcpPacket.SourcePort;
                        dPort = tcpPacket.DestinationPort;
                        tranChecksum = tcpPacket.Checksum;
                        tranHeadLen = tcpPacket.Header.Length;
                        seqNum = tcpPacket.SequenceNumber;
                        ackNum = tcpPacket.AcknowledgmentNumber;
                        windowSize = tcpPacket.WindowSize;
                        flags = tcpPacket.AllFlags;
                        dataLen = tcpPacket.PayloadData.Length;

                        int st = packetstr.IndexOf("Flag");
                        if (st >= 0)
                        {
                            int en = packetstr.IndexOf("}");
                            if (en > st)
                            {
                                flag = packetstr.Substring(st, en - st + 1);
                            }
                            else
                            {
                                flag = "";
                            }
                        }
                        else
                        {
                            flag = "";
                        }
                        info = sPort.ToString() + " -> " + dPort.ToString() + "  Len = " + fraLen.ToString() + "  " + flag;

                        //info = sPort.ToString() + "->" + dPort.ToString() + ",Len=" + fraLen.ToString() + "Flags:" + p.ToString().Substring(p.ToString().LastIndexOf("{"), p.ToString().LastIndexOf("}") - p.ToString().LastIndexOf("{"));
                        //flag = p.ToString().Substring(p.ToString().LastIndexOf("{ "), p.ToString().LastIndexOf("}"));

                        //添加数据
                        refreshData(capnum, fraLen, dateTime, seconds, timeVal, epochTime, linkType, sMac, dMac, linkHeadLen, ethType
                       , sIp, dIp, TTL, IpTotalLen, ipHeadLen, ipChecksum, version, ipType, sPort, dPort, seqNum
                       , ackNum, tranTotalLen, tranHeadLen, windowSize, tranChecksum, dataLen, info, flag, compMAC, compIP);

                    }
                    else if (ipPacket.Protocol == IPProtocolType.UDP)
                    {
                        udp++;

                        UdpPacket udpPacket = (UdpPacket)ipPacket.PayloadPacket;
                        sPort = udpPacket.SourcePort;
                        dPort = udpPacket.DestinationPort;
                        tranChecksum = udpPacket.Checksum;
                        tranHeadLen = udpPacket.Header.Length;
                        dataLen = udpPacket.Length;
                        info = sPort.ToString() + " -> " + dPort.ToString() + "  Len = " + fraLen.ToString();

                        //refreshGridData(capnum, timeVal, sIp, dIp, ethType, fraLen,info );
                        //refreshTreeViewData(capnum, capnum, fraLen, fraLen, epochTime, sMac, dMac, ethType, sIp, dIp, sPort, dPort, timeVal, ipHeadLen, TTL, ipType, Flags);
                        refreshData(capnum, fraLen, dateTime, seconds, timeVal, epochTime, linkType, sMac, dMac, linkHeadLen, ethType
                        , sIp, dIp, TTL, IpTotalLen, ipHeadLen, ipChecksum, version, ipType, sPort, dPort, seqNum
                        , ackNum, tranTotalLen, tranHeadLen, windowSize, tranChecksum, dataLen, info, flag, compMAC, compIP);
                    }
                    else if (ipPacket.Protocol == IPProtocolType.ICMP || ipPacket.Protocol == IPProtocolType.ICMPV6)
                    {
                        icmp++;
                    }
                    else if (ipPacket.Protocol == IPProtocolType.IGMP)
                    {
                        igmp++;
                    }
                }
                else if (ethPacket.Type == EthernetPacketType.Arp)
                {
                    arp++;
                    ARPPacket aRPPacket = (ARPPacket)ethPacket.PayloadPacket;
                    sMac = aRPPacket.SenderHardwareAddress.ToString();
                    dMac = aRPPacket.TargetHardwareAddress.ToString();

                }
                else if (ethPacket.Type == EthernetPacketType.ReverseArp)
                {

                }
                else
                {

                }
                //new Thread(new ThreadStart(() =>
                //{

                //    ThreadPool.QueueUserWorkItem(delegate
                //    {
                //        this.Dispatcher.Invoke(new Action(() =>
                //        {

                //        }), null);
                //    });

                total.Dispatcher.Invoke(() => total.Content = capnum);

                l_tcp.Dispatcher.Invoke(() => l_tcp.Content = tcp);
                pb_tcp.Dispatcher.Invoke(() => pb_tcp.Value = tcp * 100 / capnum);
                p_tcp.Dispatcher.Invoke(() => p_tcp.Content = tcp * 100 / capnum + "%");

                l_udp.Dispatcher.Invoke(() => l_udp.Content = udp);
                pb_udp.Dispatcher.Invoke(() => pb_udp.Value = udp * 100 / capnum);
                p_udp.Dispatcher.Invoke(() => p_udp.Content = udp * 100 / capnum + "%");

                l_icmp.Dispatcher.Invoke(() => l_icmp.Content = icmp);
                pb_icmp.Dispatcher.Invoke(() => pb_icmp.Value = icmp * 100 / capnum);
                p_icmp.Dispatcher.Invoke(() => p_icmp.Content = icmp * 100 / capnum + "%");

                l_igmp.Dispatcher.Invoke(() => l_igmp.Content = igmp);
                pb_igmp.Dispatcher.Invoke(() => pb_igmp.Value = igmp * 100 / capnum);
                p_igmp.Dispatcher.Invoke(() => p_igmp.Content = igmp * 100 / capnum + "%");


                l_arp.Dispatcher.Invoke(() => l_arp.Content = arp);
                pb_arp.Dispatcher.Invoke(() => pb_arp.Value = arp * 100 / capnum);
                p_arp.Dispatcher.Invoke(() => p_arp.Content = arp * 100 / capnum + "%");

                this.Dispatcher.Invoke(() => other = capnum - tcp - udp - icmp - igmp - arp);

                l_other.Dispatcher.Invoke(() => l_other.Content = other);
                pb_other.Dispatcher.Invoke(() => pb_other.Value = other * 100 / captNum);
                p_other.Dispatcher.Invoke(() => p_other.Content = other * 100 / captNum + "%");

                //})).Start();
            }


            {

                //DateTime time = e.Packet.Timeval.Date;
                //var len = e.Packet.Data.Length;
                //Tim = time.Hour.ToString() + ":" + time.Minute.ToString() + ":" + time.Second.ToString() + "." + time.Millisecond.ToString();
                //Len = len;
                //framelentgh = Len;
                //capturelength = framelentgh;
                //ind++;
                //No = ind;
                ////handling IPV6
                //if (Typ == "IpV6")
                //{
                //    //ind++;
                //    PacketListInfo.Add(y);
                //    //packetarrayinfo[ind] = y;



                //    destPort = TimeToLive;
                //    sourcePort = 0;

                //    frameNum = No;
                //    protocol = NextHeader;
                //    HeaderLength =0;
                //    TimeToLive = 0;
                //    Flags =" ";
                //    Tim = time.ToString()+"."+time.Millisecond.ToString();

                //    //making filter
                //    if (Filt != "")
                //    {
                //        if (protocol != Filt)
                //        {
                //            return;
                //        }
                //    }

                //    // adding data to list view
                //    refreshGridData(No, Tim, source, dest, ethType, fraLen, sPort.ToString()+"->"+dPort.ToString());

                //    refreshTreeViewData(No, frameNum, fraLen, capturelength, sMac, dMac, ethType, sIp, dIp, sPort, dPort, timeVal, ipHeadLen, TTL, ipType, Flags);


                //    //// add data of packet to treeview
                //    //drawTreeView(No, frameNum, framelentgh, capturelength, sourceMac, destMac, Typ, source, dest, sourcePort, destPort, Tim, HeaderLength, TimeToLive, protocol, Flags);

                //    //addItemsToList(No, frameNum, framelentgh, capturelength, sourceMac, destMac, Typ, source, dest, sourcePort, destPort, Tim, HeaderLength, TimeToLive, protocol, Flags);

                //}

                ////handling IPV4
                //else if (Typ == "IpV4")
                //{
                //    //ind++;
                //    PacketListInfo.Add(y);
                //    //packetarrayinfo[ind] = y;
                //    //No = ind.ToString();
                //    frameNum = No;
                //    Tim = time.ToString() + "." + time.Millisecond.ToString();
                //    if (protocol == "UDP")
                //        destPort = DestPort;

                //    //making filter
                //    if (Filt != "")
                //    {
                //        if (protocol != Filt)
                //        {
                //            return;
                //        }
                //    }
                //    // adding data to list view
                //    string info = sourcePort + " -> " + destPort + " , Len=" + len+"   ";
                //    if (protocol == "TCP") info += Flags;
                //    refreshGridData(No, timeVal, sIp, dIp, protocol, Len, p.ToString());

                //    refreshTreeViewData(No, frameNum, framelentgh, capturelength, sourceMac, destMac, Typ, source, dest, sourcePort, destPort, Tim, HeaderLength, TimeToLive, protocol, Flags);
                //    //// add data of packet to treeview
                //    //drawTreeView(No, frameNum, framelentgh, capturelength, sourceMac, destMac, Typ, source, dest, sourcePort, destPort, Tim, HeaderLength, TimeToLive, protocol, Flags);

                //    //addItemsToList(No, frameNum, framelentgh, capturelength, sourceMac, destMac, Typ, source, dest, sourcePort, destPort, Tim, HeaderLength, TimeToLive, protocol, Flags);

                //}
            }

        }

        private void refreshData(int capnum, int fraLen, DateTime dateTime, double seconds, string timeVal, string epochTime
            , string linkType, string sMac, string dMac, int linkHeadLen, string ethType, string sIp, string dIp, int TTL, int IpTotalLen
            , int ipHeadLen, int ipChecksum, int version, string ipType, int sPort, int dPort, long seqNum, long ackNum
            , int tranTotalLen, int tranHeadLen, int windowSize, int tranChecksum, int dataLen, string info, string flag, bool compMAC, bool compIP)
        {

            //1插入简单
            PacketSimpInfo packetSimpInfo = new PacketSimpInfo(capnum, timeVal, sIp, dIp, ipType, fraLen, info);

            ThreadPool.QueueUserWorkItem(delegate
            {
                this.Dispatcher.Invoke(new Action(() =>
                {
                    packetSimpInfos.Add(packetSimpInfo);
                }), null);
            });


            //2.插入复杂
            PacketCompInfo packetCompInfo = new PacketCompInfo(capnum, capnum, fraLen, fraLen, epochTime, sMac, dMac, ethType, sIp, dIp, sPort, dPort, timeVal, ipHeadLen, TTL, ipType, flag);
            packetCompInfos.Add(packetCompInfo);

            //3。插入完整
            PacketInfo packetInfo = new PacketInfo(capnum, fraLen, dateTime, seconds, timeVal, epochTime, linkType, sMac, dMac, ethType
                        , sIp, dIp, TTL, IpTotalLen, ipHeadLen, ipChecksum, version, ipType, sPort, dPort, seqNum
                        , ackNum, tranHeadLen, windowSize, tranChecksum, dataLen, info, flag);
            packetInfos.Add(packetInfo);
            //4.插入实时 (更改datetime）
            RealTimeInfo realTimeInfo = new RealTimeInfo(capnum, new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second), seconds, ethType, fraLen, ipType);

            //5.插入实时本地
            //System.Object lockthis = (System.Object)seconds;
            //lock (lockthis)
            //{
            if (!IOChartStatisticsWindow.RealTimeInfos.ContainsKey((int)seconds))
            {
                RealChartItem realChartItem = new RealChartItem((int)seconds, dateTime, 1, fraLen);
                IOChartStatisticsWindow.RealTimeInfos.Add((int)seconds, realChartItem);
            }
            else
            {
                IOChartStatisticsWindow.RealTimeInfos[(int)seconds].Count++;
                IOChartStatisticsWindow.RealTimeInfos[(int)seconds].SumLen += fraLen;
            }
            //}

            //统计1插入
            StatisticsInfo statisticsInfo = null;
            if (compIP == true)
            {//换
                if (compMAC == true)
                    statisticsInfo = new StatisticsInfo(fraLen, seconds, linkType, dMac, sMac, linkHeadLen, ethType, dIp, sIp, IpTotalLen, ipHeadLen, ipType, dPort, sPort, tranTotalLen, tranHeadLen, dataLen, compMAC, compIP);
                else
                    statisticsInfo = new StatisticsInfo(fraLen, seconds, linkType, sMac, dMac, linkHeadLen, ethType, dIp, sIp, IpTotalLen, ipHeadLen, ipType, dPort, sPort, tranTotalLen, tranHeadLen, dataLen, compMAC, compIP);
            }
            else
            {
                if (compMAC == true)
                    statisticsInfo = new StatisticsInfo(fraLen, seconds, linkType, dMac, sMac, linkHeadLen, ethType, sIp, dIp, IpTotalLen, ipHeadLen, ipType, sPort, dPort, tranTotalLen, tranHeadLen, dataLen, compMAC, compIP);
                else
                    statisticsInfo = new StatisticsInfo(fraLen, seconds, linkType, sMac, dMac, linkHeadLen, ethType, sIp, dIp, IpTotalLen, ipHeadLen, ipType, sPort, dPort, tranTotalLen, tranHeadLen, dataLen, compMAC, compIP);
            }

            //5 更新数据库
            //serviceStackDBHelper.InsertAllTable(packetSimpInfo, packetCompInfo, packetInfo, realTimeInfo);
            new Thread(() =>
            {
                serviceStackDBHelper.InsertSimpPcap(packetSimpInfo);
                serviceStackDBHelper.InsertCompPcap(packetCompInfo);
                serviceStackDBHelper.InsertPcap(packetInfo);
                serviceStackDBHelper.InsertRT(realTimeInfo);
                serviceStackDBHelper.InsertStatistics(statisticsInfo);
            }).Start();

        }

        private void packetList_SelectionChanged(object sender, C1.WPF.DataGrid.DataGridSelectionChangedEventArgs e)
        {
            //foreach(var treeItem in treeviewPacket.Items)
            //{
            //    ((TreeViewItem)treeItem).Items.Clear();
            //}
            int index = packetList.SelectedIndex;
            if (index < 0) return;
            PacketCompInfo packetCompInfo = packetCompInfos[index: index];
            int No = packetCompInfo.No;
            int frameNum = packetCompInfo.FrameNum;
            int framelentgh = packetCompInfo.Framelentgh;
            int capturelength = packetCompInfo.Capturelength;
            string sourceMac = packetCompInfo.SourceMac;
            string destMac = packetCompInfo.DestMac;
            string type = packetCompInfo.Type;
            string source = packetCompInfo.Source;
            string dest = packetCompInfo.Dest;
            int sourcePort = packetCompInfo.SourcePort;
            int destPort = packetCompInfo.DestPort;
            string Tim = packetCompInfo.TimeVal;
            int HeaderLength = packetCompInfo.HeaderLength;
            int TimeToLive = packetCompInfo.TimeToLive;
            string protocol = packetCompInfo.Protocol;
            string Flags = packetCompInfo.Flags;
            string epochTime = packetCompInfo.EpochTime;

            ((TreeViewItem)treeviewPacket.Items[0]).Header = "Frame " + (No + ": " + framelentgh + " bytes on wire (" + (framelentgh * 8).ToString() +
                    " bits), " + framelentgh + " bytes captured (" + (framelentgh * 8).ToString() + " bits) on interface " + Globals.Index.ToString()); ;
            ((TreeViewItem)treeviewPacket.Items[1]).Header = "Ethernet II, Src:" + (sourceMac + ", Dst: " + destMac);
            ((TreeViewItem)treeviewPacket.Items[2]).Header = "Internet Protocol Version" + (type[(type.Length - 1)].ToString() + ", Src: " + source + ", Dst: " + dest);
            if (protocol == "UDP")
            { ((TreeViewItem)treeviewPacket.Items[3]).Header = ("User Datagram Protocol, Src Port: " + sourcePort + ", Dst Port: " + destPort); }
            else if (protocol == "IGMP")
            { ((TreeViewItem)treeviewPacket.Items[3]).Header = "Internet Group Management Protocol"; }
            else if (protocol == "ICMPV6")
            { ((TreeViewItem)treeviewPacket.Items[3]).Header = "Internet Control Message Protocol v6"; }
            else { ((TreeViewItem)treeviewPacket.Items[3]).Header = "Transmission Control Protocol, Src Port:" + (sourcePort + ", Dst Port: " + destPort); }
            //add data to nades of root 0 (frame)
            ((TreeViewItem)(((TreeViewItem)treeviewPacket.Items[0]).Items[0])).Header = "Interface id: " + (Globals.Index.ToString() + " " + Globals.Device.Name.ToString());
            ((TreeViewItem)(((TreeViewItem)treeviewPacket.Items[0]).Items[2])).Header = "Arrival Time: " + Tim;
            ((TreeViewItem)(((TreeViewItem)treeviewPacket.Items[0]).Items[3])).Header = "Epoch Time: " + epochTime;
            ((TreeViewItem)(((TreeViewItem)treeviewPacket.Items[0]).Items[4])).Header = "Frame Number: " + frameNum;
            ((TreeViewItem)(((TreeViewItem)treeviewPacket.Items[0]).Items[5])).Header = "Frame Length: " + framelentgh + " bytes";
            ((TreeViewItem)(((TreeViewItem)treeviewPacket.Items[0]).Items[6])).Header = "Capture Length: " + capturelength + " bytes";
            //add data to nades of root 1 (ethernet)
            ((TreeViewItem)(((TreeViewItem)treeviewPacket.Items[1]).Items[0])).Header = "Source: " + sourceMac;
            ((TreeViewItem)(((TreeViewItem)treeviewPacket.Items[1]).Items[1])).Header = "Destination: " + destMac;
            ((TreeViewItem)(((TreeViewItem)treeviewPacket.Items[1]).Items[2])).Header = "Type: " + type;
            //add data to nades of root 2 (IP)
            ((TreeViewItem)(((TreeViewItem)treeviewPacket.Items[2]).Items[0])).Header = "Version: " + (type[(type.Length - 1)].ToString());
            ((TreeViewItem)(((TreeViewItem)treeviewPacket.Items[2]).Items[1])).Header = "Header Length: " + HeaderLength;
            ((TreeViewItem)(((TreeViewItem)treeviewPacket.Items[2]).Items[2])).Header = "Time to live: " + TimeToLive;
            ((TreeViewItem)(((TreeViewItem)treeviewPacket.Items[2]).Items[3])).Header = "Protocol: " + protocol;
            ((TreeViewItem)(((TreeViewItem)treeviewPacket.Items[2]).Items[4])).Header = "Source: " + source;
            ((TreeViewItem)(((TreeViewItem)treeviewPacket.Items[2]).Items[5])).Header = "Destination: " + dest;
            //add data to nades of root 3 (Transmission)
            if (protocol == "IGMP")
            {
                ((TreeViewItem)(((TreeViewItem)treeviewPacket.Items[3]).Items[0])).Header = "Type: " + sourcePort;
                ((TreeViewItem)(((TreeViewItem)treeviewPacket.Items[3]).Items[1])).Header = "Max Resp Time: " + destPort;
                ((TreeViewItem)(((TreeViewItem)treeviewPacket.Items[3]).Items[2])).Header = "Multicast Address: " + Flags;
            }
            else if (protocol == "ICMPV6")
            {
                ((TreeViewItem)(((TreeViewItem)treeviewPacket.Items[3]).Items[0])).Header = "Type: " + sourcePort;
                ((TreeViewItem)(((TreeViewItem)treeviewPacket.Items[3]).Items[1])).Header = "Code: 0" + destPort;
                ((TreeViewItem)(((TreeViewItem)treeviewPacket.Items[3]).Items[2])).Header = "Flags: " + Flags;
            }
            else
            {
                ((TreeViewItem)(((TreeViewItem)treeviewPacket.Items[3]).Items[0])).Header = "Source Port: " + sourcePort;
                ((TreeViewItem)(((TreeViewItem)treeviewPacket.Items[3]).Items[1])).Header = "Destination Port: " + destPort;
                ((TreeViewItem)(((TreeViewItem)treeviewPacket.Items[3]).Items[2])).Header = "Flags: " + Flags;
            }

        }



        private void stopCaptureMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _device.StopCapture();
            }
            catch (Exception ex)
            {
                _device.Close();
                _device.Open();
                string Err = ex.Message.ToString();
                Console.WriteLine("Err:{0}", Err);
            }
            finally
            {
            }
        }

        private void startCaptureMenuItem_Click(object sender, RoutedEventArgs e)
        {
            _device.StartCapture();
        }

        private void restartCaptureMenuItem_Click(object sender, RoutedEventArgs e)
        {
            packetSimpInfos.Clear();
            packetCompInfos.Clear();

            insertCaptureLog();
            captNum = 0;
            captLen = 0;
            serviceStackDBHelper.DropAllTables();
            try
            {
                _device.StopCapture();
                _device.Close();
            }
            catch (Exception)
            {

            }

            getPackets();
        }

        public void insertCaptureLog()
        {
            CaptureLog captureLog = new CaptureLog();
            captureLog.AdapterName = Globals.DeviceInfo.Name;
            captureLog.StartTime = Globals.StartTime;
            captureLog.EndTime = DateTime.Now;
            captureLog.Packets = captNum;
            captureLog.Bytes = captLen;
            serviceStackDBHelper.InsertCaptureLog(captureLog);
        }

        private void go_set_tb_Click(object sender, RoutedEventArgs e)
        {
            setFilter(filter_tb.Text);
        }

        private void filter_delete_tb_Click(object sender, RoutedEventArgs e)
        {
            filter_tb.Text = "";
        }

        private void filter_tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (filter_tb.IsFocused)
                check(filter_tb.Text);
        }

        public void setFilter(string filter)
        {
            if (_device != null)
            {
                _device.Filter = filter;
                filter_tb.Text = filter;
                Console.WriteLine("set filter:{0}", filter);
            }
        }

        private void check(string filter)
        {
            string s;
            var b = PcapDevice.CheckFilter(filter, out s);
            if (b)
            {
                System.Windows.Media.Color color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("LightGreen");

                filter_tb.Background = new SolidColorBrush(color);
                go_set_tb.IsEnabled = true;
            }
            else
            {
                System.Windows.Media.Color color = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("Red");

                filter_tb.Background = new SolidColorBrush(color);
                go_set_tb.IsEnabled = false;
            }
        }

        private void captureFilterMenuItem_Click(object sender, RoutedEventArgs e)
        {
            FilterSettingWindow filterSettingWindow = new FilterSettingWindow();
            filterSettingWindow.Show();
        }


        private void statis_Click(object sender, RoutedEventArgs e)
        {
            string name = ((MenuItem)sender).Header.ToString();
            if (name.Equals(@"I/O图表"))
            {
                iOChartStatisticsWindow = new IOChartStatisticsWindow();
                iOChartStatisticsWindow.Show();
            }
            else if (name.Equals(@"协议分级(P)"))
            {
                protocalStatisticsWindow = new ProtocalStatisticsWindow();
                protocalStatisticsWindow.Show();
            }
            else if (name.Equals(@"对话"))
            {
                conversationStatisticsWindow = new ConversationStatisticsWindow();
                conversationStatisticsWindow.Show();
            }
            else if (name.Equals(@"端点"))
            {
                endpointsStatisticsWindow = new EndpointsStatisticsWindow();
                endpointsStatisticsWindow.Show();
            }
            else if (name.Equals(@"分组长度"))
            {
                packetLengthsStatisticsWindow = new PacketLengthsStatisticsWindow();
                packetLengthsStatisticsWindow.Show();
            }
            else if (name.Equals(@"攻击日志"))
            {
                attackLogWindow = new AttackLogWindow();
                attackLogWindow.Show();
            }
            else if (name.Equals(@"捕包日志"))
            {
                captureLogWindow = new CaptureLogWindow();
                captureLogWindow.Show();
            }
            else if (name.Equals(@"异常"))
            {
                realTimeWindow = new RealTimeWindow();
                realTimeWindow.Show();
            }

        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (iOChartStatisticsWindow != null) iOChartStatisticsWindow.Close();
            if (conversationStatisticsWindow != null) conversationStatisticsWindow.Close();
            if (endpointsStatisticsWindow != null) endpointsStatisticsWindow.Close();
            if (packetLengthsStatisticsWindow != null) packetLengthsStatisticsWindow.Close();

            MessageBoxResult result = MessageBox.Show("需要保存数据库吗？", "询问", MessageBoxButton.YesNo, MessageBoxImage.Question);

            //关闭窗口
            if (result == MessageBoxResult.No)
            {
                savedb = false;

            }
            else
            {
                savedb = true;
            }
            e.Cancel = false;

        }
        private void Window_Closed(object sender, EventArgs e)
        {
            try
            {
                _device.StopCapture();
            }
            catch (Exception)
            {

            }
            if (savedb == false)
            {

                insertCaptureLog();
                Thread.Sleep(1000);

                serviceStackDBHelper.DropAllTables();
            }
        }

        private void savefileCaptureToobar_Click(object sender, RoutedEventArgs e)
        {
            new Thread(() =>
            {
                System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1)); // 当地时区
                long timeStamp = (long)(DateTime.Now - startTime).TotalMilliseconds; // 相差毫秒数

                string filename = @"E:\CapFile\capfile-" + timeStamp.ToString();
                //File.Create(filename);
                captureFileWriterDevice = new CaptureFileWriterDevice(filename);
                foreach (var p in packets)
                {
                    captureFileWriterDevice.Write(p);
                }
                packets.Clear();
                captureFileWriterDevice.Close();
            }).Start();


        }


    }
}
