using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NodeTypeA : MonoBehaviour, IObject
{
    [SerializeField] private GameObject _nodeModel;
    [SerializeField] private TextMeshProUGUI _nodeName;
    [SerializeField] private GameObject nodeIcon;
   
    // Start is called before the first frame update

    void Awake(){
        nodeIcon.SetActive(false);
    }
    void Start() {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    // Activate is called when object is picked up
    public void PickupActivate() {
        nodeIcon.SetActive(true);
        Debug.Log("ENABLED");
    }

    // Deactivate called when object is 'thrown' away
    public void ReleaseDeactivate() {
        nodeIcon.SetActive(false);
        Debug.Log("DISABLED");
    }
}
