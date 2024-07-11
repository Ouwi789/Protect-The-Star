using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBehaviour : MonoBehaviour
{
    public bool occupied;
    public bool isHydrogenType;
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
    public void setToHydrogen(bool hydrogen)
    {
        isHydrogenType = hydrogen;
    }
    public bool isHydrogen()
    {
        if(isHydrogenType)
        {
            return true;
        }
        return false;
    }
}
