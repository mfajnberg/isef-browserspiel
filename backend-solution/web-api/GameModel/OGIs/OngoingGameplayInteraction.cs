using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using web_api.GameModel.Creatures;

namespace web_api.GameModel.OGIs
{
    /// <summary>
    /// represents an OngoingGamplayInteraction, like traveling 
    /// </summary>
    [DebuggerDisplay("Id:{Id}, Type:{Type}, ScheduledAt:{ScheduledAt}, ScheduledFor:{ScheduledFor}")] 
    [Table("OGIs")]
    public abstract class OngoingGameplayInteraction
    {
        /// <summary>
        /// gets or sets the id of an OGI
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ForeignKey("Party")]
        public int PartyId { get; set; }

        /// <summary>
        /// gets or sets the Type of <c>OngoingGameplayInteraction</c> 
        /// </summary>
        public InteractionType Type { get; set; }

        /// <summary>
        /// gets or sets the <c>DateTime</c> value, when the OGI was scheduled
        /// </summary>
        public DateTime ScheduledAt { get; set; }

        /// <summary>
        /// gets or sets the <c>DateTime</c> value, when the OGI hast to be done
        /// </summary>
        public DateTime ScheduledFor { get; set; }

        /// <summary>
        /// represents a gameplay rule, 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        internal virtual bool ValidateRule(DataContext context)
        {
            return true;
        }

        /// <summary>
        /// Executes this OngoingGameplayInteraction immediately
        /// </summary>
        /// <param name="context"><c>DataContext</c> for Database interactions</param>
        /// <returns></returns>
        public virtual async Task ExecuteSelf(DataContext context)
        {
            
        }

        /// <summary>
        /// converts the <c>OngoingGameplayInteraction</c> to a string representation
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Id:{Id}, Type:{Type}, ScheduledAt:{ScheduledAt}, ScheduledFor:{ScheduledFor}";
        }
    }
}
