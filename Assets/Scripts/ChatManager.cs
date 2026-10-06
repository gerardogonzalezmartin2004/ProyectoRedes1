using System;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class ChatManager : NetworkBehaviour
{
    // Usar Rpcs y NetworkVariables 
    //1. el script tiene q heredar de NetworkBehaviour
    //2. el gameobject tiene q tener un NetworkObject
    //3.En el caso del RPC tenmso que poner un header que indique es un RPC y a quien se envia: Rpc(SendTo.Everyone) o Rpc(SendTo.Owner) o Rpc(SendTo.Server)
    //4. el nimbre del metodo tiene que acabar  en RPC

    [SerializeField] private TMP_Text chatLog;
    [SerializeField] private TMP_InputField chatMessageToSend;
    [SerializeField] private TMP_Text userConnected;

   
    public void DisconectFromGame()
    {
        NetworkManager.Singleton.Shutdown();
    }
    
    public void SendChatMessage()
    {
        
        SendChatMessageWithRpc(chatMessageToSend.text, GameNetworkData.Singleton.localPlayerName);
        chatMessageToSend.text = "";

    }

    [Rpc(SendTo.Everyone)]
    private void SendChatMessageWithRpc (string message, string playerName)
    {
        chatLog.text += playerName + ": " + message + "\n"; //retorno de carro, sirve como para hacer por parte los dos mensajes

    }
    
 
   
}
