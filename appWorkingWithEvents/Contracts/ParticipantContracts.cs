namespace eventApp.API.Contracts
{
    public record ParticipantResponse(
        Guid Id,
        string FirstName,
        string LastName,
        DateTime DateOfBirth,
        string Email
    );
    public record ParticipantRequest(
        string FirstName,
        string LastName,
        DateTime DateOfBirth,
        string Email
    );
}
