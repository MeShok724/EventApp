namespace eventApp.API.Contracts
{
    public record EventResponse
    (
        Guid Id,
        string Name,
        string Description,
        DateTime DateTime,
        string Location,
        string Category,
        int MaxParticipants,
        byte[] Image
    );
    public record EventRequest
    (
        string Name,
        string Description,
        DateTime DateTime,
        string Location,
        string Category,
        int MaxParticipants,
        byte[] Image
    );
}
