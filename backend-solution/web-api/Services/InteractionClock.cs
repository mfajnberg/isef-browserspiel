using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Runtime.CompilerServices;
using web_api.GameModel;

namespace web_api.Services
{
    public class InteractionClock : BackgroundService
    {
        string connectionString;
        public InteractionClock(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private DataContext GetDataContext()
        {
            var option = new DbContextOptionsBuilder<DataContext>().UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)).Options;
            DataContext dataContext = new DataContext(option);

            return dataContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {

                ConsoleLogger.LogInfo("Read Database");
                OngoingInteraction? interaction = await GetNextInteraction();
                // ToDo: Execute OnGoingInteraction


                //Todo: get optimal delay time
                await Task.Delay(1000);
            }
        }


        private async Task<OngoingInteraction?> GetNextInteraction()
        {
            OngoingInteraction? interactionResult = null;
            using (DataContext context = GetDataContext())
            {

                interactionResult = await context.Interactions.OrderBy(i => i.ScheduledAt).FirstOrDefaultAsync();

                if (interactionResult != null)
                {
                    ConsoleLogger.LogInfo(interactionResult.ToString());
                    context.Interactions.Remove(interactionResult);
                    await context.SaveChangesAsync();
                }
            }
            return interactionResult;
        }
        
    }

}
