using SharpPcap.LibPcap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LAN002.Windows.Init
{
    /// <summary>
    /// FilterSettingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class FilterSettingWindow : Window
    {
        private bool value_focus = false;
        private string old_filter;
        private MainWindow _mainWindow = null;
        public FilterSettingWindow()
        {
            InitializeComponent();
        }

        public FilterSettingWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            _mainWindow = mainWindow;
        }

        private void filter_cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void filter_ok_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.setFilter(filter_cont.Text);
            this.Close();
        }

        private void rel_value_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
           
        }

        private void rel_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (rel_list.SelectedItem != null)
            {
                ListViewItem listViewItem = rel_list.SelectedItem as ListViewItem;
                string filter = listViewItem.Content.ToString();
                filter = filter_cont.Text.Trim(' ') + " " + filter;
                filter_cont.Text = filter;
                //check(filter);
            }
        }

        private void filter_list_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (filter_list.SelectedItem != null)
            {
                rel_value.Text = "";
                TreeViewItem treeViewItem = filter_list.SelectedItem as TreeViewItem;
                string str = treeViewItem.Header.ToString();
                string filter = str.Substring(0, str.IndexOf('·'));

                if (filter.StartsWith("tcp flags "))
                {
                    string flag = filter.Substring(10).Trim(' ');
                    filter = "tcp[tcpflags] & (tcp-" + flag + ") != 0 ";

                }
                else
                {
                    filter = filter.ToLower();
                }

                filter = filter_cont.Text.Trim(' ') + " " + filter;
                filter_cont.Text = filter;
                Console.WriteLine("filter: {0}", filter);
                
            }
        }


        private void check(string filter)
        {
            string s;
            var b = PcapDevice.CheckFilter(filter, out s);
            if (b)
            {
                Color color = (Color)ColorConverter.ConvertFromString("LightGreen");
                filter_cont.Background = new SolidColorBrush(color);
                filter_ok.IsEnabled = true;
            }
            else
            {
                Color color = (Color)ColorConverter.ConvertFromString("Red");
                filter_cont.Background = new SolidColorBrush(color);
                filter_ok.IsEnabled = false;
            }
        }

        private void rel_value_FocusableChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (value_focus)
            {
                old_filter = filter_cont.Text.Trim(' ').ToString();
                value_focus = false;
            }
            else
            {
                value_focus = true;
                old_filter = filter_cont.Text.Trim(' ').ToString();
            }
            Console.WriteLine("{0}---{1}", value_focus, old_filter);
        }

        private void rel_value_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (rel_value.IsFocused)
            {
                string filter = old_filter + " " + rel_value.Text;
                //filter = old_filter + " " + filter;
                filter_cont.Text = filter;
                check(filter);
            }
            else
            {
                Console.WriteLine("dsdddddd   {0}---{1}", value_focus, old_filter);
            }
        }

        private void rel_value_IsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Console.WriteLine("rel_value_IsKeyboardFocusedChanged");
            if (value_focus)
            {
                old_filter = filter_cont.Text.Trim(' ').ToString();
                value_focus = false;
            }
            else
            {
                value_focus = true;
                old_filter = filter_cont.Text.Trim(' ').ToString();
            }
            Console.WriteLine("{0}---{1}", value_focus, old_filter);
        }

        private void rel_value_IsKeyboardFocusWithinChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Console.WriteLine("rel_value_IsKeyboardFocusWithinChanged");
        }

        private void filter_cont_TextChanged(object sender, TextChangedEventArgs e)
        {
            string filter = filter_cont.Text;
            check(filter);
        }

        private void filter_delete_Click(object sender, RoutedEventArgs e)
        {
            old_filter = "";
            filter_cont.Text = "";
        }
    }
}
