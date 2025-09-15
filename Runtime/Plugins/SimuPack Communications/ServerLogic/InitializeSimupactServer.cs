using System.Collections.Generic;
using Unity.VisualScripting;
using SimupactComs;


namespace ThreeDPactXR.SimuPact.UnityNode
{
    
    [UnitCategory("3D Pact/SimuPact/Server Calls")]
    public class InitializeSimupactServer : Unit
    {

        [DoNotSerialize] // No need to serialize ports.
        public ControlInput input; //Adding the ControlInput port variable

        [DoNotSerialize] // No need to serialize ports.
        public ControlOutput output;//Adding the ControlOutput port variable.
        
        [DoNotSerialize] // No need to serialize ports.
        public ValueOutput ServerData;

        public ValueInput ServerDataProperties;
        
        
        
        private SimuPactServer Result=new SimuPactServer();


        [DoNotSerialize] // No need to serialize ports.
        public ValueInput Debug;
        
        protected override void Definition() //The method to set what our node will be doing.
        {

            input = ControlInput(nameof(input), (flow) =>
            {
                Result.ServerName = flow.GetValue<SimuPactServer>(ServerDataProperties).ServerName;
                Result.ServerIP = flow.GetValue<SimuPactServer>(ServerDataProperties).ServerIP;
                Result.ServerPort = flow.GetValue<SimuPactServer>(ServerDataProperties).ServerPort;
                
                
                Result.server = Simupactcommunication.InitializeCommunication(Result.ServerIP, Result.ServerPort, flow.GetValue<bool>(Debug));
                
                if (Result.server == null)
                {
                    Result.IsConnected = false;
                }
                else
                {
                    Result.IsConnected = true;
                }
                
                Result.ServerState = Simupactcommunication.SimupactState(ref Result.server, flow.GetValue<bool>(Debug));
                
                var SimupactSnapData = Simupactcommunication.GetSnapList(ref Result.server, flow.GetValue<bool>(Debug));
                Result.SnapInformationList = new List<SnapData>();
                foreach (var snap in SimupactSnapData)
                {
                    var snapInformation = new SnapData();
                    snapInformation.SnapName = snap.SnapName;
                    snapInformation.SnapTimeCode = snap.SnapTimeCode;
                    Result.SnapInformationList.Add(snapInformation);
                }
                
                
                
                return output;
            });
            output = ControlOutput("output");
            
            ServerData = ValueOutput<SimuPactServer>("ServerData", (flow) => { return Result; });

            
            
            ServerDataProperties = ValueInput<SimuPactServer>("ServerProperties",Result);
            
            Debug = ValueInput<bool>("Debug", false);
        }
    }
}