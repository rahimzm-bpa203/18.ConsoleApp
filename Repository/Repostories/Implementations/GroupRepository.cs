using Domain.Entities;
using Repository.Data;
using Repository.Exceptions;
using Repository.Repostories.Interfaces;

namespace Repository.Repostories.Implementations
{
    public class LibraryRepository : IRepository<Group>
    {
        public void Create(Group data)
        {
            try
            {
                if (data == null) throw new NotFoundException("Data not found!");

                AppDbContext<Group>.datas.Add(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Delete(Group data)
        {
            AppDbContext<Group>.datas.Remove(data);
        }

        public Group Get(Predicate<Group> predicate)
        {
            return predicate != null ? AppDbContext<Group>.datas.Find(predicate) : null;
        }

        public List<Group> GetAll(Predicate<Group> predicate = null)
        {
            return predicate != null ? AppDbContext<Group>.datas.FindAll(predicate) : AppDbContext<Group>.datas;
        }

        public void Update(Group data)
        {
            Group dbGroup = Get(l => l.Id == data.Id);

            if (dbGroup == null) return;

            if (!string.IsNullOrEmpty(data.Name))
            {
                dbGroup.Name = data.Name;
            }

            if (!string.IsNullOrEmpty(data.Teacher))
            {
                dbGroup.Teacher = data.Teacher;
            }

            if (!string.IsNullOrEmpty(data.Room))
            {
                dbGroup.Room = data.Room;
            }

        }
    }
}
