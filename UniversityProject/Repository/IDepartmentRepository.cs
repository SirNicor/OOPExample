using Logger;

namespace Repository;

using UCore;
using static MyLogger;

public interface IDepartmentRepository
{
    public void Add(Department department, MyLogger myLogger);
    public List<Department> ReturnList(MyLogger myLogger);
}
