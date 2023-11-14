using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Runtime.CompilerServices;
using web_api.GameModel.OGIs;

namespace web_api.Services
{
    /// <summary>
    /// a Conductor for Ongoing Gameplay Interactions
    /// </summary>
    public class OGIConductor : BackgroundService
    {
        string connectionString;
        /// <summary>
        /// Constructor for Conductor
        /// </summary>
        /// <param name="configuration"></param>
        public OGIConductor(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        /// <summary>
        /// Creates a DataContext for Database interactions
        /// </summary>
        /// <returns></returns>
        private DataContext GetDataContext()
        {
            var option = new DbContextOptionsBuilder<DataContext>().UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)).Options;
            DataContext dataContext = new DataContext(option);

            return dataContext;
        }

        /// <summary>
        /// starts the background task
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //ConsoleLogger.LogInfo("Read Database... " + DateTime.Now.ToString());
                OngoingGameplayInteraction? interaction = await ExecuteNextOGI();

                // Todo: determine optimal delay time
                await Task.Delay(1000);
            }
        }

        /// <summary>
        /// execute Ongoing Gameplay Interaction 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private async Task<OngoingGameplayInteraction?> ExecuteNextOGI()
        {
            OngoingGameplayInteraction? resultBase = null;
            using (DataContext context = GetDataContext())
            {
                resultBase = await context.TravelOGIs
                    .OrderBy(i => i.ScheduledFor).FirstOrDefaultAsync();
                if (resultBase == null || resultBase.ScheduledFor > DateTime.Now) 
                    return null;
                
                ConsoleLogger.LogInfo(resultBase.ToString());

                switch (resultBase.Type)
                {
                    case InteractionType.Travel:
                        TravelOGI travelOGI = await context.TravelOGIs
                            .Where(travel => travel.Id == resultBase.Id).FirstOrDefaultAsync();
                        await travelOGI.ExecuteSelf(context);
                        break;

                    // todo: implement other OGI types

                    throw new NotImplementedException();
                }
                await context.SaveChangesAsync();
            }
            return resultBase;
        }

    }

}
