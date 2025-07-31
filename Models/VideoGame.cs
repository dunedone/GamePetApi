namespace GamePetApi.Models
{
    public class VideoGame
    {
        public required int Id { get; set; }
        public required string Title { get; set; }
        public required int YearReleased { get; set; }
        public required string Developer { get; set; }
        public required string Genre { get; set; }
        public required string Platform { get; set; }
    }
}