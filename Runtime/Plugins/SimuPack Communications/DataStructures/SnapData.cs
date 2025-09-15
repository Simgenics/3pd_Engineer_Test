using Unity.VisualScripting;
using UnityEngine.Scripting;

namespace ThreeDPactXR.SimuPact.UnityNode
{
    [Preserve]
    [IncludeInSettings(true)]
    [Inspectable]
    public class SnapData
    {
        [Serialize]
        [Inspectable]
        public string SnapName { get; set; }
        
        [Serialize]
        [Inspectable]
        public string SnapTimeCode { get; set; }
        
    }
}