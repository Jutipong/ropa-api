namespace WebApi.Models
{
    public class QTableModel
    {
        public int Page { get; set; } = 1;
        public int RowsPerPage { get; set; } = 25;
        public string SortBy { get; set; }
        public bool Descending { get; set; } = false;
    }
}
