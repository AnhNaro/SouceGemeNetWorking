using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;
using Photon.Realtime;

public class ConnectServer : MonoBehaviourPunCallbacks
{
    [SerializeField] InputField Nameplayer;
    [SerializeField] Text connect;
    public GameObject rawimageVideo;
    private void Start()
    {
        PhotonNetwork.GameVersion = "0.1";
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 5;
    }
    private void Update()
    {
        if (Time.time > 5)
        {
            rawimageVideo.SetActive(false);
        }
    }
    public void On_clickConnect()
    {
        if (Nameplayer.text.Length >= 1)
        {
            PhotonNetwork.NickName=Nameplayer.text;
            connect.text = "Connecting...";
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.ConnectUsingSettings();
        }
    }
    public override void OnConnectedToMaster()
    {
        SceneManager.LoadScene(1);
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        connect.text = "Kiem tra mang va thu lai";
    }
}
