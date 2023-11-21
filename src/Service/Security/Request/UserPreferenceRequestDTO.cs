using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portolo.Security.Request
{
    public class UserPreferenceRequestDTO
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string TopMenuColor { get; set; }
        public string TopMenuTextColor { get; set; }
        public string FontFamily { get; set; }
        public string BottomMenuColor { get; set; }
        public string BottomMenuTextColor { get; set; }
        public string FontSize { get; set; }
        public string PreferedLanguage { get; set; }
        public string DefaultDateFormat { get; set; }
        public string ShipmentViewType { get; set; }
    }
}
