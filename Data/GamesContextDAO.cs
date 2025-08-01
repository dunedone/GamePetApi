using GamePetApi.Models;
using GamePetApi.Interfaces;

namespace GamePetApi.Data
{
    public class GamesContextDAO : ICRUDDAO<VideoGame>
    {
        private GamesContext _context;
        
        public GamesContextDAO(GamesContext context)
        {
            _context = context;
        }

        public int? AddItem(VideoGame game)
        {
            var duplicateGames = _context.Games.Where(g => g.Title == game.Title && g.YearReleased == game.YearReleased && g.Genre == game.Genre && g.Developer == game.Developer && g.Platform == game.Platform).ToList();
            try
            {
                if (!duplicateGames.Any())
                {
                    _context.Games.Add(game);
                    _context.SaveChanges();
                    return 0;
                }
                return duplicateGames.Count();
            }
            catch (Exception) { return -1; }
        }

        public List<VideoGame> GetAllItems()
        {
            return _context.Games.ToList();
        }

        public List<VideoGame> GetFirstFiveItems()
        {
            return _context.Games.OrderBy(m => m.Id).Take(5).ToList();
        }

        public VideoGame GetItemById(int id)
        {
            if (id == 0 || id == null) GetFirstFiveItems();
            return _context.Games.Where(m => m.Id == id).FirstOrDefault();
        }

        public int? RemoveItemById(int id)
        {
            var game = GetItemById(id);
            if (game is null) return null;
            try
            {
                _context.Games.Remove(game);
                _context.SaveChanges();
                return 0;
            }
            catch (Exception) { return -1; }
        }

        public int? UpdateItem(VideoGame game)
        {
            var gameToUpdate = GetItemById(game.Id);
            if (gameToUpdate is null) return null;

            gameToUpdate.Title = game.Title;
            gameToUpdate.Developer = game.Developer;
            gameToUpdate.YearReleased = game.YearReleased;
            gameToUpdate.Genre = game.Genre;
            gameToUpdate.Platform = game.Platform;

            try
            {
                _context.Games.Update(gameToUpdate);
                _context.SaveChanges();
                return 0;
            }
            catch (Exception) { return -1; }
        }
    }
}