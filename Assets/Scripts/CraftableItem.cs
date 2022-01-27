using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "CraftableItem", menuName = "ProjectSpecific/CraftableItem", order = 0)]
public class CraftableItem : ScriptableObject {
    
    public int woodRequirement;
    public int stoneRequirement;
    public int limit = 999;
    public List<CraftableItem> dependencies;
    public Sprite sprite;
    public BuildingType behaviour;
    public float scale = 1;
    [TextArea] public string description; 
}