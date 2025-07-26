namespace GamePetApi.Models
{
    public class VideoGame
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int YearReleased { get; set; }
        public string Developer { get; set; }
        public string Genre { get; set; }
        public string Platform { get; set; }
    }
}