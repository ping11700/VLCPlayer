using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace VLCPlayerDemo
{
    /// <summary>
    /// VlcPlayerView.xaml 的交互逻辑
    /// </summary>
    public partial class VlcPlayerView : Window
    {
        public VlcPlayerView()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var currentAssembly = Assembly.GetEntryAssembly();
            var currentDirectory = new FileInfo(currentAssembly.Location).DirectoryName;
            var libDirectory = new DirectoryInfo(System.IO.Path.Combine(currentDirectory, "libvlc", IntPtr.Size == 4 ? "win-x86" : "win-x64"));
            this.VlcControl.SourceProvider.CreatePlayer(libDirectory);
            this.VlcControl.SourceProvider.MediaPlayer.LengthChanged += MediaPlayer_LengthChanged;
            LocationChanged += new EventHandler(MainWindow_LocationChanged);

        }

        private void MediaPlayer_LengthChanged(object sender, Vlc.DotNet.Core.VlcMediaPlayerLengthChangedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                slider1.Maximum = this.VlcControl.SourceProvider.MediaPlayer.Length;
            }), DispatcherPriority.Normal);
        }





        private void Slider1_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            var position = (float)(slider1.Value / slider1.Maximum);
            if (position == 1)
            {
                position = 0.99f;
            }
            this.VlcControl.SourceProvider.MediaPlayer.Position = position;//Position为百分比，要小于1，等于1会停止
        }
        private void Slider2_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            //Audio.IsMute :静音和非静音
            //Audio.Volume：音量的百分比，值在0—200之间
            //Audio.Tracks：音轨信息，值在0 - 65535之间
            //Audio.Channel：值在1至5整数，指示的音频通道模式使用，值可以是：“1 = 立体声”，“2 = 反向立体声”，“3 = 左”，“4 = 右” “5 = 混音”。 
            //Audio.ToggleMute() : 方法，切换静音和非静音 
            this.VlcControl.SourceProvider.MediaPlayer.Audio.Volume = (int)slider2.Value;
        }


        /// <summary>
        /// 播放视频
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void palyVideo_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Title = "请选择视频文件";
            var result = ofd.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                string filePath = ofd.FileName;
                try
                {
                    this.VlcControl.SourceProvider.MediaPlayer.Play(new Uri(filePath));
                }
                catch (Exception ex)
                {

                }
            }
        }

        public void pause_Click(object sender, RoutedEventArgs e)
        {
            //if (btnPause.Content.ToString() == "播放")
            //{
            //    btnPause.Content = "暂停";
            //    this.VlcControl.SourceProvider.MediaPlayer.Play();
            //}
            //else
            //{
            //    btnPause.Content = "播放";
            //    this.VlcControl.SourceProvider.MediaPlayer.Pause();
            //}
        }
        private void stop_Click(object sender, RoutedEventArgs e)
        {
            new Task(() =>
            {
                this.VlcControl.SourceProvider.MediaPlayer.Stop();//这里要开线程处理，不然会阻塞播放

            }).Start();
        }

        private void rate_Click(object sender, RoutedEventArgs e)
        {
            //float rate;
            //if (float.TryParse(this.tblRate.Text, out rate))
            //{
            //    this.VlcControl.sourceProvider.MediaPlayer.Rate = rate;
            //}
            //this.VlcControl.SourceProvider.MediaPlayer.Play();
        }

        private void btnhttp_Click(object sender, RoutedEventArgs e)
        {
            //string httpUri = this.tbhttp.Text.Trim();
            //if (!string.IsNullOrEmpty(httpUri))
            //{
            //    this.VlcControl.SourceProvider.MediaPlayer.Play(httpUri);
            //}
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Close();
        }

       
        /// <summary>
        /// 播放下一个视频
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void playNext_Click(object sender, RoutedEventArgs e)
        {

        }


        /// <summary>
        /// 选择视频清晰度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox_SelectedChanged(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// 选择剧集
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selecVidoes_btn_Click(object sender, RoutedEventArgs e)
        {
            
            if (this.selectVideo_popup.IsOpen)
            {
                this.selectVideo_popup.IsOpen = false;
            }
            else
            {
                this.selectVideo_popup.IsOpen = true;
            }
        }


        #region Windows API

        /// <summary>
        /// 解决Popup随window一起移动问题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_LocationChanged(object sender, EventArgs e)
        {
            var mi = typeof(Popup).GetMethod("UpdatePosition", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            mi.Invoke(this.selectVideo_popup, null);
        }


        /// <summary>
        ///解决最小化恢复时会有阴影问题
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStateChanged(EventArgs e)
        {
            switch (WindowState)
            {
                case WindowState.Normal:
                    Opacity = 1;
                    break;
                case WindowState.Minimized:
                    Opacity = 0;
                    break;
            }
        }

        //重写
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            HwndSource hwndSource = PresentationSource.FromVisual(this) as HwndSource;
            if (hwndSource != null)
            {
                hwndSource.AddHook(new HwndSourceHook(WndProc));
            }
        }

        private const int WM_NCHITTEST = 0x0084;
        private Point mousePoint = new Point(); //鼠标坐标
        private const int ResizeBorderAGWidth = 12;//转角宽度 
        private const int ResizeBorderThickness = 4;//边框宽度
        /// <summary>
        /// 委托方法
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="msg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <param name="handled"></param>
        /// <returns></returns>
        protected virtual IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case WM_NCHITTEST:
                    mousePoint.X = (lParam.ToInt32() & 0xFFFF);
                    mousePoint.Y = (lParam.ToInt32() >> 16);
                    // 窗口左上角
                    if (mousePoint.Y - Top <= ResizeBorderAGWidth && mousePoint.X - Left <= ResizeBorderAGWidth)
                    {
                        handled = true;
                        return new IntPtr((int)HitTest.HTTOPLEFT);
                    }
                    // 窗口左下角　　
                    else if (ActualHeight + Top - mousePoint.Y <= ResizeBorderAGWidth && mousePoint.X - Left <= ResizeBorderAGWidth)
                    {
                        handled = true;
                        return new IntPtr((int)HitTest.HTBOTTOMLEFT);
                    }
                    // 窗口右上角
                    else if (mousePoint.Y - Top <= ResizeBorderAGWidth && ActualWidth + Left - mousePoint.X <= ResizeBorderAGWidth)
                    {
                        handled = true;
                        return new IntPtr((int)HitTest.HTTOPRIGHT);
                    }
                    // 窗口右下角
                    else if (ActualWidth + Left - mousePoint.X <= ResizeBorderAGWidth && ActualHeight + Top - mousePoint.Y <= ResizeBorderAGWidth)
                    {
                        handled = true;
                        return new IntPtr((int)HitTest.HTBOTTOMRIGHT);
                    }
                    // 窗口左侧
                    else if (mousePoint.X - Left <= ResizeBorderThickness)
                    {
                        handled = true;
                        return new IntPtr((int)HitTest.HTLEFT);
                    }
                    // 窗口右侧
                    else if (ActualWidth + Left - mousePoint.X <= ResizeBorderThickness)
                    {
                        handled = true;
                        return new IntPtr((int)HitTest.HTRIGHT);
                    }
                    // 窗口上方
                    else if (mousePoint.Y - Top <= ResizeBorderThickness)
                    {
                        handled = true;
                        return new IntPtr((int)HitTest.HTTOP);
                    }
                    // 窗口下方
                    else if (ActualHeight + Top - mousePoint.Y <= ResizeBorderThickness)
                    {
                        handled = true;
                        return new IntPtr((int)HitTest.HTBOTTOM);
                    }
                    else // 窗口移动
                    {
                        return IntPtr.Zero;
                    }
            }

            return IntPtr.Zero;
        }

        #endregion
    }
}
