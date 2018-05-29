using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LAN002.DTO;
using LAN002.Windows.Statistics;
using System.Threading;
using Seekford.Controls.WPFTreeListView;

namespace LAN002.Windows.ViewModel
{
    public class PacketLengthsStatisticsTreeViewModel : ViewModelBase, ITreeListViewFilter
    {
        PacketLengthsStatisticsTreeModel packetStatByLength = null;
        List<PacketLengthsStatisticsTreeModel> packetLengthsStatisticsTreeModels = null;
        Thread thread = null;
        public PacketLengthsStatisticsTreeViewModel()
        {

            CreateTreeRoot();

            //thread = new Thread(new ThreadStart(() =>
            //{
            //    while (true)
            //    {
            //        LoadData();
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

            packetLengthsStatisticsTreeModels = PacketLengthsStatisticsWindow.getPacketLengthStatisticsAll();
            if (packetLengthsStatisticsTreeModels.Count > 0)
            {
                if (packetLengthsStatisticsTreeModels[0].Start == 0 && packetLengthsStatisticsTreeModels[0].End == int.MaxValue)
                {
                    packetLengthsStatisticsTreeModels[0].DisplayName = getDisplayName(0);
                    for (int i = 1; i < packetLengthsStatisticsTreeModels.Count; i++)
                    {
                        packetLengthsStatisticsTreeModels[i].DisplayName = getDisplayName(i);
                        packetLengthsStatisticsTreeModels[0].Children.Add(packetLengthsStatisticsTreeModels[i]);
                    }
                    TreeRoot.Children.Add(packetLengthsStatisticsTreeModels[0]);
                }
            }
            TreeRoot.IsBatchLoading = false;
            TreeListModel = TreeRoot;
        }

        private string getDisplayName(int i)
        {
            switch (i)
            {
                case 0:
                    return "Packet Lengths";
                case 1:
                    return "0-19";
                case 2:
                    return "20-39";
                case 3:
                    return "40-79";
                case 4:
                    return "80-159";
                case 5:
                    return "160-319";
                case 6:
                    return "320-639";
                case 7:
                    return "640-1279";
                case 8:
                    return "1280-2559";
                case 9:
                    return "2560-5119";
                case 10:
                    return "5120 and greater";
            }
            return "";
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
