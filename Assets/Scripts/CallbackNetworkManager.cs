using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CallbackNetworkManager : MonoBehaviour
{
    //para poder trabajar en linea necesitamos el NetworkManager, es el principal en cual entorno con netcode. Es muy importante elegir la capa de transporte detro de netocde. es ismplente elegilo y te genero el unity transport
    private static CallbackNetworkManager singleton;


    public static CallbackNetworkManager Singleton => singleton; // Singleton instance of the NetworkEventsManager ocn un get


    public void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    private void Start()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += MethodCalledOnClientConnected;
        NetworkManager.Singleton.OnClientDisconnectCallback += MethodCalledOnClientDisconnected;
    }

    private void MethodCalledOnClientConnected(ulong playerId)
    {
        if (NetworkManager.Singleton.IsHost && playerId == NetworkManager.Singleton.LocalClientId)
        {
            NetworkManager.Singleton.SceneManager.LoadScene("Chat", LoadSceneMode.Single); //utlizaos esa para q se sincronize         
        }
    }
    private void MethodCalledOnClientDisconnected(ulong playerId)
    {
        if (playerId == NetworkManager.Singleton.LocalClientId)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

   
}
