namespace Repository;
using UCore;

public interface IRoleRepository
{
    public RoleAccessDto GetRoleAccess(int roleId);
}