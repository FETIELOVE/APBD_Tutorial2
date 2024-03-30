namespace Tutorial_2a;

public abstract class Container
{
    private static HashSet<string> _usedSerialNumbers = new HashSet<string>();
    private static int _serialCounter = 0;
    public double CargoMass { get; protected set; }
    public int Height { get; private set; }
    public double TareWeight { get; private set; }
    public int Depth { get; private set; }
    public string SerialNumber { get; private set; }
    public double MaximumPayload { get; private set; }

    protected Container(int height, double tareWeight, int depth, double maximumPayload)
    {
        this.Height = height;
        this.TareWeight = tareWeight; 
        this.Depth = depth;
        this.MaximumPayload = maximumPayload;
        this.SerialNumber = GenerateSerialNumber();
    }

    private string GenerateSerialNumber()
    {
        string serialNumber;
        do
        {
            serialNumber = $"KON-{GetSerialNumberPrefix()}-{++_serialCounter}"; 
        } while (_usedSerialNumbers.Contains(serialNumber));

        _usedSerialNumbers.Add(serialNumber);
        return serialNumber;
    }

    protected abstract string GetSerialNumberPrefix();

    public virtual void LoadCargo(double mass)
    {
        if (mass > MaximumPayload)
            throw new OverfillException("Mass of the cargo is greater than the capacity of a given container.");

        CargoMass = mass;
    }

    public virtual void EmptyCargo()
    {
        CargoMass = 0;
    }

    public override string ToString()
    {
        return SerialNumber; 
    }
}