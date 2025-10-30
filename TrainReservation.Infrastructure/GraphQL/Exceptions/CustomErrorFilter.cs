using HotChocolate;
using Microsoft.EntityFrameworkCore;
using System;
using TrainReservation.Infrastructure.Exceptions;

namespace TrainReservation.Infrastructure.GraphQL.Exceptions
{
    public class CustomErrorFilter : IErrorFilter
    {
        public IError OnError(IError error)
        {
            if (error.Exception is IdNotFoundException infe)
            {
                return error
                    .WithMessage(infe.Message)
                    .WithPath(error.Path)
                    .SetExtension("code", "ID_NOT_FOUND")
                    .RemoveLocations();
            }

            if (error.Exception is NoMatchException nme)
            {
                return error
                    .WithMessage(nme.Message)
                    .WithPath(error.Path)
                    .SetExtension("code", "NO_MATCH")
                    .RemoveLocations();
            }

            if (error.Exception is DbUpdateConcurrencyException duce)
            {
                return error
                    .WithMessage(duce.Message)
                    .WithPath(error.Path)
                    .SetExtension("code", "DB_UPDATE_CONCURRENCY")
                    .RemoveLocations();
            }

            if (error.Exception is InvalidOperationException ioe)
            {
                return error
                    .WithMessage(ioe.Message)
                    .WithPath(error.Path)
                    .SetExtension("code", "INVALID_OPERATION")
                    .RemoveLocations();
            }

            return error;
        }
    }
}
