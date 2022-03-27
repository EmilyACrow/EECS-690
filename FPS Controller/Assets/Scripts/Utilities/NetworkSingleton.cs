//Taken from Dilmer Valecillo's Singleton class

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NetworkSingleton<T> : NetworkBehaviour
    where T : Component
{

    private static T _instance;
    public static T Instance {
        get {
            if(_instance == null) {
                var objs = FindObjectsOfType(typeof(T)) as T[];
                if (objs.Length > 0) {
                    _instance = objs[0];
                }
                if (objs.Length > 1) {
                    Debug.LogError("More than one instance of " + typeof(T).Name + " in the scene");
                }
                if (_instance == null) {
                    GameObject obj = new GameObject();
                    obj.name = string.Format("_{0}", typeof(T).Name);
                    _instance = obj.AddComponent<T>();
                }
            }
            return _instance;
        }
    }
}
