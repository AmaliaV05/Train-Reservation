namespace TrainReservation.Application.DTOs
{
    public class ResponseService<TResponse, TMessage>
    {
        public TResponse Response { get; set; }
        public TMessage Message { get; set; }
    }

    public class ResponseService<TResponse, TAlternativeResponse, TMessage>
    {
        public TResponse Response { get; set; }
        public TAlternativeResponse AlternativeResponse { get; set; }
        public TMessage Message { get; set; } 
    }
}
