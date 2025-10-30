using System.Collections.Generic;
using System.Threading.Tasks;
using TrainReservation.Application.GraphQL.Filters;
using TrainReservation.Application.GraphQL.Interfaces;
using TrainReservation.Core.Models;

namespace TrainReservation.Infrastructure.GraphQL.Queries
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
