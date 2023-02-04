using System.Diagnostics;

namespace web_api.DTOs
{
    /// <summary>
    /// Data Transfer Object for creatures
    /// </summary>
    [DebuggerDisplay("Name = {Name}")]
    public class CreatureDTO
    {
        /// <summary>
        /// gets or sets the name of the creature
        /// </summary>
        public string Name { get; set; }

    }
}
