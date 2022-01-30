using UnityEngine;

[CreateAssetMenu(fileName = "RocketShip", menuName = "ProjectSpecific/Buildings/RocketShip", order = 0)]
public class RocketShip : BuildingType {

    public override void OnInteract(Building parent) {
        parent.manager.WinGame();
    }
}