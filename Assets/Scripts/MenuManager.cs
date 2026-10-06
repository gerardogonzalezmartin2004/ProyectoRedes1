using UnityEngine;
using Unity.Netcode;
using TMPro;


public class MenuManager : MonoBehaviour
{

    public TMP_InputField inputlocalPlayerName;
    public TMP_InputField inputLocalAddress;
    public TMP_InputField inputLocalPortAdress;
    public void StartLocalHost()
    {
        GameNetworkData.Singleton.localPlayerName = inputlocalPlayerName.text;
        NetworkManager.Singleton.StartHost();
        
    }
    public void StartLocalClient()
    {
        GameNetworkData.Singleton.localPlayerName = inputlocalPlayerName.text;
        NetworkManager.Singleton.StartClient();
    }
    public void DisconectFromNetcode ()
    {
        NetworkManager.Singleton.Shutdown();
    }
}
