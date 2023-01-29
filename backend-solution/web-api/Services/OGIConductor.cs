using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Runtime.CompilerServices;
using web_api.GameModel.OGIs;

namespace web_api.Services
{
    public class OGIConductor : BackgroundService
    {
        string connectionString;
        public OGIConductor(IConfiguration configuration)
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
                OngoingGameplayInteraction? interaction = await ExecuteNextOGI();
                // ToDo: Execute OnGoingInteraction

                // Todo: determine optimal delay time
                await Task.Delay(1000);
            }
        }


        private async Task<OngoingGameplayInteraction?> ExecuteNextOGI()
        {
            OngoingGameplayInteraction? resultBase = null;
            using (DataContext context = GetDataContext())
            {
                resultBase = await context.TravelOGIs
                    .OrderBy(i => i.ScheduledFor).FirstOrDefaultAsync();
                if (resultBase == null) 
                    return null;
                
                ConsoleLogger.LogInfo(resultBase.ToString());

                switch (resultBase.Type)
                {
                    case InteractionType.Travel:
                        TravelOGI travelOGI = await context.TravelOGIs
                            .Where(travel => travel.Id == resultBase.Id).FirstOrDefaultAsync();
                        await travelOGI.ExecuteSelf(context);
                        break;

                    //...

                    throw new NotImplementedException();
                }
                await context.SaveChangesAsync();
            }
            return resultBase;
        }

    }

}
