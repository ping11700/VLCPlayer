using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VLCPlayerDemo.Control;

namespace VLCPlayerDemo
{
    public class Controller

    {
        private static Controller controller;

        private static object _lock = new object();

        //懒汉模式-多线程
        public static Controller GetControlller()
        {
            if (controller == null) 
            {
                lock (_lock)
                {
                    if (controller == null) 
                    {
                        controller = new Controller();
                    }
                }
            }
            return controller;
        }


        /// <summary>
        /// VlcPlayerViewModel 播放器ViewModel
        /// </summary>
        public VlcPlayerViewModel VlcPlayerVM { get; set; }

        /// <summary>
        /// MoviesDataViewModel 影片DataviewModel
        /// </summary>
        public MoviesDataViewViewModel MoviesDataViewVM { get; set; }

        /// <summary>
        /// LoginViewModel 登录窗口viewModel
        /// </summary>
        public LoginViewModel LoginVM { get; set; }

        public void Init()
        {
            HttpConnection.HttpConnect();//http连接

            MoviesDataViewVM = new MoviesDataViewViewModel();
            MoviesDataViewVM.Init();

            LoginVM = new LoginViewModel();
            LoginVM.Init();

            VlcPlayerVM = new VlcPlayerViewModel();
            VlcPlayerVM.Init();
        }
    }
}
