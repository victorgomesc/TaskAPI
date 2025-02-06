namespace TaskApi.Models;

public record TaskRequest(
    string taskName,
    string category,
    string priority,
    DateTime date
);