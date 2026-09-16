using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;

namespace iotonaspdotnet.Api;

public static class MessagingEndpointEndpoints
{
    public static IEndpointRouteBuilder MapMessagingEndpointEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/messagingEndpoint").WithTags("MessagingEndpoints");

        group.MapGet("/", GetAll);
        group.MapGet("/{id:guid}", GetById);
        group.MapPost("/", Create);
        group.MapPut("/{id:guid}", Update);
        group.MapDelete("/{id:guid}", Delete);

        return app;
    }

    private static async Task<IResult> GetAll(
        IMessagingEndpointService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassNames = await service.GetAllAsync(cancellationToken);
        return Results.Ok(lowercaseClassNames.Select(ToResponse));
    }

    private static async Task<IResult> GetById(
        Guid id,
        IMessagingEndpointService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassName = await service.GetByIdAsync(id, cancellationToken);
        return lowercaseClassName is null ? Results.NotFound() : Results.Ok(ToResponse(lowercaseClassName));
    }

    private static async Task<IResult> Create(
        CreateMessagingEndpointRequest request,
        IMessagingEndpointService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassName = new MessagingEndpoint
        {
            Id = Guid.NewGuid(),

                Host = request.Host,
                Port = request.Port,
                Secure = request.Secure,
                Protocol = request.Protocol,

                TenantId = request.TenantId

        };

        try
        {
            await service.CreateAsync(lowercaseClassName, cancellationToken);
        }
        catch (InvalidOperationException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }

        return Results.Created($"/api/lowercaseClassNames/{lowercaseClassName.Id}", ToResponse(lowercaseClassName));
    }

    private static async Task<IResult> Update(
        Guid id,
        UpdateMessagingEndpointRequest request,
        IMessagingEndpointService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassName = new MessagingEndpoint
        {
            Id = id,
            MessagingEndpointNumber = request.MessagingEndpointNumber,
            Balance = request.Balance,
            CustomerId = request.CustomerId
        };

        try
        {
            var updated = await service.UpdateAsync(lowercaseClassName, cancellationToken);
            return updated ? Results.NoContent() : Results.NotFound();
        }
        catch (InvalidOperationException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }
    }

    private static async Task<IResult> Delete(
        Guid id,
        IMessagingEndpointService service,
        CancellationToken cancellationToken)
    {
        var deleted = await service.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static MessagingEndpointResponse ToResponse(MessagingEndpoint lowercaseClassName)
        => new(lowercaseClassName.Id, lowercaseClassName.MessagingEndpointNumber, lowercaseClassName.Balance, lowercaseClassName.CustomerId);
}
