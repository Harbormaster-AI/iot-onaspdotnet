using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;
using iotonaspdotnet.Contracts;

namespace iotonaspdotnet.Api;

public static class ProvisioningRecordEndpoints
{
    public static IEndpointRouteBuilder MapProvisioningRecordEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/provisioningRecord").WithTags("ProvisioningRecords");

        group.MapPost("/", create);
        group.MapGet("/", get);
        group.MapGet("/", getAll);
        group.MapPut("/", update);
        group.MapDelete("/", delete);

        group.MapPut("/", assignDevice);
        group.MapPut("/", unassignDevice);
        group.MapPut("/", assignCertificate);
        group.MapPut("/", unassignCertificate);
        group.MapPut("/", assignTenant);
        group.MapPut("/", unassignTenant);


        return app;
    }

    private static async Task<IResult> Create(
        ProvisioningRecordRequest request,
        IProvisioningRecordService service,
        CancellationToken cancellationToken) {

        var model = mapRequestTo( request );

        try
        {
            await service.Create(lowercaseClassName, cancellationToken);
        }
        catch (InvalidOperationException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }

        return Results.NoContent();
    }

    private static async Task<IResult> Update(
        ProvisioningRecordRequest request,
        IProvisioningRecordService service,
        CancellationToken cancellationToken) {

        var model = mapRequestTo( request );

        try
        {
            var updated = await service.Update(model, cancellationToken);
            return updated ? Results.NoContent() : Results.NotFound();
        }
        catch (InvalidOperationException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }
    }


    private static async Task<IResult> Get(
        IdentifierRequest identifier,
        IProvisioningRecordService service,
        CancellationToken cancellationToken) {

        var provisioningRecord = await service.Get(identifier, cancellationToken);
        return provisioningRecord is null ? Results.NotFound() : Results.Ok( provisioningRecord );
    }


    private static async Task<IResult> GetAll(
        IProvisioningRecordService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( ProvisioningRecordResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IProvisioningRecordService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignDevice(
        AssociationRequest request,
        IProvisioningRecordService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignDevice(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignDevice(
    AssociationRequest request,
    IProvisioningRecordService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignDevice(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCertificate(
        AssociationRequest request,
        IProvisioningRecordService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCertificate(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCertificate(
    AssociationRequest request,
    IProvisioningRecordService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCertificate(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignTenant(
        AssociationRequest request,
        IProvisioningRecordService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignTenant(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignTenant(
    AssociationRequest request,
    IProvisioningRecordService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignTenant(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private ProvisioningRecord mapRequestToProvisioningRecord( ProvisioningRecordRequest request ) {
        var model = new ProvisioningRecord
        {
            Id = request.id,
            EnrolledAt = request.EnrolledAt,
            ProvisioningService = request.ProvisioningService,
            Device = request.Device,
            Certificate = request.Certificate,
            Tenant = request.Tenant,
            Method = request.Method,
            Status = request.Status,
        };
        return model;
    }

}
