using MLAPI;
using MLAPI.Logging;
using MLAPI.Transports.UNET;
using QFSW.QC;
using UnityEngine;

public class NetworkMaster : MonoBehaviour
{
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
        else if (NetworkManager.Singleton.IsHost)
        {
            Debug.Log("Shutown Host Sent");
            NetworkManager.Singleton.StopServer();
            NetworkManager.Singleton.OnClientConnectedCallback -= Singleton_OnClientConnectedCallback;
            NetworkManager.Singleton.OnClientDisconnectCallback -= Singleton_OnClientDisconnectCallback;
            NetworkManager.Singleton.OnServerStarted -= Singleton_OnServerStarted;
        }

        Application.Quit();
    }

    [Command]
    public void StartAsHost()
    {
        Debug.Log("Start Host Command Sent");
        NetworkManager.Singleton.OnServerStarted += Singleton_OnServerStarted;
        NetworkManager.Singleton.OnClientConnectedCallback += Singleton_OnClientConnectedCallback;
        NetworkManager.Singleton.OnClientDisconnectCallback += Singleton_OnClientDisconnectCallback;
        NetworkManager.Singleton.StartHost();
    }

    [Command]
    public void StartAsClient(string route = "127.0.0.1")
    {
        Debug.Log("Client Connect Command Sent");
        NetworkManager.Singleton.GetComponent<UNetTransport>().ConnectAddress = route;
        NetworkManager.Singleton.OnClientConnectedCallback += Singleton_OnClientConnectedCallback;
        NetworkManager.Singleton.OnClientDisconnectCallback += Singleton_OnClientDisconnectCallback;
        NetworkManager.Singleton.StartClient();
    }

    [Command]
    public void StartAsServer()
    {
        Debug.Log("Server Start Command Sent");
        NetworkManager.Singleton.OnServerStarted += Singleton_OnServerStarted;
        NetworkManager.Singleton.StartServer();
    }

    #region Events

    private void Singleton_OnServerStarted()
    {
        Debug.Log("Server Started");
    }

    private void Singleton_OnClientConnectedCallback(ulong obj)
    {
        Debug.Log($"Client Connected {obj}");
        NetworkLog.LogInfoServer($"Client Connected {obj}");
    }

    private void Singleton_OnClientDisconnectCallback(ulong obj)
    {
        Debug.Log($"Client Disconnected {obj}");
        NetworkLog.LogInfoServer($"Client Disconnected {obj}");
    }

    #endregion
}
