namespace tracerapi.DTOs
{
    public class IncidentPostModel
    {
       
            public string Titre { get; set; } = string.Empty;
            public string Sujet { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public string NumContratMaintenance { get; set; } = string.Empty;
            public string NumSerie { get; set; } = string.Empty;
            public string TypePrestation { get; set; } = string.Empty;
            public string Type { get; set; } = string.Empty;
            public string AskToClose { get; set; } = string.Empty;
            public string Note { get; set; } = string.Empty;
            public string Statut { get; set; } = string.Empty;
            public string Priorite { get; set; } = string.Empty;
            public string Owner { get; set; } = string.Empty;
            public IFormFile NewFile { get; set; } = null;
            public DateTime DateIncident { get; set; } = DateTime.Now;
        
    }
    public class IncidentPutModel:IncidentPostModel
    {
        public int Id { get; set; }
        public string File { get; set; } = string.Empty;



    }
}
