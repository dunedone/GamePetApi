using GamePetApi.Models;
using GamePetApi.Interfaces;

namespace GamePetApi.Data
{
    public class BookContextDAO : ICRUDDAO<Book>
    {
        private BookContext _context;

        public BookContextDAO(BookContext context)
        {
            _context = context;
        }

        public int? AddItem(Book book)
        {
            var duplicateBooks = _context.Books.Where(b => b.Title == book.Title && b.Author == book.Author).ToList();
            try
            {
                if (!duplicateBooks.Any())
                {
                    _context.Books.Add(book);
                    _context.SaveChanges();
                    return 0; 
                }
                return duplicateBooks.Count(); 
            }
            catch (Exception) { return -1; } 
        }

        public List<Book> GetAllItems()
        {
            return _context.Books.ToList();
        }

        public List<Book> GetFirstFiveItems()
        {
            return _context.Books.OrderBy(b => b.Id).Take(5).ToList();
        }

        public Book? GetItemById(int id)
        {
            return _context.Books.Where(b => b.Id == id).FirstOrDefault();
        }

        public int? RemoveItemById(int id)
        {
            var book = GetItemById(id);
            if (book is null) return null;
            try
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
                return 0; 
            }
            catch (Exception) { return -1; }
        }

        public int? UpdateItem(Book book)
        {
            var bookToUpdate = GetItemById(book.Id);
            if (bookToUpdate is null) return null;

            bookToUpdate.Title = book.Title;
            bookToUpdate.Author = book.Author;
            bookToUpdate.Genre = book.Genre;
            bookToUpdate.PublicationYear = book.PublicationYear;

            try
            {
                _context.Books.Update(bookToUpdate);
                _context.SaveChanges();
                return 0; 
            }
            catch (Exception) { return -1; }
        }
    }
}
