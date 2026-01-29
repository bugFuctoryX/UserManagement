namespace UserManagement.Application.Common;

public sealed record AppError(
  ErrorType Type,
  string Message,
  IReadOnlyDictionary<string, string[]>? ValidationErrors = null);
