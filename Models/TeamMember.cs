namespace GamePetApi.Models
{
    public class TeamMember
    {
        public required int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required DateOnly BirthDate { get; set; }
        public required string Program { get; set; }
        public required string Year { get; set; }
    }
}
