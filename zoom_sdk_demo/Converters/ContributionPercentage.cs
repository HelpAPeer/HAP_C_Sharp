using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using zoom_sdk_demo.Models;

namespace zoom_sdk_demo.Converters
{
    public class ContributionPercentage : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int talk_time = (int)value;
       
            if (Session.instance.total_talking_time >0)
            {
                int percetage_time = ((talk_time * 100) / Session.instance.total_talking_time) ;
                return percetage_time.ToString() + "%";

            }
            else
                return "0%";
            //throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
