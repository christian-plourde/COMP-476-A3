using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

//this represents a location where a tank can spawn. They are created from set positions where they can spawn and
//have a field saying if it has been taken or not
public class SpawnLocation
{
    private Vector3 position; //the spawn position
    private bool taken; //indicates if someone already spawned here

    public SpawnLocation(Vector3 position)
    {
        this.position = position;
        taken = false;
    }

    //this will return a vec3 that contains a location in a list of possible spawn positions that has not been already assigned
    //to a tank
    public static Vector3 GetFreeLocation(List<SpawnLocation> locations)
    {
        int i = 0;

        //first check if all spots are taken, if so throw an exception
        bool allTaken = true;

        foreach(SpawnLocation l in locations)
        {
            if (!l.taken)
            {
                allTaken = false;
                break;
            }
        }

        if (allTaken)
            throw new Exception("No free locations for spawn");

        //if there is a spot, find it and assign it, returning its position
        do
        {
            i = UnityEngine.Random.Range(0, locations.Count);
            if (locations[i].taken)
                continue;
            else
            {
                locations[i].taken = true;
                return locations[i].position;
            }
        }
        while (true);
    }
}
