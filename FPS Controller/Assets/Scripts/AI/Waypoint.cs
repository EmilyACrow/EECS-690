using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private Waypoint nextWaypoint;
    [SerializeField] private Waypoint prevWaypoint;
    // Start is called before the first frame update
    public Waypoint getPrevWaypoint() {
        return prevWaypoint;
    }

    public Waypoint getNextWaypoint() {
        return nextWaypoint;
    }


}
