using Unity.VisualScripting;
using SimupactComs;

namespace ThreeDPactXR.SimuPact.UnityNode
{
    [UnitCategory("3D Pact/SimuPact/Split Data")]
    public class SpliteSnapData: Unit
    {
        
        [DoNotSerialize] // No need to serialize ports.
        public ValueInput SnapData;

        [DoNotSerialize] // No need to serialize ports.
        public ValueOutput SnapName;

        [DoNotSerialize] // No need to serialize ports.
        public ValueOutput SnapTimeCode;

        

        protected override void Definition() //The method to set what our node will be doing.
        {
            SnapData = ValueInput<SnapInformation>("SnapData");

            SnapName = ValueOutput<string>("Snap Name", (flow) => flow.GetValue<SnapInformation>(SnapData).SnapName);
            SnapTimeCode = ValueOutput<string>("Snap Time Code", (flow) => flow.GetValue<SnapInformation>(SnapData).SnapTimeCode);
            
        }
        
    }
}