using System.Collections.Generic;
using System.Threading.Tasks;
using Train_Reservation_Application.GraphQL.Filters;
using Train_Reservation_Application.GraphQL.Interfaces;
using Train_Reservation_Application.Models;

namespace Train_Reservation_Application.GraphQL.Queries
{
    public class TrainQuery
    {
        public static async Task<List<Train>> GetTrainsByDateAsync(IGraphQLTrainsService trainsService, DateFilterInput filter)
        {
            return await trainsService.GetTrainsByDateAsync(filter);
        }

        public static async Task<Train> GetCarsByTypeAsync(IGraphQLTrainsService trainsService, CarTypeFilterInput filter)
        {
            return await trainsService.GetCarsByTypeAsync(filter);
        }

        public static async Task<List<int>> GetSeatListAsync(IGraphQLTrainsService trainsService, SeatListFilterInput filter)
        {
            return await trainsService.GetSeatListAsync(filter);
        }
    }    
}
