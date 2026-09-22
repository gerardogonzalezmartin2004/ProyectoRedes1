using Unity.Netcode;
using UnityEngine;

public class TestingSimpleRpcs : NetworkBehaviour
{
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
