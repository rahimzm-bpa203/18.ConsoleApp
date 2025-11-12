using Domain.Entities;

namespace Service.Services.Interfaces
{
    public interface IGroupService
    {
        Group Create(Group group);
        Group Update(int id, Group group);
        void Delete(int id);
        Group GetById(int id);
        List<Group> GetByTeacher(string teacher);
        List<Group> GetByRoom(string room);
        List<Group> Search(string searchText);
        List<Group> GetAll();
    }
}
