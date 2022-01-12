using System;

namespace Train_Reservation_Application.Exceptions
{
    public class IdNotFoundException : Exception
    {
        public IdNotFoundException() { }
        public IdNotFoundException(string model, int id) : base(String.Format("There is no {0} with id={1}", model, id.ToString())) { }
    }

    public class NoMatchException : Exception
    {
        public NoMatchException() { }
        public NoMatchException(int first, int second, string model) : base(String.Format("Id {0} is not a match with id {1} for {2}", first.ToString(), second.ToString(), model)) { }
    }
}
