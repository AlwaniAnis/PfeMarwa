namespace tracerapi.Models
{
    public class Incident
    {
        public int Id { get; set; }
        public string Titre { get; set; } 
        public string Sujet { get; set; } 
        public string Description { get; set; } 
        public string NumContratMaintenance { get; set; } 
        public string NumSerie { get; set; } 
        public string TypePrestation { get; set; } 
        public string Type { get; set; } 
        public CloseStatus AskToClose { get; set; } 
        public string Note { get; set; } 
        public string Statut { get; set; } 
        public string Priorite { get; set; } 
        public string Owner { get; set; } 
        public string UserEmail { get; set; }
        public string File { get; set; } 
        public decimal duration { get; set; }
        public DateTime DateIncident { get; set; } 
        public bool closed { get; set; }
    }
    public enum CloseStatus{
        open, askedToBeClosed, closed
    };
}
