using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace VLCPlayerDemo
{
    public class CusImageButton : Button
    {


        public string MoviePicture
        {
            get { return (string)GetValue(MoviePictureProperty); }
            set { SetValue(MoviePictureProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MoviePicture.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MoviePictureProperty =
            DependencyProperty.Register("MoviePicture", typeof(string), typeof(CusImageButton), null);


        public bool IsButtonSelecct
        {
            get { return (bool)GetValue(IsButtonSelecctProperty); }
            set { SetValue(IsButtonSelecctProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsButtonSelecct.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsButtonSelecctProperty =
            DependencyProperty.Register("IsButtonSelecct", typeof(bool), typeof(CusImageButton), new PropertyMetadata(false));

    }
}
