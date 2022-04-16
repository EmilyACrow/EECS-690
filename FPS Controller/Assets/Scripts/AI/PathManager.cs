using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : Singleton<PathManager>
{
    [SerializeField] private List<Waypoint> waypoints;
    // Start is called before the first frame update
    public Waypoint getNextWaypoint(Waypoint current) {
        return current.getNextWaypoint();
    }
}
