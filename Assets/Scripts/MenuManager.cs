using UnityEngine;
using Unity.Netcode;
using TMPro;
using Unity.Netcode.Transports.UTP;



public class MenuManager : MonoBehaviour
{

    public TMP_InputField inputlocalPlayerName;
    public TMP_InputField inputLocalAddress;
    public TMP_InputField inputLocalPortAdress;

    private void Start()
    {
        string[] ramdomNames = { "Player1", "Player2", "Player3", "Player4", "Player5" };
        inputlocalPlayerName.text = ramdomNames[Random.Range(0, ramdomNames.Length-1)];
    }
    public void StartLocalHost()
    {
        if(!string.IsNullOrEmpty(inputLocalAddress.text))
        {
            SetConnectionData();
            GameNetworkData.Singleton.localPlayerName = inputlocalPlayerName.text;
            NetworkManager.Singleton.StartHost();
        }
        
    }
    public void StartLocalClient()
    {
        if (!string.IsNullOrEmpty(inputLocalAddress.text))
        {
            SetConnectionData();
            GameNetworkData.Singleton.localPlayerName = inputlocalPlayerName.text;
            NetworkManager.Singleton.StartClient();

        }
    }

    private void SetConnectionData()
    {
        UnityTransport transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
        transport.ConnectionData.Address = inputLocalAddress.text;
        if(ushort.TryParse(inputLocalPortAdress.text, out ushort portValue))
        {
            transport.ConnectionData.Port = portValue;
        }


    }
    public void DisconectFromNetcode ()
    {
        NetworkManager.Singleton.Shutdown();
    }
}
