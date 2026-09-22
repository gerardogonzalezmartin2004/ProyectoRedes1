using UnityEngine;
using Unity.Netcode;


public class MenuManager : MonoBehaviour
{
    public void StartLocalHost()
    {

        NetworkManager.Singleton.StartHost();
        
    }
    public void StartLocalClient()
    {
        NetworkManager.Singleton.StartClient();
    }
    public void DisconectFromNetcode ()
    {
        NetworkManager.Singleton.Shutdown();
    }
}
