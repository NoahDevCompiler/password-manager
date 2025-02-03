namespace password_manager_API.Models
{
    public class DBResult
    {
        public bool Success { get; set; }       
        public string Error { get; set; }
        public object? Message { get; set; }

        public DBResult(bool _success, string _error, object _message) { 

            Success = _success;          
            Error = _error;
            Message = _message;
        }
        public DBResult(bool _success, string _error) {
            Success = _success;
            Error = _error;

        }
    }
}
