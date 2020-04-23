using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VLCPlayerDemo.View.ContorlView;

namespace VLCPlayerDemo
{
    public class VlcPlayerViewModel
    {

        /// <summary>
        /// 播放器页面
        /// </ summary>
        public VlcPlayerView VlcPlayerV { get; set; }

        /// <summary>
        /// 播放器页面头
        /// </summary>
        public PalyerHeaderUserControl PalyerHeaderUC { get; set; }

       


        public VlcPlayerViewModel()
        {
        }

        public void Init()
        {
            PalyerHeaderUC = new PalyerHeaderUserControl() { DataContext = this };

            VlcPlayerV = new VlcPlayerView() { DataContext = this };
            VlcPlayerV.Show();
        }

        /// <summary>
        /// 视频名称 绑定在 PalyerHeaderUserControl 播放器页面头
        /// </summary>
        public string VideoName { get; set; }
    }
}

