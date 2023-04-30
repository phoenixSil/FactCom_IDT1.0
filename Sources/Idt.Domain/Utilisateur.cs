namespace Idt.Domain
{
    public class Utilisateur: BaseDomainEntite
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string PseudoUtilisateur { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Adresse> Adresses { get; set; }
        public List<Message> Messages { get; set; }
    }
}
