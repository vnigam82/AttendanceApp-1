using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AttendanceApp;
using AttendanceApp.iOS.Renderer;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(FlowListViewInternalCell), typeof(FlowListViewInternalCellRenderer))]
namespace AttendanceApp.iOS.Renderer
{
    public class FlowListViewInternalCellRenderer : ViewCellRenderer
    {
        public override UIKit.UITableViewCell GetCell(Xamarin.Forms.Cell item, UIKit.UITableViewCell reusableCell, UIKit.UITableView tv)
        {
            tv.AllowsSelection = false;
            var cell = base.GetCell(item, reusableCell, tv);
            cell.SelectionStyle = UITableViewCellSelectionStyle.None;

            return cell;
        }
    }
}