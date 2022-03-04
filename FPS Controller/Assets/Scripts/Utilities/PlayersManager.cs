using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayersManager : NetworkSingleton<PlayersManager>
{
    private NetworkVariable<int> playersInLobby = new NetworkVariable<int>();

    public int PlayersInLobby {
        get {
            return playersInLobby.Value;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += (id) => {
            if(IsServer) {
                Debug.Log($"Player {id} connected...");
                playersInLobby.Value++;
            }
        };

        NetworkManager.Singleton.OnClientDisconnectCallback += (id) => {
            if(IsServer) {
                Debug.Log($"Player {id} disconnected...");
                playersInLobby.Value--;
            }
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
