using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectAnimation : MonoBehaviour
{
    [SerializeField] Transform target;
    [Header("Transition Parameters")]
    [SerializeField] private float frontAngleThreshold = 60.0f;
    [SerializeField] private float sideAngleThreshold = 120.0f;


    [Header("Views")]
    [SerializeField] private Material frontView;
    [SerializeField] private Material leftView;
    [SerializeField] private Material rightView;
    [SerializeField] private Material backView;

    [SerializeField] private float aimOffset = 90.0f;

    public float relativeAngle;

    private MeshRenderer renderer;
    private float aimDirection;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        aimDirection = transform.localEulerAngles.y - transform.parent.transform.localEulerAngles.x - aimOffset;
        //Match aimDirection to inspector values for convenience
        aimDirection = aimDirection > 180.0f ? aimDirection - 360 : aimDirection;
        if(Mathf.Abs(aimDirection) <= frontAngleThreshold) { // Target is in front of the owner
            renderer.material = frontView;
        } else if (Mathf.Abs(aimDirection) <= sideAngleThreshold) {
            if(aimDirection < 0) { // Target is to the left of the owner
                renderer.material = leftView;
            } else { // Target is to the right of the owner
                renderer.material = rightView;
            }
        } else { // Target is behind the owner
            renderer.material = backView;
        }
    }
}
