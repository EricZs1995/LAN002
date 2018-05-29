using LAN002.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Seekford.Controls.WPFTreeListView;

namespace LAN002.Windows.ViewModel
{
    public class PacketLengthsStatisticsTreeModel : TreeModelBase
    {
        public PacketLengthsStatisticsTreeModel(PacketStatByLength packetStatByLength)
        {
            Start = packetStatByLength.Start;
            End = packetStatByLength.End;
            Count = packetStatByLength.Num;
            Average = packetStatByLength.Average;
            MaxVal = packetStatByLength.MaxVal;
            MinVal = packetStatByLength.MinVal;
            Rate = packetStatByLength.Rate;
            Percent = packetStatByLength.PercentNum;
        }

        public PacketLengthsStatisticsTreeModel()
        {
        }

        private int _start;
        public int Start
        {
            get { return _start; }
            set
            {
                _start = value;
                RaisePropertyChanged("Start");
            }
        }

        private int _end;
        public int End
        {
            get { return _end; }
            set
            {
                _end = value;
                RaisePropertyChanged("End");
            }
        }

        private int _count;
        public int Count
        {
            get { return _count; }
            set
            {
                _count = value;
                RaisePropertyChanged("Count");
            }
        }

        private double _average;
        public double Average
        {
            get { return _average; }
            set
            {
                _average = value;
                RaisePropertyChanged("Average");
            }
        }

        private int _minVal;
        public int MinVal
        {
            get { return _minVal; }
            set
            {
                _minVal = value;
                RaisePropertyChanged("MinVal");
            }
        }

        private int _maxVal;
        public int MaxVal
        {
            get { return _maxVal; }
            set
            {
                _maxVal = value;
                RaisePropertyChanged("MaxVal");
            }
        }

        private double _rate;
        public double Rate
        {
            get { return _rate; }
            set
            {
                _rate = value;
                RaisePropertyChanged("Rate");
            }
        }

        private double _percent;
        public double Percent
        {
            get { return _percent; }
            set
            {
                _percent = value;
                RaisePropertyChanged("Percent");
            }
        }
    }
}
