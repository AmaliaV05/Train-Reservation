using System;

namespace TrainReservation.Infrastructure.Exceptions
{
    public class IdNotFoundException : Exception
    {
        public IdNotFoundException() { }
        public IdNotFoundException(string model, int id) : base(string.Format("There is no {0} with id={1}", model, id.ToString())) { }
    }

    public class NoMatchException : Exception
    {
        public NoMatchException() { }
        public NoMatchException(int first, int second, string model) : base(string.Format("Id {0} is not a match with id {1} for {2}", first.ToString(), second.ToString(), model)) { }
    }
}
