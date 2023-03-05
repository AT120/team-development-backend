namespace TeamDevelopmentBackend.Services.Interfaces;

public interface IGlobalCounter<T>
{
    T Next();
}