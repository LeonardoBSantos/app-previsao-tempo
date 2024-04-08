namespace Application.Model
{
    public class SearchHistoryModel
    {
        public List<HistoryModel> lista_historico { get; set; }
    }

    public class HistoryModel
    {
        public string cidade { get; set; }
        public string data { get; set; }
    }
}
