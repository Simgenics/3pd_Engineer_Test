using Unity.VisualScripting;
using SimupactComs;

namespace ThreeDPactXR.SimuPact.UnityNode
{
    
    [UnitCategory("3D Pact/SimuPact/Server Calls")]
    public class GetSimupactServerState: Unit
    {

        [DoNotSerialize] // No need to serialize ports.
        public ControlInput input; //Adding the ControlInput port variable

        [DoNotSerialize] // No need to serialize ports.
        public ControlOutput output;//Adding the ControlOutput port variable.
        
        [DoNotSerialize] // No need to serialize ports.
        public ValueInput ServerData;
        
        [DoNotSerialize] // No need to serialize ports.
        public ValueOutput ServerState;
        
        private Simupactcommunication.SimupactStateEnum Result=Simupactcommunication.SimupactStateEnum.UNKNOWN;
        
        [DoNotSerialize] // No need to serialize ports.
        public ValueInput Debug;
        protected override void Definition() //The method to set what our node will be doing.
        {

            input = ControlInput(nameof(input), (flow) =>
            {
                var serverData = flow.GetValue<SimuPactServer>(ServerData).server;

                Result = Simupactcommunication.SimupactState(ref serverData,
                    flow.GetValue<bool>(Debug));
                
                return output;
            });
            output = ControlOutput("output");
            
            ServerData = ValueInput<SimuPactServer>("ServerData");
            ServerState = ValueOutput<Simupactcommunication.SimupactStateEnum>("ServerState",(flow) => { return Result; });
            Debug = ValueInput<bool>("Debug", false);
        }
    }
}