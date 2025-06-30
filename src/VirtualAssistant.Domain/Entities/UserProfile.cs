namespace VirtualAssistant.Domain.Entities
{
    public class UserProfile
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; } // "Cidadao", "Colaborador"
        public List<string> Permissions { get; set; } = new();
    }
}