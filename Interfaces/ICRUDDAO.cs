using GamePetApi.Models;

namespace GamePetApi.Interfaces
{
    public interface ICRUDDAO<T>
    {
        int? AddItem(T item);
        List<T> GetAllItems();
        List<T> GetFirstFiveItems();
        T? GetItemById(int id);
        int? RemoveItemById(int id);
        int? UpdateItem(T item);
    }
}