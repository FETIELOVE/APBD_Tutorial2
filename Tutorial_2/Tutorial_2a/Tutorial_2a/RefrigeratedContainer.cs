namespace Tutorial_2a;

public class RefrigeratedContainer : Container
{
    private static readonly Dictionary<string, double> ProductTemperatureRequirements = new Dictionary<string, double>
    {
        { "Bananas", 13.3 },
        { "Chocolate", 18 },
        { "Fish", 2 },
        { "Meat", -15 },
        { "Ice cream", -18 },
        { "Frozen pizza", -30 },
        { "Cheese", 7.2 },
        { "Sausages", 5 },
        { "Butter", 20.5 },
        { "Eggs", 19 }
    };

    public string ProductType { get; private set; } = "";
    public double Temperature { get; private set; }

    public RefrigeratedContainer(int height, double tareWeight, int depth, double maximumPayload, string productType,
        double temperature)
        : base(height, tareWeight, depth, maximumPayload)
    {
        SetProduct(productType, temperature);
    }

    protected override string GetSerialNumberPrefix()
    {
        return "C";
    }

    public override void LoadCargo(double mass)
    {
        if (mass > MaximumPayload)
            throw new OverfillException(
                $"Refrigerated container overload: mass of the cargo exceeds the allowable payload for {ProductType}.");
        
        base.LoadCargo(mass);
    }

    public void SetProduct(string productType, double temperature)
    {
        if (!ProductTemperatureRequirements.ContainsKey(productType))
            throw new ArgumentException("Invalid product type.");

        if (!IsTemperatureSuitable(productType, temperature)) 
            throw new InvalidOperationException(
                $"The temperature of the container cannot be lower than the required temperature: {productType}.");

        this.ProductType = productType;
        this.Temperature = temperature;
    }

    private bool IsTemperatureSuitable(string productType, double temperature)
    {
        if (ProductTemperatureRequirements.TryGetValue(productType, out double requiredTemperature))
            return temperature >= requiredTemperature;

        return false; 
    }

    public void AdjustTemperature(double newTemperature)
    {
        if (!IsTemperatureSuitable(this.ProductType, newTemperature))
            throw new InvalidOperationException($"The new temperature {newTemperature} is not suitable to set for this product type: {this.ProductType}");

        this.Temperature = newTemperature;
    }
}
    
