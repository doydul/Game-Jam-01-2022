using UnityEngine;

// [CreateAssetMenu(fileName = "BuildingType", menuName = "ProjectSpecific/BuildingType", order = 0)]
public class BuildingType : ScriptableObject {
    
    public virtual void Init(Building parent) {}
    public virtual void OnUpdate(Building parent) {}
    public virtual void OnInteract(Building parent) {}
}