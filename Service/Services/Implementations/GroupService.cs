using Domain.Entities;
using Repository.Repostories.Implementations;
using Service.Services.Interfaces;

namespace Service.Services.Implementations
{
    public class GroupService : IGroupService
    {
        private GroupRepository _groupRepository;
        private int _count = 1;

        public GroupService()
        {
            _groupRepository = new GroupRepository();
        }

        public Group Create(Group group)
        {
            group.Id = _count;

            _groupRepository.Create(group);

            _count++;

            return group;
        }

        public void Delete(int id)
        {
            Group group = GetById(id);

            _groupRepository.Delete(group);
        }

        public List<Group> GetAll()
        {
            return _groupRepository.GetAll();
        }

        public Group GetById(int id)
        {
            Group group = _groupRepository.Get(g => g.Id == id);

            if (group is null) return null;

            return group;
        }
        public List<Group> GetByTeacher(string teacher) 
        {
            if (string.IsNullOrWhiteSpace(teacher))
                return new List<Group>();

            return _groupRepository.GetAll(g => g.Teacher != null && g.Teacher.Trim().ToLower() == teacher.Trim().ToLower());
        }

        public List<Group> GetByRoom(string room)
        {
            if (string.IsNullOrWhiteSpace(room))
                return new List<Group>();

            return _groupRepository.GetAll(g => g.Room != null && g.Room.Trim().ToLower() == room.Trim().ToLower());
        }

        public List<Group> Search(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return new List<Group>();

            return _groupRepository.GetAll(g => g.Name != null && g.Name.Trim().ToLower() == name.Trim().ToLower());
        }

        public Group Update(int id, Group group)
        {
           Group dbGroup = GetById(id);

            if (dbGroup is null) return null;

            group.Id = id;

            _groupRepository.Update(group);

            return GetById(id);
        }

      
    }
}
