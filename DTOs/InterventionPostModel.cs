namespace tracerapi.DTOs
{
    public class InterventionPostModel
    {
        public string Sujet { get; set; } = string.Empty;
        public string Compte { get; set; } = string.Empty;
        public DateTime? DateEcheance { get; set; }
        public DateTime? DateProchaineEcheance { get; set; }
        public DateTime? DateContrat { get; set; }
        public string CodeContrat { get; set; } = string.Empty;
        public string ContratMaintenance { get; set; } = string.Empty;
        public DateTime? DateAppel { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
        public string Owner { get; set; } = string.Empty;

        public IFormFile NewFile { get; set; } =null;
        public DateTime DateIntervention { get; set; } = DateTime.Now;
    }
   public class InterventionPutModel: InterventionPostModel
    {
        public int Id { get; set; }
        public string File { get; set; } = string.Empty;

    }
}
