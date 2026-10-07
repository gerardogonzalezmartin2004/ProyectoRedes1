using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using Unity.Services.Matchmaker.Models;
using UnityEngine;


[Serializable] // esto lo hacemos para enviar la informacion de los jugadores conectados a todos los clientes, para que cada cliente tenga la informacion de los jugadores conectados
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

    public event Action PlayerListOnChange;
    public List<NetworkUserGameData> conecctedUsers = new List<NetworkUserGameData>();
    private void Start()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += MethodCalledOnClientConnected;
        NetworkManager.Singleton.OnClientDisconnectCallback += MethodCalledOnClientDisconnected;
    }
    public override void  OnNetworkSpawn()
    {
        NewNetworkCLientConeccetRpc(localPlayerName, NetworkManager.Singleton.LocalClientId);
    }


    [Rpc(SendTo.Server, InvokePermission = RpcInvokePermission.Everyone)]
    private void NewNetworkCLientConeccetRpc(string playerName, ulong playerId)
    {
        NetworkUserGameData newConectedUser = new NetworkUserGameData();
        newConectedUser.networkPlayerId = playerId;
        newConectedUser.networkPlayerName = playerName;
        conecctedUsers.Add(newConectedUser);
        UpdateUserConnectedRPC(conecctedUsers.ToArray());
    }

    private void MethodCalledOnClientConnected(ulong playerId)
    {
        //if(NetworkManager.Singleton.IsHost )
        //{
        //    NetworkUserGameData newConectedUser= new NetworkUserGameData();
        //    newConectedUser.networkPlayerId = playerId;
        //    newConectedUser.networkPlayerName = "Player when solved ";
        //    conecctedUsers.Add(newConectedUser);
        //    UpdateUserConnectedRPC(conecctedUsers.ToArray());

        //}
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
            UpdateUserConnectedRPC(conecctedUsers.ToArray());

        }
    }
    [Rpc(SendTo.Everyone)]
    private void UpdateUserConnectedRPC(NetworkUserGameData[] userConnected )
    {


        conecctedUsers = userConnected.ToList();
        PlayerListOnChange?.Invoke();// el signo de interrogacion es para que si no hay nadie suscrito a ese evento no se rompa el juego, es como un if
    }
}
