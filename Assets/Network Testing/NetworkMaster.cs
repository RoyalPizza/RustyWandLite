using MLAPI;
using MLAPI.Transports.UNET;
using QFSW.QC;
using UnityEngine;

public class NetworkMaster : MonoBehaviour
{
    [Command]
    public void StartAsServer(string route = "127.0.0.1")
    {
        NetworkManager.Singleton.GetComponent<UNetTransport>().ConnectAddress = route;
        NetworkManager.Singleton.StartServer();

        Debug.Log("Server Connected");
    }

    [Command]
    public void StartAsClient(string route = "127.0.0.1")
    {
        try
        {
            NetworkManager.Singleton.GetComponent<UNetTransport>().ConnectAddress = route;
            NetworkManager.Singleton.StartClient();
            Debug.Log("Client Connected");
        }
        catch (System.Exception err)
        {
            Debug.LogError(err);
        }
    }

    [Command]
    public void ClientStatus()
    {
        Debug.Log(NetworkManager.Singleton.IsConnectedClient);
    }

    [Command]
    public void DisconnectAsClient()
    {
        NetworkManager.Singleton.StopClient();
        Application.Quit();
    }
}
