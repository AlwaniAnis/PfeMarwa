namespace tracerapi.Models
{
    public class Intervention
    {
        public int Id { get; set; }
        public string Sujet { get; set; } 
        public string Titre { get; set; }
        public string Compte { get; set; } 
        public DateTime? DateEcheance { get; set; }
        public DateTime? DateProchaineEcheance { get; set; }
        public DateTime? DateContrat { get; set; }
        public string CodeContrat { get; set; } 
        public string ContratMaintenance { get; set; } 
        public DateTime? DateAppel { get; set; }
        public string Type { get; set; } 
        public string Note { get; set; } 
        public string Owner { get; set; } 
       
        public string File { get; set; } 
        public DateTime DateIntervention { get; set; } = DateTime.Now;
    }
}
