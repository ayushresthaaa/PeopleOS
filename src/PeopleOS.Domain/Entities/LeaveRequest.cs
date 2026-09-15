using PeopleOS.Domain.Common;
using PeopleOS.Domain.Enums;

namespace PeopleOS.Domain.Entities;
//One thing we're intentionally leaving out for now is leave overlap detection. Whether an employee already has leave covering those dates requires querying other leave requests, so we'll handle that when we build the Application/use-case layer.


public class LeaveRequest : AuditableEntity
{
    public Guid EmployeeId { get; private set; }

    public DateOnly StartDate { get; private set; }

    public DateOnly EndDate { get; private set; }

    public string Reason { get; private set; } = null!;

    public LeaveStatus Status { get; private set; }

    private LeaveRequest()
    {
        // Required by EF Core.
    }

    public LeaveRequest(
        Guid employeeId,
        DateOnly startDate,
        DateOnly endDate,
        string reason)
    {
        if (employeeId == Guid.Empty)
        {
            throw new ArgumentException(
                "EmployeeId cannot be empty.",
                nameof(employeeId));
        }

        if (startDate > endDate)
        {
            throw new ArgumentException(
                "Start date cannot be later than end date.",
                nameof(endDate));
        }

        if (string.IsNullOrWhiteSpace(reason))
        {
            throw new ArgumentException(
                "Leave reason cannot be empty.",
                nameof(reason));
        }

        EmployeeId = employeeId;
        StartDate = startDate;
        EndDate = endDate;
        Reason = reason.Trim();
        Status = LeaveStatus.Pending;
    }

    public void Approve()
    {
        if (Status != LeaveStatus.Pending)
        {
            throw new InvalidOperationException(
                "Only pending leave requests can be approved.");
        }

        Status = LeaveStatus.Approved;
    }

    public void Reject()
    {
        if (Status != LeaveStatus.Pending)
        {
            throw new InvalidOperationException(
                "Only pending leave requests can be rejected.");
        }

        Status = LeaveStatus.Rejected;
    }

    public void Cancel()
    {
        if (Status != LeaveStatus.Pending)
        {
            throw new InvalidOperationException(
                "Only pending leave requests can be cancelled.");
        }

        Status = LeaveStatus.Cancelled;
    }
}