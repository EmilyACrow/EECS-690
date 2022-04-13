using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NodeTypeA : MonoBehaviour, IObject
{
    [SerializeField] public GameObject _nodeModel;
    [SerializeField] public GameObject _nodeName;
    [SerializeField] public GameObject _nodeIcon;
   
    // Start is called before the first frame update
    public GameObject getModel(){
        return _nodeModel;
    }
    void Awake(){
    }
    void Start() {
        _nodeIcon.SetActive(false);
        _nodeName.SetActive(false);
        _nodeModel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
    }

    // Activate is called when object is picked up
    public void PickupActivate() {
        _nodeIcon.SetActive(true);
        Debug.Log("ENABLED");
        _nodeModel.SetActive(false);
    }

    // Deactivate called when object is 'thrown' away
    public void ReleaseDeactivate() {
        _nodeIcon.SetActive(false);
        Debug.Log("DISABLED");
        _nodeModel.SetActive(true);
        _nodeName.SetActive(false);
    }
}
