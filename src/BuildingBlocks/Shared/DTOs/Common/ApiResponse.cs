namespace M4Webapp.Shared.DTOs.Common;
public record ApiResponse<T>(T Data, string? Message = null, bool Success = true);
public record ApiErrorResponse(string Message, IEnumerable<string>? Errors = null, bool Success = false);
