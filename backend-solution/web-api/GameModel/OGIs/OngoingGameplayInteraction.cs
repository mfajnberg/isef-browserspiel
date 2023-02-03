using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using web_api.GameModel.Creatures;

namespace web_api.GameModel.OGIs
{
    [Table("OGIs")]
    public abstract class OngoingGameplayInteraction
    {
        [Key]
        public int Id { get; set; }

        public InteractionType Type { get; set; }
        public DateTime ScheduledAt { get; set; }

        public DateTime ScheduledFor { get; set; }

        internal virtual bool ValidateRule(DataContext context)
        {
            return true;
        }

        public virtual async Task ExecuteSelf(DataContext context)
        {
        }

        public override string ToString()
        {
            return $"Id:{Id}, ScheduledAt:{ScheduledAt}, ScheduledFor:{ScheduledFor}";
        }
    }
}
