using Seekford.Controls.WPFTreeListView;

namespace LAN002.Windows.ViewModel
{
    public class ProtocalStatisticsTreeModel : TreeModelBase
    {

        private string _protocal;
        public string Protocal
        {
            get { return _protocal; }
            set
            {
                _protocal = value;
                RaisePropertyChanged("Protocal");
            }
        }

        private double _packetPercent;
        public double PacketPercent
        {
            get => _packetPercent;
            set
            {
                _packetPercent = value;
                RaisePropertyChanged("PacketPercent");
            }
        }

        private int _packetNum;
        public int PacketNum
        {
            get => _packetNum;
            set
            {
                _packetNum = value;
                RaisePropertyChanged("PacketNum");
            }
        }

        private double _BytesPercent;
        public double BytesPercent
        {
            get => _BytesPercent;
            set
            {
                _BytesPercent = value;
                RaisePropertyChanged("BytesPercent");
            }
        }

        private int _Bytes;
        public int Bytes
        {
            get => _Bytes;
            set
            {
                _Bytes = value;
                RaisePropertyChanged("Bytes");
            }
        }

        private double _bps;
        public double bps
        {
            get => _bps;
            set
            {
                _bps = value;
                RaisePropertyChanged("bps");
            }
        }

    }
}