namespace UCore;

public class RoleAccessDto
{
    public int Id { get; set; }
    public string NameRole { get; set; }
    public Tuple<string, string>[] TypeOperationAccessPage { get; set; }
}

public class RoleAccessRaw
{
    public int Id { get; set; }
    public string NameRole { get; set; }
    public string TypeOperation { get; set; }
    public string AccessPage { get; set; }
}
