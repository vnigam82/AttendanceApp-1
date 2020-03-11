using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AttendanceApp
{
    /// <summary>
	/// FlowListView grid cell.
	/// </summary>
	[Preserve(AllMembers = true)]
    public class FlowGridCell : Grid, IFlowViewCell
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DLToolkit.Forms.Controls.FlowGridCell"/> class.
        /// </summary>
        public FlowGridCell()
        {
        }

        /// <summary>
        /// Raised when cell is tapped.
        /// </summary>
        public virtual void OnTapped()
        {
        }
    }
}
