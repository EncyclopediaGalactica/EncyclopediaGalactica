namespace EncyclopediaGalactica.Infrastructure.Graphql.ErrorFilters;

using HotChocolate;

public class CustomErrorFilter<TException> : IErrorFilter
    where TException : Exception
{
    public IError OnError(IError error)
    {
        return HandleCustomError(error);
    }

#pragma warning disable CS0693 // Type parameter has the same name as the type parameter from outer type
    protected IError HandleCustomError(IError error)
#pragma warning restore CS0693 // Type parameter has the same name as the type parameter from outer type
    {
        if (error.Exception != null
            && !string.IsNullOrEmpty(error.Exception.Source)
            && error.Exception.GetType().Name == typeof(TException).Name)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            error = RemoveSensitiveInformation(error);
            TException exception = error.Exception as TException;
            return error.WithMessage(exception.Message);

#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        return error;
    }

    private IError RemoveSensitiveInformation(IError error)
    {
        return error.RemoveExtension("field")
            .RemoveExtension("code")
            .RemoveLocations()
            .RemovePath();
    }
}