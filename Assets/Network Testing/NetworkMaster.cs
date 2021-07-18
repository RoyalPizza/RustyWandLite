using MLAPI;
using MLAPI.Transports.UNET;
using QFSW.QC;
using UnityEngine;

public class NetworkMaster : MonoBehaviour
{
    [Command]
    public void StartAsServer()
    {
        NetworkManager.Singleton.StartServer();
        NetworkManager.Singleton.OnServerStarted += Singleton_OnServerStarted;

        Debug.Log("Server Start Command Set");
    }

    [Command]
    public void StartAsClient(string route = "127.0.0.1")
    {
        try
        {
            NetworkManager.Singleton.GetComponent<UNetTransport>().ConnectAddress = route;
            NetworkManager.Singleton.StartClient();
            NetworkManager.Singleton.OnClientConnectedCallback += Singleton_OnClientConnectedCallback;
            NetworkManager.Singleton.OnClientDisconnectCallback += Singleton_OnClientDisconnectCallback;
            Debug.Log("Client Connect Command Set");
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
    public void Shutdown()
    {
        if (NetworkManager.Singleton.IsClient)
        {
            NetworkManager.Singleton.StopClient();
            NetworkManager.Singleton.OnClientConnectedCallback -= Singleton_OnClientConnectedCallback;
            NetworkManager.Singleton.OnClientDisconnectCallback -= Singleton_OnClientDisconnectCallback;
        }
        else if (NetworkManager.Singleton.IsServer)
        {
            NetworkManager.Singleton.StopServer();
            NetworkManager.Singleton.OnServerStarted -= Singleton_OnServerStarted;
        }

        Application.Quit();
    }

    private void Singleton_OnServerStarted()
    {
        Debug.Log("Server Started");
    }

    private void Singleton_OnClientConnectedCallback(ulong obj)
    {
        Debug.Log($"Client Connected {obj}");
    }

    private void Singleton_OnClientDisconnectCallback(ulong obj)
    {
        Debug.Log($"Client Disconnected {obj}");
    }
}
