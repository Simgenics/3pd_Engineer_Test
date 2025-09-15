using Unity.VisualScripting;
using SimupactComs;

namespace ThreeDPactXR.SimuPact.UnityNode
{
    
    [UnitCategory("3D Pact/SimuPact/Server Calls")]
    public class GetSimupactSimulationTime: Unit
    {
        
        [DoNotSerialize] // No need to serialize ports.
        public ControlInput input; //Adding the ControlInput port variable

        [DoNotSerialize] // No need to serialize ports.
        public ControlOutput output;//Adding the ControlOutput port variable.
        
        [DoNotSerialize] // No need to serialize ports.
        public ValueInput ServerData;
        
        
        [DoNotSerialize] // No need to serialize ports.
        public ValueOutput ServerSimulationTime;

        [DoNotSerialize] // No need to serialize ports.
        public ValueInput Debug;
        
        private double Result=0;
        protected override void Definition() //The method to set what our node will be doing.
        {

            input = ControlInput(nameof(input), (flow) =>
            {
                var serverData = flow.GetValue<SimuPactServer>(ServerData).server;

                Result = Simupactcommunication.GetSimupactSimulationTime(ref serverData,
                    flow.GetValue<bool>(Debug));
                
                return output;
            });
            output = ControlOutput("output");
            
            ServerData = ValueInput<SimuPactServer>("ServerData");
            ServerSimulationTime = ValueOutput<double>("ServerSimulationTime",(flow) => { return Result; });
            Debug = ValueInput<bool>("Debug", false);
        }
    }
}