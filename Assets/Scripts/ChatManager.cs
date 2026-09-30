using System;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class ChatManager : MonoBehaviour
{
    [SerializeField] private TMP_Text chatLog;
    [SerializeField] private TMP_InputField chatMessageToSend;

    public void DisconectFromGame()
    {
        NetworkManager.Singleton.Shutdown();
    }
    public void SendChatMessage()
    {
        
        SendChatMessageWithRpc(chatMessageToSend.text, "playerName");
        chatMessageToSend.text = "";

    }

    [Rpc(SendTo.Everyone)]
    private void SendChatMessageWithRpc (string message, string playerName)
    {
        chatLog.text += playerName + ": " + message + "\n"; //retorno de carro, sirve como para hacer por parte los dos mensajes

    }
}
