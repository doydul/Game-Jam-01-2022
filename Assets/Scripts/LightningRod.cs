using UnityEngine;

[CreateAssetMenu(fileName = "LightningRod", menuName = "ProjectSpecific/Buildings/LightningRod", order = 0)]
public class LightningRod : BuildingType {

    public override void Init(Building parent) {
        GameObject.FindObjectOfType<LightningStorm>().lightningRod = parent.transform;
    }
}