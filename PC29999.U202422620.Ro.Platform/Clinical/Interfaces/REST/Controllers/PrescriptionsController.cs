using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using PC29999.U202422620.Ro.Platform.Clinical.Application;
using PC29999.U202422620.Ro.Platform.Clinical.Domain.Exceptions;
using PC29999.U202422620.Ro.Platform.Clinical.Interfaces.REST.Resources;
using PC29999.U202422620.Ro.Platform.Clinical.Interfaces.REST.Transform;

namespace PC29999.U202422620.Ro.Platform.Clinical.Interfaces.REST.Controllers;

[ApiController]
[Route("api/v1/patients/{patientId}/prescriptions")]
public sealed class PrescriptionsController(CreatePrescriptionService service) : ControllerBase
{
    [HttpPost]
    [EndpointSummary("Create a prescription for a patient")]
    [EndpointDescription("Creates a prescription resource for the patient identified in the URL. The patient identifier in the request body must match the URL.")]
    [ProducesResponseType<PrescriptionResource>(StatusCodes.Status201Created)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create(string patientId, [FromBody] CreatePrescriptionResource resource, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(patientId, out var routePatientId) || !Guid.TryParse(resource.PatientId, out var bodyPatientId))
        {
            throw new ArgumentException("Patient identifier must be a valid GUID.");
        }

        if (!PrescriptionAssembler.TryReadStatus(resource.Status, out var status))
        {
            throw new ArgumentException("Status must be Draft, Authorized, or Dispensed.");
        }

        var command = new CreatePrescriptionCommand(
            resource.PrescriptionNumber,
            bodyPatientId,
            resource.StartDate,
            resource.EndDate,
            resource.DailyQuantity,
            resource.ActiveIngredient,
            status,
            resource.ClinicalNotes);

        var prescription = await service.ExecuteAsync(routePatientId, command, cancellationToken);
        var response = PrescriptionAssembler.ToResource(prescription);
        return StatusCode(StatusCodes.Status201Created, response);
    }
}
