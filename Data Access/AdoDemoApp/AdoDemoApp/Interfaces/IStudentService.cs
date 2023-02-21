using AdoDemoApp.Models;

namespace AdoDemoApp.Interfaces;
public interface IStudentService
{
    void Create(Student student);

    Student? Get(int id);

    IEnumerable<Student> GetAll();

    void Update(Student student);

    void Delete(int id);

    IEnumerable<Student> FindByName(string name);
}
