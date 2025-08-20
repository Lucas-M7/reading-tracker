namespace ReadingTracker.API.Services.Auth;

public interface ICurrentUserService
{
    Guid GetUserId(); // lança se não autenticado
}