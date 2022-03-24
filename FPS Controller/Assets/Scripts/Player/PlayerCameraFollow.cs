using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class PlayerCameraFollow : NetworkSingleton<PlayerCameraFollow>
{
    // private CinemachineFreeLook camera;
    private CinemachineVirtualCamera virtualCamera;
    private Transform player = null; 
    private Camera camera;
    private AudioListener audioListener;

    // Start is called before the first frame update
    void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        camera = GetComponent<Camera>();
        audioListener = GetComponent<AudioListener>();
    }
    
    // Called every frame
    // private void Update() {
    //     if(player != null) {
    //         camera.m_YAxis.Value = player.eularAngles.y;
    //     }
    // }

    public void FollowPlayer(Transform transform) {
        player = transform;
        virtualCamera.Follow = transform;
        virtualCamera.LookAt = transform;
        camera.enabled = true;
        audioListener.enabled = true;
        
    }
}
