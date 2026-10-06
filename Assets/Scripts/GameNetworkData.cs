using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;


[Serializable]
public class NetworkUserGameData: INetworkSerializable
{
    public string networkPlayerName;
    public ulong networkPlayerId;
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref networkPlayerId);
        serializer.SerializeValue(ref networkPlayerName);
    }
}
public class GameNetworkData : NetworkBehaviour
{
    private static GameNetworkData singleton;
     

    public static GameNetworkData Singleton => singleton; // Singleton instance of the NetworkEventsManager ocn un get


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
    public string localPlayerName;

    private List<NetworkUserGameData> conecctedUsers = new List<NetworkUserGameData>();
    private void Start()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += MethodCalledOnClientConnected;
        NetworkManager.Singleton.OnClientDisconnectCallback += MethodCalledOnClientDisconnected;
    }

    private void MethodCalledOnClientConnected(ulong playerId)
    {
        if(NetworkManager.Singleton.IsHost )
        {
            NetworkUserGameData newConectedUser= new NetworkUserGameData();
            newConectedUser.networkPlayerId = playerId;
            newConectedUser.networkPlayerName = "Player when solved ";
            conecctedUsers.Add(newConectedUser);
                   
        }
    }
    private void MethodCalledOnClientDisconnected(ulong playerId)
    {
        if (NetworkManager.Singleton.IsHost)
        {

            for (int i = conecctedUsers.Count - 1; i >= 0; i--)
            {
                if (conecctedUsers[i].networkPlayerId == playerId)
                {
                    conecctedUsers.RemoveAt(i);               
                }
            }

        }
    }
    [Rpc(SendTo.Everyone)]
    private void UpdateUserConnectedRPC(NetworkUserGameData[] userConnected )
    {
        conecctedUsers = userConnected.ToList();
    }
}
