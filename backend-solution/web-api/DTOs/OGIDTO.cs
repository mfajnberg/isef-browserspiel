using System.Diagnostics;

namespace web_api.DTOs
{
    /// <summary>
    /// Data Transfer Object for Ongoing Gameplay Interactions
    /// </summary>
    [DebuggerDisplay("OGI: Schedule for={ScheduledFor.ToString()}, Interaction: {Interaction}")]
    public class OGIDTO
    {
        /// <summary>
        /// gets or sets the DateTime value when the OGI is scheduled for
        /// </summary>
        public DateTime ScheduledFor { get; set; }

        /// <summary>
        /// gets or sets the interaction which must be scheduled
        /// </summary>
        public string Interaction { get; set; }

    }
}
