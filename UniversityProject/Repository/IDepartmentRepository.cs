using Logger;

namespace Repository;

using UCore;
using static MyLogger;

public interface IDepartmentRepository
{
    public long Create(DepartmentDto department);
    public Department Get(long Id);
    public List<Department> ReturnList();
    public void Delete(long ID);
    public long Update(DepartmentDto department);
}
