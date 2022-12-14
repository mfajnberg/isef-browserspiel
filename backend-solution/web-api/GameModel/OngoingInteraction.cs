using System.ComponentModel.DataAnnotations.Schema;

namespace web_api.GameModel
{
    public class OngoingInteraction
    {
        public int Id { get; set; }

        public DateTime ScheduledAt { get; set; }

        public DateTime ScheduledFor { get; set; }

        internal bool ValidateRule(DataContext context)
        {
            return true;
        }

        public override string ToString()
        {
            return $"Id:{Id}, ScheduledAt:{ScheduledAt}, ScheduledFor:{ScheduledFor}";
        }
    }
}
