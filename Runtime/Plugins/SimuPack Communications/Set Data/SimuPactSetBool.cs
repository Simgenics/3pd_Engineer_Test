using Unity.VisualScripting;
using SimupactComs;

namespace ThreeDPactXR.SimuPact.UnityNode
{
    
    [UnitCategory("3D Pact/SimuPact/Set Data")]
    public class SimuPactSetBool: Unit
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
                var serverData = flow.GetValue<SimuPactServer>(ServerData).server;

                Simupactcommunication.SetValueOnce(ref serverData, flow.GetValue<bool>(Debug),
                    flow.GetValue<SimuPactVariable>(PropertyID).PropertyID,flow.GetValue<bool>(SetPropertyValue));
                
                return output;
            });
            output = ControlOutput("output");
            
            ServerData = ValueInput<SimuPactServer>("ServerData");
            PropertyID = ValueInput<SimuPactVariable>("PropertyID");
            SetPropertyValue = ValueInput<bool>("SetPropertyValue", false);
            Debug = ValueInput<bool>("Debug", false);
        }
    }
}