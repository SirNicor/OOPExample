using UCore;

namespace Ucore;

public class StudentPassportAddressDto
{
    public Student Student { get; set; }
    public Address Address { get; set; }
    public Passport Passport { get; set; } = null!;
}