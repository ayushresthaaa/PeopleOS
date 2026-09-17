namespace PeopleOS.Api.Contracts.Organizations;

public record CreateOrganizationRequest(
    string Name,
    string? Description
);