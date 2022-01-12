using System.Text.Json;

namespace Train_Reservation_Application.Models.Error
{
    public class ModelError
    {
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorStack { get; set; }
        public override string ToString() => JsonSerializer.Serialize(this);
    }
}
