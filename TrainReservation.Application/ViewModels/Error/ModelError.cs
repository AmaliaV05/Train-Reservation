using System.Collections.Generic;
using System.Text.Json;

namespace TrainReservation.Application.ViewModels.Error
{
    public class ModelError
    {
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public Dictionary<string,string[]> Errors { get; set; }
        public string ErrorStack { get; set; }
        public string TimeStamp { get; set; }
        public string Path { get; set; }
        public string TraceId { get; set; }
        public override string ToString() => JsonSerializer.Serialize(this);
    }
}
