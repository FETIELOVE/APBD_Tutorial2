namespace Tutorial_2a;

public class GasContainer : Container, IHazardNotifier
{
   
    
        public double Pressure { get; private set; }
        public bool IsHazardous { get; private set; }  
        public GasContainer(int height, double tareWeight, int depth, double maximumPayload, double pressure, bool isHazardous)
            : base(height, tareWeight, depth, maximumPayload)
        {
            this.Pressure = pressure;
            this.IsHazardous = isHazardous;  
        }

        public override void LoadCargo(double mass)
        {
            if (IsHazardous && mass > MaximumPayload * 0.5)
            {
                NotifyHazard("Attempted to load hazardous cargo above 50% capacity.");
                throw new OverfillException("Gas container overload: hazardous cargo exceeds 50% of the allowable payload.");
            }
            else if (!IsHazardous && mass > MaximumPayload * 0.9)
            {
                NotifyHazard("Attempted to load non-hazardous cargo above 90% capacity.");
                throw new OverfillException("Gas container overload: non-hazardous cargo exceeds 90% of the allowable payload.");
            }

            CargoMass += mass;
        }
        
        public override void EmptyCargo()
        {
            CargoMass -= CargoMass * 0.05; 
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
