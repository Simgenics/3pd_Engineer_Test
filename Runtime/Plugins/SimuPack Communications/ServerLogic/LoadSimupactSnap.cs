using Unity.VisualScripting;
using SimupactComs;

namespace ThreeDPactXR.SimuPact.UnityNode
{
    [UnitCategory("3D Pact/SimuPact/Server Calls")]
    public class LoadSimupactSnap: Unit
    {
        
        [DoNotSerialize] // No need to serialize ports.
        public ControlInput input; //Adding the ControlInput port variable

        [DoNotSerialize] // No need to serialize ports.
        public ControlOutput output;//Adding the ControlOutput port variable.
        
        [DoNotSerialize] // No need to serialize ports.
        public ValueInput ServerData;

        [DoNotSerialize] // No need to serialize ports.
        public ValueInput SnapName;

        [DoNotSerialize] // No need to serialize ports.
        public ValueInput Debug;
        
        protected override void Definition() //The method to set what our node will be doing.
        {

            input = ControlInput(nameof(input), (flow) =>
            {
                var serverData = flow.GetValue<SimuPactServer>(ServerData).server;

                Simupactcommunication.LoadSnap(ref serverData, flow.GetValue<bool>(Debug),
                    flow.GetValue<string>(SnapName));
                
                return output;
            });
            output = ControlOutput("output");
            
            ServerData = ValueInput<SimuPactServer>("ServerData");
            SnapName = ValueInput<string>("SnapName", "");
            Debug = ValueInput<bool>("Debug", false);
        }
    }
}