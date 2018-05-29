using Seekford.Controls.WPFTreeListView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LAN002.DTO;
using LAN002.Windows.Statistics;
using System.Threading;

namespace LAN002.Windows.ViewModel
{
    public class ProtocalStatisticsTreeViewModel : ViewModelBase, ITreeListViewFilter
    {
        //ObservableCollection<ProtocalStatByLayer> protocalStatByLayers = new ObservableCollection<ProtocalStatByLayer>();
        ProtocalStatByLayer protocalStatByLayer = null;
        Thread thread = null;
        public ProtocalStatisticsTreeViewModel()
        {
            CreateTreeRoot();

            //thread = new Thread(new ThreadStart(() =>
            //{
            //    while (true)
            //    {
            //        LoadData();
            //        Console.WriteLine("now:{0}", DateTime.Now.ToString());
            //        Thread.Sleep(6000);
            //    }
            //}));
            //thread.Start();
        }

        public void LoadData()
        {
            TreeListModel = null;
            TreeRoot = new TreeModelRoot();
            TreeRoot.IsBatchLoading = true;

            protocalStatByLayer = ProtocalStatisticsWindow.getProtocalStatByLayerInfo();
            if (protocalStatByLayer != null)
            {
                //frame
                ProtocalStatisticsTreeModel frame = new ProtocalStatisticsTreeModel()
                {
                    DisplayName = "Frame",
                    Protocal = "Frame",
                    PacketPercent = 100.0,
                    PacketNum = protocalStatByLayer.LinkNum,
                    BytesPercent = 100.0,
                    Bytes = protocalStatByLayer.FrameSum,
                    bps = Math.Round(protocalStatByLayer.FrameSum / protocalStatByLayer.Duration, 3)
                };
                //ethernet
                ProtocalStatisticsTreeModel ethernet = new ProtocalStatisticsTreeModel()
                {
                    DisplayName = "Ethernet",
                    Protocal = "Ethernet",
                    PacketPercent = 100.0,
                    PacketNum = protocalStatByLayer.LinkNum,
                    BytesPercent = Math.Round(protocalStatByLayer.LinkHeadSum * 100 / (double)protocalStatByLayer.FrameSum, 2),
                    Bytes = protocalStatByLayer.LinkHeadSum,
                    bps = Math.Round(protocalStatByLayer.LinkHeadSum / protocalStatByLayer.Duration, 3)
                };
                //ipv4
                ProtocalStatisticsTreeModel ipv4 = new ProtocalStatisticsTreeModel()
                {
                    DisplayName = "Internet Protocal Version 4",
                    Protocal = "Internet Protocal Version 4",
                    PacketPercent = Math.Round(protocalStatByLayer.IpV4Num * 100 / (double)protocalStatByLayer.LinkNum, 2),
                    PacketNum = protocalStatByLayer.IpV4Num,
                    BytesPercent = Math.Round(protocalStatByLayer.IpV4HSum * 100 / (double)protocalStatByLayer.FrameSum, 2),
                    Bytes = protocalStatByLayer.IpV4HSum,
                    bps = Math.Round(protocalStatByLayer.IpV4HSum / protocalStatByLayer.Duration, 3)
                };
                //ipv4-tcp
                ProtocalStatisticsTreeModel ipv4tcp = new ProtocalStatisticsTreeModel()
                {
                    DisplayName = "Transmisson Control Protocol",
                    Protocal = "Transmisson Control Protocol",
                    PacketPercent = Math.Round(protocalStatByLayer.IpV4TCPNum * 100 / (double)protocalStatByLayer.IpV4Num, 2),

                    PacketNum = protocalStatByLayer.IpV4TCPNum,
                    BytesPercent = Math.Round(protocalStatByLayer.IpV4TCPHSum * 100 / (double)protocalStatByLayer.IpV4TSum, 2),
                    Bytes = protocalStatByLayer.IpV4TCPHSum,
                    bps = Math.Round(protocalStatByLayer.IpV4TCPHSum / protocalStatByLayer.Duration, 3)
                };
                ////ipv4-tcp-H
                //ProtocalStatisticsTreeModel ipv4tcpH = new ProtocalStatisticsTreeModel()
                //{
                //    DisplayName = "Header",
                //    Protocal = "Header",
                //    PacketPercent = protocalStatByLayer.IpV4TCPNum / (double)protocalStatByLayer.IpV4Num,
                //    PacketNum = protocalStatByLayer.IpV4TCPNum,
                //    BytesPercent = protocalStatByLayer.IpV4TCPHSum / (double)protocalStatByLayer.IpV4TSum,
                //    Bytes = protocalStatByLayer.IpV4TCPHSum,
                //    bps = protocalStatByLayer.IpV4TCPHSum / protocalStatByLayer.Duration
                //};
                ////ipv4-tcp-DATA
                //ProtocalStatisticsTreeModel ipv4tcpdata = new ProtocalStatisticsTreeModel()
                //{
                //    DisplayName = "Data",
                //    Protocal = "Data",
                //    PacketPercent = protocalStatByLayer.IpV4TCPNum / (double)protocalStatByLayer.IpV4Num,
                //    PacketNum = protocalStatByLayer.IpV4TCPNum,
                //    BytesPercent = protocalStatByLayer.IpV4TCPHSum / (double)protocalStatByLayer.IpV4TSum,
                //    Bytes = protocalStatByLayer.IpV4TCPHSum,
                //    bps = protocalStatByLayer.IpV4TCPHSum / protocalStatByLayer.Duration
                //};
                //ipv4-udp
                ProtocalStatisticsTreeModel ipv4udp = new ProtocalStatisticsTreeModel()
                {
                    DisplayName = "User Datagram Protocol",
                    Protocal = "User Datagram Protocol",
                    PacketPercent = Math.Round(protocalStatByLayer.IpV4UDPNum * 100 / (double)protocalStatByLayer.IpV4Num, 2),
                    PacketNum = protocalStatByLayer.IpV4UDPNum,
                    BytesPercent = Math.Round(protocalStatByLayer.IpV4UDPHSum * 100 / (double)protocalStatByLayer.IpV4TSum, 2),
                    Bytes = protocalStatByLayer.IpV4UDPHSum,
                    bps = Math.Round(protocalStatByLayer.IpV4UDPHSum / protocalStatByLayer.Duration, 3)
                };
                //ipv6
                ProtocalStatisticsTreeModel ipv6 = new ProtocalStatisticsTreeModel()
                {
                    DisplayName = "Internet Protocal Version 6",
                    Protocal = "Internet Protocal Version 6",
                    PacketPercent = Math.Round(protocalStatByLayer.IpV6Num * 100 / (double)protocalStatByLayer.LinkNum, 2),
                    PacketNum = protocalStatByLayer.IpV6Num,
                    BytesPercent = Math.Round(protocalStatByLayer.IpV6HSum * 100 / (double)protocalStatByLayer.FrameSum, 2),
                    Bytes = protocalStatByLayer.IpV6HSum,
                    bps = Math.Round(protocalStatByLayer.IpV6HSum / protocalStatByLayer.Duration, 3)
                };
                //ipv6-tcp
                ProtocalStatisticsTreeModel ipv6tcp = new ProtocalStatisticsTreeModel()
                {
                    DisplayName = "Transmisson Control Protocol",
                    Protocal = "Transmisson Control Protocol",
                    PacketPercent = Math.Round(protocalStatByLayer.IpV6TCPNum * 100 / (double)protocalStatByLayer.IpV6Num, 2),
                    PacketNum = protocalStatByLayer.IpV6TCPNum,
                    BytesPercent = Math.Round(protocalStatByLayer.IpV6TCPHSum * 100 / (double)protocalStatByLayer.IpV6TSum, 2),
                    Bytes = protocalStatByLayer.IpV6TCPHSum,
                    bps = Math.Round(protocalStatByLayer.IpV6TCPHSum / protocalStatByLayer.Duration, 3)
                };
                //ipv6-udp
                ProtocalStatisticsTreeModel ipv6udp = new ProtocalStatisticsTreeModel()
                {
                    DisplayName = "User Datagram Protocol",
                    Protocal = "User Datagram Protocol",
                    PacketPercent = Math.Round(protocalStatByLayer.IpV6UDPNum * 100 / (double)protocalStatByLayer.IpV6Num, 2),
                    PacketNum = protocalStatByLayer.IpV6UDPNum,
                    BytesPercent = Math.Round(protocalStatByLayer.IpV6UDPHSum * 100 / (double)protocalStatByLayer.IpV6TSum, 2),
                    Bytes = protocalStatByLayer.IpV6UDPHSum,
                    bps = Math.Round(protocalStatByLayer.IpV6UDPHSum / protocalStatByLayer.Duration, 3)
                };
                //arp
                ProtocalStatisticsTreeModel arp = new ProtocalStatisticsTreeModel()
                {
                    DisplayName = "Adress Resolution Protocal",
                    Protocal = "Adress Resolution Protocal",
                    PacketPercent = Math.Round(protocalStatByLayer.ArpNum * 100 / (double)protocalStatByLayer.LinkNum, 2),
                    PacketNum = protocalStatByLayer.ArpNum,
                    BytesPercent = Math.Round(protocalStatByLayer.ArpHSum * 100 / (double)protocalStatByLayer.FrameSum, 2),
                    Bytes = protocalStatByLayer.ArpHSum,
                    bps = Math.Round(protocalStatByLayer.ArpHSum / protocalStatByLayer.Duration, 3)
                };
                ipv4.Children.Add(ipv4tcp);
                ipv4.Children.Add(ipv4udp);

                ipv6.Children.Add(ipv6tcp);
                ipv6.Children.Add(ipv6udp);

                ethernet.Children.Add(ipv4);
                ethernet.Children.Add(ipv6);
                ethernet.Children.Add(arp);
                frame.Children.Add(ethernet);
                TreeRoot.Children.Add(frame);
            }



            TreeRoot.IsBatchLoading = false;
            TreeListModel = TreeRoot;
        }


        #region Initialization

        ITreeModelRoot _treeListModel;
        public ITreeModelRoot TreeListModel
        {
            get
            {
                return _treeListModel;
            }
            set
            {
                if (_treeListModel != value)
                {
                    _treeListModel = value;
                    RaisePropertyChanged(() => TreeListModel);
                }
            }
        }


        private void CreateTreeRoot()
        {
            TreeRoot = new TreeModelRoot();
        }

        #endregion

        #region Properties

        /// <summary>
        /// If we want to programmatically set the selected item. Use TreeRoot for multiple item selections
        /// </summary>

        private TreeNode _selectedNode;
        public TreeNode SelectedNode
        {
            get { return _selectedNode; }
            set
            {
                _selectedNode = value;
                RaisePropertyChanged("SelectedNode");
            }
        }

        private ITreeModelRoot _treeRoot;
        public ITreeModelRoot TreeRoot
        {
            get
            {
                return _treeRoot;
            }
            set
            {
                _treeRoot = value;
                RaisePropertyChanged("TreeRoot");
            }
        }

        public string NoRowsDefaultMessage
        {
            get
            {
                return "No items";
            }
        }

        //private int _amount = 450;
        //public int AmountToGenerate
        //{
        //    get
        //    {
        //        return _amount;
        //    }
        //    set
        //    {
        //        _amount = value;
        //        LoadData();
        //    }
        //}


        private List<int> _amountList = null;
        public IEnumerable<int> ValidAmounts
        {
            get
            {
                if (_amountList == null)
                {
                    _amountList = new List<int>();
                    _amountList.Add(450);
                    _amountList.Add(4500);
                    _amountList.Add(9000);
                    _amountList.Add(18000);
                    _amountList.Add(36000);
                    _amountList.Add(72000);
                }
                return _amountList;

            }
        }
        #endregion

        #region Filter

        private string _filterString;
        public string FilterString
        {
            get { return _filterString; }
            set
            {
                if (SetValue(() => FilterString, ref _filterString, value))
                {
                    //make us handle filter. Make the tree refresh and run filters.
                    var root = TreeListModel;
                    TreeListModel = null;
                    TreeListModel = root;
                }
            }
        }

        public TreeListViewFilterState GetFilterState(ITreeModel treeModel)
        {
            if (string.IsNullOrWhiteSpace(FilterString))
                return TreeListViewFilterState.Include;
            //if (treeModel is OrganizationTreeModel) return TreeListViewFilterState.OnlyWithChildren;
            if (treeModel.DisplayName.Contains(FilterString) /*|| ((ProtocalStatisticsTreeModel)treeModel).Position.Contains(FilterString)*/)//if in our filter, include it.
                return TreeListViewFilterState.Include;
            return TreeListViewFilterState.Exclude;
        }



        #endregion
    }
}
