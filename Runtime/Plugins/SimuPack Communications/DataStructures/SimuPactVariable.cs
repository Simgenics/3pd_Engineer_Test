using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Scripting;

namespace ThreeDPactXR.SimuPact.UnityNode
{
    [Preserve]
    [IncludeInSettings(true)]
    [Inspectable]
    public struct SimuPactVariable
    {
        
        [Serialize]
        [Inspectable]
        public string PropertyName;
        [Serialize]
        [Inspectable]
        public string PropertyID;
        
        [Serialize]
        [Inspectable]
        public GameObject PropertyGameObject;
        
        [Serialize]
        [Inspectable]
        public VarType DataType;
        
        
        public enum VarType
        {
            [Serialize]
            Double,
            [Serialize]
            String,
            [Serialize]
            Int,
            [Serialize]
            Bool,
            [Serialize]
            Float
        }
    }
    
    
    
}