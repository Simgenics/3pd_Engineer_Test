using Unity.VisualScripting;
using SimupactComs;
using ControlSystemConnection.GrpcGenerated;

namespace ThreeDPactXR.SimuPact.UnityNode
{
    
    [UnitCategory("3D Pact/SimuPact/Set Data")]
    public class SimuPactSetInt: Unit
    {
    
        [DoNotSerialize] // No need to serialize ports.
        public ControlInput input; //Adding the ControlInput port variable

        [DoNotSerialize] // No need to serialize ports.
        public ControlOutput output;//Adding the ControlOutput port variable.
        
        [DoNotSerialize] // No need to serialize ports.
        public ValueInput ServerData;
        
        [DoNotSerialize] // No need to serialize ports.
        public ValueInput PropertyID;
        
        [DoNotSerialize] // No need to serialize ports.
        public ValueInput SetPropertyValue;
        
        [DoNotSerialize] // No need to serialize ports.
        public ValueInput Debug;
        
        protected override void Definition() //The method to set what our node will be doing.
        {

            input = ControlInput(nameof(input), (flow) =>
            {
                Server.ServerClient serverData = flow.GetValue<SimuPactServer>(ServerData).server;

                Simupactcommunication.SetValueOnce(ref serverData, flow.GetValue<bool>(Debug),
                    flow.GetValue<SimuPactVariable>(PropertyID).PropertyID,flow.GetValue<int>(SetPropertyValue));
                
                return output;
            });
            output = ControlOutput("output");
            
            ServerData = ValueInput<SimuPactServer>("ServerData");
            PropertyID = ValueInput<SimuPactVariable>("PropertyID");
            SetPropertyValue = ValueInput<int>("SetPropertyValue", 0);
            Debug = ValueInput<bool>("Debug", false);
        }
    }
}