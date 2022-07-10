namespace tracerapi.DTOs
{
    public class TachePostModel
    {
        public string Sujet { get; set; } = string.Empty;
        public string Compte { get; set; } = string.Empty;
        public string Titre { get; set; }

        public DateTime? DateEcheance { get; set; }
        public DateTime? DateProchaineEcheance { get; set; }
        public DateTime? DateContrat { get; set; }
        public string CodeContrat { get; set; } = string.Empty;
        public string ContratMaintenance { get; set; } = string.Empty;
        public DateTime? DateAppel { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
        public string Owner { get; set; } = string.Empty;
        public DateTime DateTache { get; set; } = DateTime.Now; public IFormFile NewFile { get; set; } = null;

    }
    public class TachePutModel: TachePostModel
    {
        public int Id { get; set; }
        public string File { get; set; } = string.Empty;
    }
}
