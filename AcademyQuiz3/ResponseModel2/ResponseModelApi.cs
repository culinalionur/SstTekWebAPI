namespace AcademyQuiz3.ResponseModel2
{
    public class ResponseModelApi
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public List<dynamic> Data { get; set; }

        public ResponseModelApi()
        {
            Data = new List<dynamic>();
        }
    }
}
