using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceApp
{
    /// <summary>
	/// IFlowViewCell.
	/// </summary>
	[Preserve(AllMembers = true)]
    public interface IFlowViewCell
    {
        /// <summary>
        /// Raised when cell is tapped.
        /// </summary>
        void OnTapped();
    }
}
