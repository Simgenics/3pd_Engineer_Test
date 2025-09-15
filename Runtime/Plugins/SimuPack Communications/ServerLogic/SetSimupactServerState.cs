using Unity.VisualScripting;
using SimupactComs;
using ControlSystemConnection.GrpcGenerated;

namespace ThreeDPactXR.SimuPact.UnityNode
{
    
    [UnitCategory("3D Pact/SimuPact/Server Calls")]
    public class SetSimupactServerState: Unit
    {

        [DoNotSerialize] // No need to serialize ports.
        public ControlInput input; //Adding the ControlInput port variable

        [DoNotSerialize] // No need to serialize ports.
        public ControlOutput output;//Adding the ControlOutput port variable.
        
        [DoNotSerialize] // No need to serialize ports.
        public ValueInput ServerData;

        [DoNotSerialize] // No need to serialize ports.
        public ValueInput ServerState;

        [DoNotSerialize] // No need to serialize ports.
        public ValueInput Debug;
        
        protected override void Definition() //The method to set what our node will be doing.
        {
            
            input = ControlInput(nameof(input), (flow) =>
            {
                Server.ServerClient serverData = flow.GetValue<SimuPactServer>(ServerData).server;

                switch (flow.GetValue<Simupactcommunication.SimupactStateEnum>(ServerState))
                {
                    case Simupactcommunication.SimupactStateEnum.UNKNOWN:
                    
                        break;
                    case Simupactcommunication.SimupactStateEnum.ACTIVATING:
                        Simupactcommunication.ActivateSimupact(ref serverData, flow.GetValue<bool>(Debug));
                        break;
                    case Simupactcommunication.SimupactStateEnum.ACTIVE:
                        Simupactcommunication.ActivateSimupact(ref serverData, flow.GetValue<bool>(Debug));
                        break;
                    case Simupactcommunication.SimupactStateEnum.DEACTIVATING:
                        Simupactcommunication.DeactivateSimupact(ref serverData, flow.GetValue<bool>(Debug));
                        break;
                    case Simupactcommunication.SimupactStateEnum.INACTIVE:
                        Simupactcommunication.DeactivateSimupact(ref serverData, flow.GetValue<bool>(Debug));
                        break;
                    case Simupactcommunication.SimupactStateEnum.PAUSED:
                        Simupactcommunication.PauseSimupact(ref serverData, flow.GetValue<bool>(Debug));
                        break;
                    case Simupactcommunication.SimupactStateEnum.RUNNING:
                        Simupactcommunication.RunSimupact(ref serverData, flow.GetValue<bool>(Debug));
                        break;
                    case Simupactcommunication.SimupactStateEnum.SOLVINGSTEADYSTATE:
                        
                        break;
                    
                }
                
                
                return output;
            });
            output = ControlOutput("output");
            
            ServerData = ValueInput<SimuPactServer>("ServerData");
            ServerState = ValueInput<Simupactcommunication.SimupactStateEnum>("ServerState", Simupactcommunication.SimupactStateEnum.UNKNOWN);
            Debug = ValueInput<bool>("Debug", false);
        }
    }
}