using iotonaspdotnet.Service;
using iotonaspdotnet.Domain;

namespace iotonaspdotnet.Api;

public static class HardwareModuleEndpoints
{
    public static IEndpointRouteBuilder MapHardwareModuleEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/hardwareModule").WithTags("HardwareModules");

        group.MapGet("/", GetAll);
        group.MapGet("/{id:guid}", GetById);
        group.MapPost("/", Create);
        group.MapPut("/{id:guid}", Update);
        group.MapDelete("/{id:guid}", Delete);

        return app;
    }

    private static async Task<IResult> GetAll(
        IHardwareModuleService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassNames = await service.GetAllAsync(cancellationToken);
        return Results.Ok(lowercaseClassNames.Select(ToResponse));
    }

    private static async Task<IResult> GetById(
        Guid id,
        IHardwareModuleService service,
        CancellationToken cancellationToken)
    {
        var hardwareModule = await service.GetByIdAsync(id, cancellationToken);
        return hardwareModule is null ? Results.NotFound() : Results.Ok(ToResponse( hardwareModule ));
    }

    private static async Task<IResult> Create(
        CreateHardwareModuleRequest request,
        IHardwareModuleService service,
        CancellationToken cancellationToken)
    {
        var hardwareModule = new HardwareModule
        {
            Id = Guid.NewGuid(),

                ModuleCode = request.ModuleCode,
                DatasheetUri = request.DatasheetUri,
                ModuleType = request.ModuleType,

                DeviceVendorId = request.DeviceVendorId,

        };

        try
        {
            await service.CreateAsync(lowercaseClassName, cancellationToken);
        }
        catch (InvalidOperationException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }

        return Results.Created($"/api/hardwareModules/hardwareModule.Id", ToResponse(lowercaseClassName));
    }

    private static async Task<IResult> Update(
        Guid id,
        UpdateHardwareModuleRequest request,
        IHardwareModuleService service,
        CancellationToken cancellationToken)
    {
        var lowercaseClassName = new HardwareModule
        {
            Id = id,
            ModuleCode = request.ModuleCode,
            DatasheetUri = request.DatasheetUri,
            ModuleType = request.ModuleType,

            DeviceVendorId = request.DeviceVendorId,
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
        IHardwareModuleService service,
        CancellationToken cancellationToken)
    {
        var deleted = await service.DeleteAsync(id, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static HardwareModuleResponse ToResponse(HardwareModule lowercaseClassName)
        => new( hardwareModule.Id,
                , String, Uri_, ModuleType
                , DeviceVendorId );

}
