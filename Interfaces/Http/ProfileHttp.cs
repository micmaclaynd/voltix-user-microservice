namespace Voltix.UserMicroservice.Interfaces.Http;

public class IProfile {
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public required string Patronymic { get; set; }
    public required string? Description { get; set; }
}

public class IUpdateProfileRequest {
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public required string Patronymic { get; set; }
    public required string? Description { get; set; }
}