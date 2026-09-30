using Unity.Netcode;
using UnityEngine;

public class TestingSimpleRpcs : NetworkBehaviour
{
    // Usar Rpcs y NetworkVariables 
    //1. el script tiene q heredar de NetworkBehaviour
    //2. el gameobject tiene q tener un NetworkObject
    //3.En el caso del RPC tenmso que poner un header que indique es un RPC y a quien se envia: Rpc(SendTo.Everyone) o Rpc(SendTo.Owner) o Rpc(SendTo.Server)
    //4. el nimbre del metodo tiene que acabar  en RPC


    //Rpc es un remote Procedure Call, que permite ejecutar  metodos en otros equipos de la partida
    public void TestingRpcAll()
    {
      toAllRPC();
    }

    [Rpc(SendTo.Everyone)]
    private void toAllRPC()
    {
        Debug.Log("RPC to all");
    }
}
