using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AttendanceApp
{
    /// <summary>
	/// FlowListView stack cell.
	/// </summary>
	[Preserve(AllMembers = true)]
    public class FlowStackCell : StackLayout, IFlowViewCell
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DLToolkit.Forms.Controls.FlowStackCell"/> class.
        /// </summary>
        public FlowStackCell()
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
