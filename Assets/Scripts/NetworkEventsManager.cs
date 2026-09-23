using Unity.Services.Multiplayer;
using UnityEngine;
using Unity.Netcode;
using System;
using UnityEngine.SceneManagement;

public class NetworkEventsManager : MonoBehaviour
{
    private static NetworkEventsManager singleton;
   

    public static NetworkEventsManager Singleton => singleton; // Singleton instance of the NetworkEventsManager ocn un get


    public void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (singleton != this)
        {
            Destroy(this.gameObject);
        }
    }
    private void Start()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += MethodCalledOnClientConnected;
        NetworkManager.Singleton.OnClientDisconnectCallback += MethodCalledOnClientDisconnected;
    }

    private void MethodCalledOnClientDisconnected(ulong playerId)
    {
       if(playerId == NetworkManager.Singleton.LocalClientId)
        {NetworkManager.Singleton.SceneManager.LoadScene("Chat", LoadSceneMode.Single); //utlizaos esa para q se sincronize
        }
    }

    private void MethodCalledOnClientConnected(ulong playerId)
    {
        if (playerId == NetworkManager.Singleton.LocalClientId)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}

 