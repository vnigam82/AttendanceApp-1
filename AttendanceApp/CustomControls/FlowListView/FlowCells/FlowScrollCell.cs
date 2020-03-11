using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AttendanceApp
{
    /// <summary>
	/// FlowListView scroll cell.
	/// </summary>
	[Preserve(AllMembers = true)]
    public class FlowScrollCell : ScrollView, IFlowViewCell
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DLToolkit.Forms.Controls.FlowScrollCell"/> class.
        /// </summary>
        public FlowScrollCell()
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
