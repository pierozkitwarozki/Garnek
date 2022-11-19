namespace Garnek.Infrastructure.Constans;

public static class ValidatorMessages
{
    public static string MinItemCount(string collectionName, int minNumber) =>
        $"You must enter at least {minNumber} {collectionName}";
    
    public static string MaxItemCount(string collectionName, int maxNumber) =>
        $"You must enter maximum {maxNumber} {collectionName}";
    
    public static string MinLength(string fieldName, int minLength) =>
        $"{fieldName} must be at least {minLength} characters long";
    
    public static string MaxLength(string fieldName, int maxLength) =>
        $"{fieldName} must be maximum {maxLength} characters long";
    
    public static string MustDiffer(string collectionName) =>
        $"{collectionName} values must be different";
    
    public static string NullOrEmpty(string fieldName) =>
        $"{fieldName} cannot be null/empty";
}