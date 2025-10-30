using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrainReservation.Application.GraphQL.Filters;
using TrainReservation.Application.GraphQL.Interfaces;
using TrainReservation.Core.Models;
using TrainReservation.Infrastructure.Data;
using TrainReservation.Infrastructure.Services;

namespace TrainReservation.Infrastructure.GraphQL.Services
{
    public sealed class GraphQLTrainsService : IGraphQLTrainsService
    {
        private readonly TrainsService _trainsServiceNew;
        private readonly ApplicationDbContext _context;

        public GraphQLTrainsService(IDbContextFactory<ApplicationDbContext> dbContextFactory)
        {
            _context = dbContextFactory.CreateDbContext();
            _trainsServiceNew = new TrainsService(_context);
        }

        public async Task<List<Train>> GetTrainsByDateAsync(DateFilterInput filter)
        {            
            return await _trainsServiceNew.GetTrainsByDate(filter.DayOfWeek).ToListAsync();
        }

        public async Task<Train> GetCarsByTypeAsync(CarTypeFilterInput filter)
        {
            return await _trainsServiceNew
                .GetCarsByType(filter.Id, filter.CalendarDate, filter.Type)
                .FirstOrDefaultAsync();
        }

        public async Task<List<int>> GetSeatListAsync(SeatListFilterInput filter)
        {
            return await _trainsServiceNew.GetSeatListAsync(filter.Id, filter.CalendarDate, filter.N);
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();

            GC.SuppressFinalize(this);
        }
    }
}
