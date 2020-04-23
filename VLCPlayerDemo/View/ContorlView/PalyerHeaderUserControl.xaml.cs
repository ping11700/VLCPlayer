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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VLCPlayerDemo.View.ContorlView
{
    /// <summary>
    /// PalyerHeaderUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class PalyerHeaderUserControl : UserControl
    {
        public PalyerHeaderUserControl()
        {
            InitializeComponent();
        }

        private Window parentWindow = null;

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            IntPtr controlHandle = ((HwndSource)PresentationSource.FromVisual(this)).Handle;//wpf里窗体句柄是唯一的，任何控件获取的都是窗体句柄
            foreach (Window item in Application.Current.Windows)
            {
                IntPtr windowHandle = new WindowInteropHelper(item).Handle;
                if (controlHandle.Equals(windowHandle))
                {
                    parentWindow = item;
                    break;
                }
            }
        }

        /// <summary>
        /// 固定窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFix_Click(object sender, RoutedEventArgs e)
        {

        }
        /// <summary>
        /// 最小化窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnMin_Click(object sender, RoutedEventArgs e)
        {
            parentWindow.WindowState = WindowState.Minimized;
        }


        /// <summary>
        /// 最大化窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnMax_Click(object sender, RoutedEventArgs e)
        {
            if (parentWindow.WindowState == WindowState.Maximized)
            {
                parentWindow.WindowState = WindowState.Normal;
            }
            else
            {
                parentWindow.WindowState = WindowState.Maximized;

            }
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (parentWindow is LoginView)
                {
                    parentWindow.Close();
                    Environment.Exit(System.Environment.ExitCode);
                }
                else
                {
                    parentWindow.Close();
                }
                Application.Current.Shutdown();
            }
            catch
            {

            }
        }

        /// <summary>
        /// 拖动窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            parentWindow.DragMove();
        }

     
    }
}
