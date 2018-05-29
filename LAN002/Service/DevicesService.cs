using LAN002.DTO;
using PacketDotNet;
using SharpPcap;
using SharpPcap.LibPcap;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static SharpPcap.LibPcap.Sockaddr;

namespace LAN002.Service
{
    class DevicesService
    {
        CaptureDeviceList _devices;
        ICaptureDevice _device;
        
        DeviceInfo _deviceInfo;
        ArrayList _devicesDescriptions = new ArrayList();
        ArrayList _devicesInfo = new ArrayList();

        public CaptureDeviceList Devices
        {
            get
            {
                return _devices;
            }
        }

        public ArrayList DevicesDescriptions
        {
            get
            {
                return _devicesDescriptions;
            }
        }

        public ArrayList DevicesInfo
        {
            get
            {
                return _devicesInfo;
            }
        }

        public ICaptureDevice Device
        {
            get
            {
                return _device;
            }
        }

        public DeviceInfo DeviceInfo
        {
            get
            {
                return _deviceInfo;
            }
        }

        public DevicesService()
        {
            _devices = getDevices();
            _devicesDescriptions = getDevicesDescription();
            _devicesInfo = getAllDeviceInfo();
        }

        private CaptureDeviceList getDevices()
        {
            return CaptureDeviceList.Instance;
        }

        private ArrayList getDevicesDescription()
        {
            ArrayList descriptions = new ArrayList();
            foreach (var device in _devices)
            {
                descriptions.Add(device.Description);
            }
            return descriptions;
        }

        private DeviceInfo getDeviceInfo(PcapDevice device)
        {
            DeviceInfo info = new DeviceInfo();
            info.Name = device.Interface.Name;
            info.FriendlyName = device.Interface.FriendlyName;
            info.MacAddress = device.Interface.MacAddress.ToString();
            var gat = device.Interface.GatewayAddress;
            info.GatewayAddress = (gat == null) ? string.Empty : gat.ToString(); ;
            info.Description = device.Interface.Description;
            foreach (var adr in device.Interface.Addresses)
            {
                if (adr.Addr.type == AddressTypes.AF_INET_AF_INET6)
                {
                    var ip = adr.Addr.ipAddress.ToString();
                    //if (System.Net.IPAddress.TryParse(Console.ReadLine(), out ip))
                    //    break;
                    if (ip.Length <= 16)
                    {
                        info.IpV4 = ip;
                        info.Netmask = adr.Netmask.ToString();

                    }
                    else
                    {//ipv6

                    }
                }
                else if (adr.Addr.type == AddressTypes.HARDWARE)
                {
                    info.MacAddress = adr.Addr.hardwareAddress.ToString();
                }
                else
                {//UNKNOWN

                }

            }
            return info;
        }

        public DeviceInfo getDeviceInfoBYDesp(string desp)
        {
            foreach (DeviceInfo devinfo in _devicesInfo)
            {
                if (devinfo.Description.Equals(desp))
                {
                    return devinfo;
                }
            }
            return null;
        }

        public DeviceInfo getDeviceInfoBYIndex(int index)
        {

            if (_devicesInfo.Count > 0)
            {
                foreach (var dev in _devices)
                {
                    if (dev.Name.Equals(((DeviceInfo)_devicesInfo[index: index]).Name))
                    {
                        _device = dev;
                        break;
                    }
                }
                _deviceInfo = _devicesInfo[index: index] as DeviceInfo;
                return _devicesInfo[index: index] as DeviceInfo;
            }
            return null;
        }

        private ArrayList getAllDeviceInfo()
        {
            ArrayList infos = new ArrayList();
            foreach (PcapDevice dev in _devices)
            {
                infos.Add(getDeviceInfo(dev));
            }
            return infos;
        }

        //private void getPackets(bool openfileFlag, bool done, ICaptureDevice _device)
        //{
        //    //_device.OnPacketArrival +=
        //    //    new PacketArrivalEventHandler(device_OnPacketArrival);

        //    if (openfileFlag == true)
        //    {
        //        _device.Open();
        //    }
        //    else
        //    {
        //        if (done == true)
        //        {
        //            try
        //            {
        //                _device.Close();
        //                done = false;
        //            }
        //            catch (Exception er)
        //            {
        //                MainWindow.GetWindow().MessageBox.Show(er.Message, "ERROR");
        //            }

        //        }
        //        // Register our handler function to the 'packet arrival' event
        //        _device.OnPacketArrival +=
        //            new PacketArrivalEventHandler(device_OnPacketArrival);

        //        // Open the device for capturing
        //        int readTimeoutMilliseconds = 1000;
        //        _device.Open(DeviceMode.Promiscuous, readTimeoutMilliseconds);
        //    }
        //    // Start the capturing process
        //    _device.StartCapture();
        //}

        //private void device_OnPacketArrival(object sender, CaptureEventArgs e)
        //{
        //    // initialvalues();
        //    int capnum = Interlocked.Increment(ref captNum);
        //    //define names of packet data
        //    int No = 0;//
        //    string Tim = " ";
        //    int Len = 0;
        //    string Typ = " ";
        //    string source = " ";
        //    string sourceMac = " ";
        //    string dest = " ";
        //    string destMac = " ";
        //    int HeaderLength = 0;
        //    int TimeToLive = 0;
        //    string protocol = " ";
        //    string Flags = " ";
        //    int sourcePort = 0;
        //    int destPort = 0;
        //    int DestPort = 0;
        //    int frameNum = 0;
        //    int framelentgh = 0;
        //    int capturelength = 0;
        //    string NextHeader = " ";

        //    // write the packet to the file
        //    packets.Add(e.Packet);
        //    Packet p = Packet.ParsePacket(e.Packet.LinkLayerType, e.Packet.Data);

        //    packetsBytes.Add(p.Bytes);
        //    //if (p.Bytes.Length > 2000) return;
        //    String y = " ";
        //    int j = 0, k = 1, z = 1, Sq = 1;
        //    string temp = " ";

        //    // retrieving data from packets
        //    string linkType = e.Packet.LinkLayerType.ToString();
        //    string epochTime = e.Packet.Timeval.ToString();
        //    DateTime dateTime = e.Packet.Timeval.Date;
        //    string timeVal = dateTime.ToString() + epochTime.Substring(epochTime.IndexOf("."), 7);
        //    int fraLen = e.Packet.Data.Length;

        //    string sMac = " ";
        //    string dMac = " ";
        //    string ethType = " ";
        //    string sIp = " ";
        //    string dIp = " ";
        //    int TTL = 0;
        //    int totalLen = 0;
        //    int ipHeadLen = 0;
        //    ushort ipChecksum = 0;
        //    int version = 4;
        //    string ipType = "";
        //    ushort sPort = 0;
        //    ushort dPort = 0;
        //    uint seqNum = 0;
        //    uint ackNum = 0;
        //    int tranHeadLen = 0;
        //    ushort windowSize = 0;
        //    ushort tranChecksum = 0;
        //    int flags = 0;
        //    int dataLen = 0;
        //    string info = " ";

        //    //func2
        //    if (e.Packet.LinkLayerType == LinkLayers.Ethernet)
        //    {

        //        EthernetPacket ethPacket = (EthernetPacket)Packet.ParsePacket(e.Packet.LinkLayerType, e.Packet.Data);

        //        sMac = ethPacket.SourceHwAddress.ToString();
        //        dMac = ethPacket.DestinationHwAddress.ToString();
        //        int[] indexs = { 10, 8, 6, 4, 2 };
        //        foreach (var index in indexs)
        //        {
        //            sMac = sMac.Insert(index, ":");
        //            dMac = dMac.Insert(index, ":");
        //        }
        //        ethType = ethPacket.Type.ToString();
        //        if (ethPacket.Type == EthernetPacketType.IpV4 || ethPacket.Type == EthernetPacketType.IpV6)
        //        {
        //            IpPacket ipPacket = (IpPacket)ethPacket.PayloadPacket;
        //            sIp = ipPacket.SourceAddress.ToString();
        //            dIp = ipPacket.DestinationAddress.ToString();
        //            TTL = ipPacket.TimeToLive;
        //            totalLen = ipPacket.TotalLength;
        //            ipHeadLen = ipPacket.HeaderLength;
        //            version = ipPacket.Version == IpVersion.IPv4 ? 4 : 6;
        //            if (version == 4)
        //            {
        //                ipChecksum = ((IPv4Packet)ipPacket).Checksum;

        //            }
        //            else
        //            {

        //            }
        //            ipType = ipPacket.Protocol.ToString();

        //            if (ipPacket.Protocol == IPProtocolType.TCP)
        //            {
        //                TcpPacket tcpPacket = (TcpPacket)ipPacket.PayloadPacket;
        //                sPort = tcpPacket.SourcePort;
        //                dPort = tcpPacket.DestinationPort;
        //                tranChecksum = tcpPacket.Checksum;
        //                tranHeadLen = tcpPacket.Header.Length;
        //                seqNum = tcpPacket.SequenceNumber;
        //                ackNum = tcpPacket.AcknowledgmentNumber;
        //                windowSize = tcpPacket.WindowSize;
        //                flags = tcpPacket.AllFlags;
        //                dataLen = tcpPacket.PayloadData.Length;

        //                info = sPort.ToString() + "->" + dPort.ToString() + ",Len=" + fraLen.ToString() + "Flags:" + p.ToString().Substring(p.ToString().LastIndexOf("{"), p.ToString().LastIndexOf("}") - p.ToString().LastIndexOf("{"));
        //                Flags = p.ToString().Substring(p.ToString().LastIndexOf("{ "), p.ToString().LastIndexOf("}"));

        //                //添加数据
        //                InsertData(capnum, timeVal, sIp, dIp, ethType, fraLen
        //                    , sPort.ToString() + "->" + dPort.ToString() + ",Len=" + fraLen.ToString() + "Flags:" + p.ToString().Substring(p.ToString().LastIndexOf("{"), p.ToString().LastIndexOf("}") - p.ToString().LastIndexOf("{")));
        //                refreshTreeViewData(capnum, fraLen, timeVal, epochTime, linkType, sMac, dMac, ethType
        //                , sIp, dIp, TTL, totalLen, ipHeadLen, ipChecksum, version, ipType, sPort, dPort, seqNum
        //                , ackNum, tranHeadLen, windowSize, tranChecksum, dataLen, info, Flags);

        //                refreshTreeViewData(No, frameNum, fraLen, capturelength, sMac, dMac, ethType, sIp, dIp, sPort, dPort, timeVal, ipHeadLen, TTL, ipType, Flags);
        //            }
        //            else if (ipPacket.Protocol == IPProtocolType.UDP)
        //            {
        //                UdpPacket udpPacket = (UdpPacket)ipPacket.PayloadPacket;
        //                sPort = udpPacket.SourcePort;
        //                dPort = udpPacket.DestinationPort;
        //                tranChecksum = udpPacket.Checksum;
        //                dataLen = udpPacket.Length;

        //                InsertData(capnum, timeVal, sIp, dIp, ethType, fraLen, sPort.ToString() + "->" + dPort.ToString() + ",Len=" + fraLen.ToString());
        //            }
        //        }
        //        else if (ethPacket.Type == EthernetPacketType.Arp)
        //        {

        //        }
        //        else if (ethPacket.Type == EthernetPacketType.ReverseArp)
        //        {

        //        }
        //        else
        //        {

        //        }
        //    }
        //}

        //private void InsertData(int no, string tim, string source, string dest, string protocol, int len, string info)
        //{
        //    PacketSimpInfo packetSimpInfo = new PacketSimpInfo();
        //    packetSimpInfo.No = no;
        //    packetSimpInfo.Time = tim;
        //    packetSimpInfo.Source = source;
        //    packetSimpInfo.Destination = dest;
        //    packetSimpInfo.Protocol = protocol;
        //    packetSimpInfo.Length = len;
        //    packetSimpInfo.Info = info;
        //    //packetSimpInfos.Add(packetSimpInfo);
        //    //OrmLite.RegisterProvider();
        //    //((List)(packetList.ItemsSource)).Add

        //    //using (var db = DB.CreateConnection())
        //    //{
        //    //    var flag = db.Insert(packetSimpInfo);
        //    //    Console.WriteLine("flag:{0}", flag);
        //    //}

        //    //using(var db = DB.CreateSqlServerConnection())
        //    //{
        //    //    db.CreateTableIfNotExists<PacketSimpInfo>();

        //    //        var flag = db.Insert(packetSimpInfo);
        //    //        Console.WriteLine("flag:{0}", flag);
        //    //}
        //    MainWindow.refreshGridData(packetSimpInfo);
        //    ThreadPool.QueueUserWorkItem(delegate
        //    {
        //        MainWindow.Dispatcher.Invoke(new Action(() =>
        //        {
        //            packetSimpInfos.Add(packetSimpInfo);
        //        }), null);
        //    });
        //}

    }
}
