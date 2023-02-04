using System.Diagnostics;

namespace web_api.DTOs
{
    /// <summary>
    /// Data Transfer Object for Ongoing Gameplay Interactions
    /// </summary>
    [DebuggerDisplay("OGI: Schedule for={ScheduledFor.ToString()}, Interaction: {Interaction}")]
    public class OGIDTO
    {

        public DateTime ScheduledFor { get; set; }

        public string Interaction { get; set; }

    }
}
