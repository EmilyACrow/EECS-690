using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : Singleton<PathManager>
{
    [SerializeField] private List<Waypoint> waypoints;
    // Start is called before the first frame update
    void Start() {
        var foundWaypoints = FindObjectsOfType<Waypoint>();
        for (int i = 0; i < foundWaypoints.Length; i++) {
            waypoints.Add(foundWaypoints[i]);
        }
    }

    public Waypoint getNextWaypoint(Waypoint current) {
        List<Waypoint> wps = getWaypointsByZone(current.zone);
        int index = wps.IndexOf(current);
        int newIndex = index;
        if (wps.Count == 1) { return current; }
        while (newIndex == index) {
            newIndex = Random.Range(0,wps.Count - 1);
        }
        return wps[newIndex];
    }

    private List<Waypoint> getWaypointsByZone(int zone) {
        List<Waypoint> zoneWaypoints = new List<Waypoint>();
        foreach (Waypoint wp in waypoints) {
            if (wp.zone == zone) {
                zoneWaypoints.Add(wp);
            }
        }
        return zoneWaypoints;
    }
}
