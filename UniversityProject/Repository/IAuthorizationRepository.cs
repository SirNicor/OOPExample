namespace Repository;
using UCore;

public interface IAuthorizationRepository
{
    public AuthorizationDto GetForLoginAuthorization(string login);
    public AuthorizationDto GetForIdAuthorization(long id);
    public long CreateAuthorization(AuthorizationDto dto);
    public long UpdateAuthorization(AuthorizationDto dto);
    public long? GetAuthorizationsForIndex(AuthorizationForGetJwtToken dto);
    public bool CheckPassword(string password, long id);
    public RefreshJWTTokenDTO GetJWTToken(string token);
    public long? CheckAndUpdateJWTToken(string token);
    public long CreateJWTToken(RefreshJWTTokenDTO dto);
    public void DeleteJWTToken(string token);
}