using MediatR;
using PeopleOS.Application.Common.Results;

namespace PeopleOS.Application.Features.Organizations.CreateOrganization;

public record CreateOrganizationCommand(
    string Name,
    string? Description
) : IRequest<Result<Guid>>;