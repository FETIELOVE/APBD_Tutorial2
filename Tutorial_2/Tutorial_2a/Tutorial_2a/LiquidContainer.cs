namespace Tutorial_2a;

public class LiquidContainer : Container, IHazardNotifier
{
    public bool IsHazardous { get; private set; }

    public LiquidContainer(int height, double tareWeight, int depth, double maximumPayload, bool isHazardous)
        : base(height, tareWeight, depth, maximumPayload)
    {
        this.IsHazardous = isHazardous;
    }

    public override void LoadCargo(double mass)
    {
        double permittedMass = IsHazardous ? MaximumPayload * 0.5 : MaximumPayload * 0.9; 
        if (mass > permittedMass)
        {
            NotifyHazard($"Attempt to load {mass}kg into a container with a limit of {permittedMass}kg detected.");
            throw new OverfillException(
                "Attempt to load hazardous cargo above 50% capacity or non-hazardous cargo above 90% capacity detected.");
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
        return "L";
    }
}
