namespace Test.CommonUtilities.ValueObjects;

public abstract class CatalogDescriptionBuilder
{
    public static (string name, decimal price) Build()
    {
        const string name = "Corte de cabelo + barba";
        const decimal price = 30;

        return (name, price);
    }
}