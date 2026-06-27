namespace IRepositoryAll;
using UCore;

public interface IAuthorizationRepository
{
    public Task<AuthorizationDto> GetForLoginAuthorizationAsync(string login);
    public Task<AuthorizationDto> GetForIdAuthorizationAsync(long id);
    public Task<long> CreateAuthorizationAsync(AuthorizationDto dto);
    public Task<long> UpdateAuthorizationAsync(AuthorizationDto dto);
    public Task<(long?, int[]?)> GetAuthorizationsRoleForIndexAsync(AuthorizationForGetJwtToken dto);
    public Task<string[]> GetAllRolesAsync(int[] idRoles);
    public Task<bool> CheckPasswordAsync(string password, long id);
    public Task<RefreshJWTTokenDTO> GetJWTTokenAsync(string token);
    public Task<long?> CheckAndUpdateJWTTokenAsync(string token);
    public Task<long> CreateJWTTokenAsync(RefreshJWTTokenDTO dto);
    public Task DeleteJWTTokenAsync(string token);
}