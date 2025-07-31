namespace GamePetApi.Models
{
    public class Pet
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string Species { get; set; }
        public required string Gender { get; set; }
        public required DateTime BirthDate { get; set; }
        public required string OwnerName { get; set; }
    }
}
