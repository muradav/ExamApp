namespace Exam.Dto.AppModel
{
    public class ResultModel<T>
    {
        public object Data { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
