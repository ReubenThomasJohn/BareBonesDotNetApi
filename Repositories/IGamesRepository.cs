using StudentApi.Entities;

namespace StudentApi.Repositories
{
    public interface IGamesRepository
    {
        void Create(Student student);

        void Delete(int id);

        Student? Get(int id);

        IEnumerable<Student> GetAll();

        void Update(Student updatedStudent);
    }
}