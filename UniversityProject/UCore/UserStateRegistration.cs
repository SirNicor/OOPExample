namespace UCore;

public class UserStateRegistration : IUserStateRegistration
{
    public void SetUserStateAsync(long chatId, UserStateRegEnum userState)
    {
        ChatId = chatId;
        UserState = userState;
    }

    public void IncrementUserStateAsync()
    {
        UserState++;
    }

    public void DecrementUserStateAsync()
    {
        UserState--;
    }

    public void ResetUserStateAsync()
    {
        UserState = 0;
    }
    
    public long ChatId { get; set; }
    public UserStateRegEnum UserState { get; set; }
    public long UniversityId { get; set; }
    public long FacultyId{ get; set; }
    public long DepartmentId{ get; set; }
    public long DirectionId{ get; set; }
    public long StudentId{ get; set; }
    public string StudentFirstName{ get; set; }
    public string StudentLastName{ get; set; }
}