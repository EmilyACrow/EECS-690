using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class UIManager : MonoBehaviour
{

    [SerializeField] private Button startServerBtn;
    [SerializeField] private Button startHostBtn;
    [SerializeField] private Button startClientBtn;
    [SerializeField] private TextMeshProUGUI playerInGameText;

    // Awake is called before the first frame update
    void Awake()
    {
        Cursor.visible = true;
    }

    private void Start() {
        startServerBtn.onClick.AddListener(() => {
            if (NetworkManager.Singleton.StartServer()) {
                Debug.Log("Server started...");
            } else {
                Debug.Log("Server could not start");
            }
        });

        startHostBtn.onClick.AddListener(() => {
            if (NetworkManager.Singleton.StartHost()) {
                Debug.Log("Host started...");
            } else {
                Debug.Log("Host could not start");
            }
        });

        startClientBtn.onClick.AddListener(() => {
            if (NetworkManager.Singleton.StartClient()) {
                Debug.Log("Client started...");
            } else {
                Debug.Log("Client could not start");
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        playerInGameText.text = $"Players in game: {PlayersManager.Instance.PlayersInLobby}";
    }
}
