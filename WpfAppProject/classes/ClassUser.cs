using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfAppProject
{
    public partial class TableUsers
    {
        public string FIO
        {
            get
            {
                return UserSurname + " " + UserName + " " + UserPatronymic;
            }
        }

        public string Photo
        {
            get
            {
                if(UserPhoto!=null)
                {
                    return "\\images\\" + UserPhoto;
                }
                else
                {
                    return null;
                }
            }
        }

        public SolidColorBrush Color
        {
            get
            {
                //var brush = new BrushConverter();
                //return (SolidColorBrush)(Brush)brush.ConvertFrom("#111111");
                return (SolidColorBrush)new BrushConverter().ConvertFrom("#ffaacc");
            }
        }
    }
}
