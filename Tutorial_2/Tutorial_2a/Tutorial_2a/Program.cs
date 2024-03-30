namespace Tutorial_2a;


public class Program
{
    private static ContainerShip _containerShip = new ContainerShip(30, 5, 300000);

    public static void Main(string[] args)
    {
        ContainerShip ship1 = new ContainerShip(20, 5, 100000); 
        ContainerShip ship2 = new ContainerShip(25, 10, 200000); 

        var refrigeratedContainer = CreateRefrigeratedContainer("Fish", 2);
        var gasContainer = CreateGasContainer(10.5, false);

        refrigeratedContainer.LoadCargo(5000);
        gasContainer.LoadCargo(8500);

        ship1.LoadContainer(refrigeratedContainer);
        ship1.LoadContainer(gasContainer);

        List<Container> containers = new List<Container>
        {
            CreateGasContainer(8, true),
            CreateRefrigeratedContainer("Bananas", 13.3)
        };
        containers.ForEach(ship2.LoadContainer);

        ship1.UnloadContainer(gasContainer);

        refrigeratedContainer.EmptyCargo();

        var newRefrigeratedContainer = CreateRefrigeratedContainer("Ice cream", -18);
        ship1.ReplaceContainer(refrigeratedContainer, newRefrigeratedContainer);

        ship1.TransferContainer(newRefrigeratedContainer, ship2);

        ship2.PrintContainerInfo(newRefrigeratedContainer.SerialNumber);

        ship1.PrintShipInformation();
        ship2.PrintShipInformation();
    }

    private static RefrigeratedContainer CreateRefrigeratedContainer(string productType, double temperature)
    {
        return new RefrigeratedContainer(250, 1000, 200, 20000, productType, temperature);
    }

    private static GasContainer CreateGasContainer(double pressure, bool isHazardous)
    {
        return new GasContainer(300, 1500, 250, 15000,  pressure, isHazardous);
    }
}