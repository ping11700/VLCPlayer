using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VLCPlayerDemo.Control
{
    public class LoginViewModel
    {
        /// <summary>
        /// loginView  登录页面
        /// </summary>
        public LoginView LoginV { get; set; }


        public LoginViewModel()
        {

        }

        public void Init()
        {
            LoginV = new LoginView() { DataContext = this };
        }

    }

}
