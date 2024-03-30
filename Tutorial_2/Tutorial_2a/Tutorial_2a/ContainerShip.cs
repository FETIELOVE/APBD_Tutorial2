namespace Tutorial_2a;

public class ContainerShip
{
    
    public List<Container> Containers { get; private set; } = new List<Container>();
    public double MaxSpeed { get;   set; }
    public int MaxContainerAmount { get;  set; }
    public double MaxWeight { get;  set; }

    public ContainerShip(double maxSpeed, int maxContainerAmount, double maxWeight)
    {
        this.MaxSpeed = maxSpeed;
        this.MaxContainerAmount = maxContainerAmount;
        this.MaxWeight = maxWeight;
    }

    public void LoadContainer(Container container)
    {
        if (this.Containers.Count >= MaxContainerAmount)
            throw new InvalidOperationException(
                "Ship reached its maximum capacity, it cannot load anymore containers.");

        if (GetTotalWeightOfContainers() + container.CargoMass + container.TareWeight > MaxWeight * 1000) 
            throw new InvalidOperationException("Loading this container would exceed the ship's weigth capacity.");
        
        this.Containers.Add(container);
    }

    private double GetTotalWeightOfContainers()
    {
        return Containers.Sum(container => container.CargoMass + container.TareWeight);
    }

    public void UnloadContainer(Container container)
    {
        if (!this.Containers.Remove(container))
            throw new InvalidOperationException("Container not found.");

        Console.WriteLine($"Container {container.SerialNumber} unloaded from the ship.");
    }

    public void ReplaceContainer(Container oldContainer, Container newContainer)
    {
        int i = Containers.IndexOf(oldContainer);

        if (i == -1) throw new InvalidOperationException("Container to be replaced not found.");

        Containers[i] = newContainer;
    }

    public void TransferContainer(Container container, ContainerShip targetShip)
    {
        if (!this.Containers.Remove(container))
            throw new InvalidOperationException("Container not found.");

        Console.WriteLine($"Container {container.SerialNumber} unloaded from the current ship.");

        targetShip.LoadContainer(container);
        Console.WriteLine($"Container {container.SerialNumber} loaded to new target ship.");
    }
    public void LoadListOfContainers(IEnumerable<Container> containers)
    {
        foreach (var container in containers)
        {
            LoadContainer(container);
        }
    }

    public void PrintContainerInfo(string serialNumber)
    {
        var container = Containers.FirstOrDefault(c => c.SerialNumber == serialNumber);

        if (container == null)
            throw new InvalidOperationException("Container not found.");
        
        Console.WriteLine(container);
    }

    public void PrintShipInformation()
    {
        Console.WriteLine($"Ship information: Maximum Speed = {this.MaxSpeed} knots, Maximum Containers " +
                          $"= {this.MaxContainerAmount}, Maximum Weight = {this.MaxWeight} tons.");
        Console.WriteLine("Cargo:");
        foreach (var container in Containers)
            Console.WriteLine($"- {container.SerialNumber}");
    }
}
