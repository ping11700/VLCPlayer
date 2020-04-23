using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VLCPlayerDemo.Control
{
    public class MoviesDataViewViewModel
    {
        /// <summary>
        /// 影片DataView 页面
        /// </summary>
        public MoviesDataViewView MoviesDataViewV { get; set; }
        public MoviesDataViewViewModel()
        {

        }
        public void Init()
        {
            MoviesDataViewV = new MoviesDataViewView() { DataContext =this};
        }
    }
}
