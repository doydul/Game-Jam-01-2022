using UnityEngine;

[CreateAssetMenu(fileName = "BasicShelter", menuName = "ProjectSpecific/Buildings/BasicShelter", order = 0)]
public class BasicShelter : BuildingType {

    public override void Init(Building parent) {
        var manager = GameObject.FindObjectOfType<GameManager>();
        manager.nightDamageAmount = 0;
    }
}