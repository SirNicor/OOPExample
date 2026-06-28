namespace EFRepository;

public class StudentPassportAddressDto
{
    public EFStudent Student { get; set; }
    public EFAddress Address { get; set; }
    public EFPassport Passport { get; set; } = null!;
}