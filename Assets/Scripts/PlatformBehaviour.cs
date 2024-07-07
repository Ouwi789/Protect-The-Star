using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBehaviour : MonoBehaviour
{
    public bool occupied;
    public GameObject occupiedBuilding;
    void Start()
    {
        occupied = false;
    }

    public bool isOccupied()
    {
        return occupied;
    }
    public void setOccupied(bool occupied)
    {
        this.occupied = occupied;
    }
    public GameObject getBuilding()
    {
        return occupiedBuilding;
    }
    public void setBuilding(GameObject building)
    {
        occupiedBuilding = building;
    }
}
