using Unity.VisualScripting;
using SimupactComs;

namespace ThreeDPactXR.SimuPact.UnityNode
{
    
    [UnitCategory("3D Pact/SimuPact/Get Data")]
    public class SimuPactGetFloat: Unit
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
        public ValueOutput PropertyValue;
        
        private float Result = float.NaN;
        
        [DoNotSerialize] // No need to serialize ports.
        public ValueInput Debug;
        
        protected override void Definition() //The method to set what our node will be doing.
        {

            input = ControlInput(nameof(input), (flow) =>
            {
                var serverData = flow.GetValue<SimuPactServer>(ServerData).server;

                Result = Simupactcommunication.GetValueOnceFloat( ref serverData, flow.GetValue<bool>(Debug),
                    flow.GetValue<SimuPactVariable>(PropertyID).PropertyID);
                
                
                return output;
            });
            output = ControlOutput("output");
            
            ServerData = ValueInput<SimuPactServer>("ServerData");
            PropertyID = ValueInput<SimuPactVariable>("PropertyID");
            PropertyValue = ValueOutput<float>("PropertyValue",(flow) => { return Result; });
            Debug = ValueInput<bool>("Debug", false);
        }
    }
}