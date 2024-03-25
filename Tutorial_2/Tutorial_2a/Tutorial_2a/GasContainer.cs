namespace Tutorial_2a;

public class GasContainer : Container, IHazardNotifier
{
    public double Pressure { get; private set; }

    public GasContainer(int height, double tareWeight, int depth, double maximumPayload, double pressure)
        : base(height, tareWeight, depth, maximumPayload)
    {
        this.Pressure = pressure;
    }

    public override void LoadCargo(double mass)
    {
        if (mass > MaximumPayload)
        {
            NotifyHazard("Attempted to load beyond container's capacity.");
            throw new OverfillException("Gas container overload: mass of the cargo exceeds the allowable payload.");
        }

        base.LoadCargo(mass);
    }

    public void NotifyHazard(string message)
    {
        Console.WriteLine(
            $"Notification: Hazardous cargo detected.\nContainer Serial Number: {SerialNumber}: {message}");
    }

    protected override string GetSerialNumberPrefix()
    {
        return "G";
    }
}