namespace ApiTelegramBot;
public interface IUserStateRegistration
{  
    public void SetUserStateAsync(long chatId, UserStateRegEnum userState);
    public void IncrementUserStateAsync();
    public void DecrementUserStateAsync();
    public void ResetUserStateAsync();
}