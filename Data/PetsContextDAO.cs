using GamePetApi.Models;
using GamePetApi.Interfaces;

namespace GamePetApi.Data
{
    public class PetsContextDAO : ICRUDDAO<Pet>
    {
        private PetsContext _context;

        public PetsContextDAO(PetsContext context)
        {
            _context = context;
        }

        public int? AddItem(Pet pet)
        {
            var duplicatePets = _context.Pets.Where(p => p.Name == pet.Name && p.Species == pet.Species).ToList();
            try
            {
                if (!duplicatePets.Any())
                {
                    _context.Pets.Add(pet);
                    _context.SaveChanges();
                    return 0;
                }
                return duplicatePets.Count();
            }
            catch (Exception) { return -1; }
        }

        public List<Pet> GetAllItems()
        {
            return _context.Pets.ToList();
        }

        public List<Pet> GetFirstFiveItems()
        {
            return _context.Pets.OrderBy(p => p.Id).Take(5).ToList();
        }

        public Pet? GetItemById(int id)
        {
            return _context.Pets.Where(p => p.Id == id).FirstOrDefault();
        }

        public int? RemoveItemById(int id)
        {
            var pet = GetItemById(id);
            if (pet is null) return null;
            try
            {
                _context.Pets.Remove(pet);
                _context.SaveChanges();
                return 0;
            }
            catch (Exception) { return -1; }
        }

        public int? UpdateItem(Pet pet)
        {
            var petToUpdate = GetItemById(pet.Id);
            if (petToUpdate is null) return null;

            petToUpdate.Name = pet.Name;
            petToUpdate.Species = pet.Species;
            petToUpdate.Gender = pet.Gender;
            petToUpdate.BirthDate = pet.BirthDate;
            petToUpdate.OwnerName = pet.OwnerName;

            try
            {
                _context.Pets.Update(petToUpdate);
                _context.SaveChanges();
                return 0;
            }
            catch (Exception) { return -1; }
        }
    }
}
