namespace StudentManagement.DTOs
{
    public class StudentQueryParams
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 5;

        public string? Search { get; set; }
    }
}
