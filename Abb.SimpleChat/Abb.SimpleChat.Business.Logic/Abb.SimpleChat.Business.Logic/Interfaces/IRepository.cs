namespace Abb.SimpleChat.Business.Logic.Interfaces
{
    public interface IRepository<T>
        where T : class
    {
        T GetItem(int id);          // получение одного объекта по id
        void Add(T item);           // создание объекта
        void Update(T item);        // обновление объекта
        void DeleteItem(int id);    // удаление объекта по id
    }
}
