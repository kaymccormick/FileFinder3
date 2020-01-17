using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vanara.Windows.Shell;

namespace WpfApp1
{
    public class ShellItemAttributeListItem
    {
        public ShellItemAttribute ShellItemAttribute { get; set; }
        public string SummaryText { get; set; }

        public ShellItemAttributeListItem(ShellItemAttribute shellItemAttribute, string summaryText)
        {
            ShellItemAttribute = shellItemAttribute;
            SummaryText = summaryText;
        }
    }
}
