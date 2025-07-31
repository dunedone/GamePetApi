using GamePetApi.Models;
using GamePetApi.Interfaces;

namespace GamePetApi.Data
{
    public class TeamContextDAO : ICRUDDAO<TeamMember>
    {
        private TeamContext _context;
        
        public TeamContextDAO(TeamContext context)
        {
            _context = context;
        }

        public int? AddItem(TeamMember member)
        {
            var duplicateMembers = _context.Team.Where(m => m.FirstName == member.FirstName && m.LastName == member.LastName && m.BirthDate == member.BirthDate).ToList();
            try
            {
                if (duplicateMembers.Any())
                {
                    _context.Team.Add(member);
                    _context.SaveChanges();
                    return 0;
                }
                return duplicateMembers.Count();
            }
            catch (Exception) { return -1; }
        }

        public List<TeamMember> GetAllItems()
        {
            return _context.Team.ToList();
        }

        public List<TeamMember> GetFirstFiveItems()
        {
            return _context.Team.OrderBy(m => m.Id).Take(5).ToList();
        }

        public TeamMember GetItemById(int id)
        {
            if (id == 0 || id == null) GetFirstFiveItems();
            return _context.Team.Where(m => m.Id == id).FirstOrDefault();
        }

        public int? RemoveItemById(int id)
        {
            var member = GetItemById(id);
            if (member is null) return null;
            try
            {
                _context.Team.Remove(member);
                _context.SaveChanges();
                return 0;
            }
            catch (Exception) { return -1; }
        }

        public int? UpdateItem(TeamMember member)
        {
            var memberToUpdate = GetItemById(member.Id);
            if (memberToUpdate is null) return null;

            memberToUpdate.BirthDate = member.BirthDate;
            memberToUpdate.FirstName = member.FirstName;
            memberToUpdate.LastName = member.LastName;
            memberToUpdate.Program = member.Program;
            memberToUpdate.Year = member.Year;

            try
            {
                _context.Team.Update(memberToUpdate);
                _context.SaveChanges();
                return 0;
            }
            catch (Exception) { return -1; }
        }
    }
}