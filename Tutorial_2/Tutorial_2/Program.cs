


namespace Tutorial_2;
public class Container
{
    public string SerialNumber { get; private set; }
    public double CargoMass { get; protected set; }
    public int Height { get; private set; }
    public double TareWeight { get; private set; }
    public int Depth { get; private set; }
    public double MaxPayload { get; private set; }

    public Container(string serialNumber, int height, int depth, double tareWeight, double maxPayload)
    {
        SerialNumber = serialNumber;
        Height = height;
        Depth = depth;
        TareWeight = tareWeight;
        MaxPayload = maxPayload;
    }

   
    public virtual void LoadCargo(double mass)
    {
        if (mass > MaxPayload)
        {
            throw new OverfillException($"Cargo mass ({mass} kg) exceeds the maximum payload ({MaxPayload} kg) of container {SerialNumber}.");
        }

        CargoMass = mass;
    }

    public void EmptyCargo()
    {
        CargoMass = 0;
    }
}
    
  
public interface IHazardNotifier
{
    void NotifyHazard(string containerNumber);
}


public class LiquidContainer : Container, IHazardNotifier
{
    public bool IsHazardous { get; private set; }

    public LiquidContainer(string serialNumber, int height, int depth, double tareWeight, double maxPayload, bool isHazardous)
        : base(serialNumber, height, depth, tareWeight, maxPayload)
    {
        IsHazardous = isHazardous;
    }

    
    public override void LoadCargo(double mass)
    {
        if (IsHazardous && mass > MaxPayload * 0.5)
        {
            throw new InvalidOperationException($"Hazardous cargo mass ({mass} kg) exceeds 50% of the maximum payload ({MaxPayload} kg) of container {SerialNumber}.");
        }
        else if (!IsHazardous && mass > MaxPayload * 0.9)
        {
            throw new InvalidOperationException($"Cargo mass ({mass} kg) exceeds 90% of the maximum payload ({MaxPayload} kg) of container {SerialNumber}.");
        }

        base.LoadCargo(mass);
    }

    
    public void NotifyHazard(string containerNumber)
    {
        Console.WriteLine($"Hazard detected in container {containerNumber}");
    }
}



public class OverfillException : Exception
{
    public OverfillException(string message) : base(message)
    {
    }
}

class Program
{
    static void Main(string[] args)
    {
        try
        {
           
            Container container = new Container("KON-C-1", 200, 100, 50, 700); 

            
            double cargoMass = 600; 
            container.LoadCargo(cargoMass);
            Console.WriteLine($"Cargo loaded into container {container.SerialNumber}: {container.CargoMass} kg");

            
            container.EmptyCargo();
            Console.WriteLine($"Cargo emptied from container {container.SerialNumber}: {container.CargoMass} kg");

          
            LiquidContainer hazardousContainer = new LiquidContainer("KON-L-1", 200, 100, 50, 500, isHazardous: true);
            hazardousContainer.LoadCargo(250); 

           
            LiquidContainer nonHazardousContainer = new LiquidContainer("KON-L-2", 200, 100, 50, 500, isHazardous: false);
            nonHazardousContainer.LoadCargo(450);
           
            hazardousContainer.LoadCargo(400); 

          
            nonHazardousContainer.LoadCargo(550); 
        }
        catch (OverfillException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}