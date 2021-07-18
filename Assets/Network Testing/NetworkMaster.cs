using MLAPI;
using MLAPI.Transports.UNET;
using QFSW.QC;
using UnityEngine;

public class NetworkMaster : MonoBehaviour
{
    [Command]
    public void StartAsServer()
    {
        Debug.Log("Server Start Command Sent");
        NetworkManager.Singleton.OnServerStarted += Singleton_OnServerStarted;
        NetworkManager.Singleton.StartServer();
    }

    [Command]
    public void StartAsClient(string route = "127.0.0.1")
    {
        try
        {
            Debug.Log("Client Connect Command Sent");
            NetworkManager.Singleton.GetComponent<UNetTransport>().ConnectAddress = route;
            NetworkManager.Singleton.OnClientConnectedCallback += Singleton_OnClientConnectedCallback;
            NetworkManager.Singleton.OnClientDisconnectCallback += Singleton_OnClientDisconnectCallback;
            NetworkManager.Singleton.StartClient();
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
            Debug.Log("Shutown Client Sent");
            NetworkManager.Singleton.StopClient();
            NetworkManager.Singleton.OnClientConnectedCallback -= Singleton_OnClientConnectedCallback;
            NetworkManager.Singleton.OnClientDisconnectCallback -= Singleton_OnClientDisconnectCallback;
        }
        else if (NetworkManager.Singleton.IsServer)
        {
            Debug.Log("Shutown Server Sent");
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
