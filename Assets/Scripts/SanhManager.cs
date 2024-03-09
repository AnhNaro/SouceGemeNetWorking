using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public class SanhManager : MonoBehaviourPunCallbacks
{
    [SerializeField] InputField nameRoom;
    [SerializeField] InputField nameJoinRoom;
    [SerializeField] Text textinfo;
    [SerializeField] GameObject PanelRoom;
    [SerializeField] Text TextnotInternet;
    bool checkinternet=true;
    List<PlayerSprite>player11 = new List<PlayerSprite>();
    public PlayerSprite playerPrefabs;
    public Transform conten;
    [SerializeField] Text Room;
    [SerializeField] GameObject btnPlayGame;
    RoomOptions roomvalue=new RoomOptions() { BroadcastPropsChangeToAll=true};
    [SerializeField] Button AddPlayer;
    [SerializeField] Button LessPlayer;
    public Text txtplayerValue;
    private void Awake()
    {
        roomvalue.MaxPlayers = 3;
        AddPlayer.onClick.AddListener(() =>
        {
            roomvalue.MaxPlayers++;
        });
        LessPlayer.onClick.AddListener(() =>
        {
            roomvalue.MaxPlayers--;
        });
    }
    private void Start()
    {
        PhotonNetwork.JoinLobby();
    }
    public void OnClick_CreatRoom()
    {
        if(nameRoom.text.Length>=1)
            PhotonNetwork.CreateRoom(nameRoom.text, roomvalue);
        else
        {
            textinfo.text = "Vui Long Nhap Ky Tu Khac";
        }
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        textinfo.text = "Phòng Đã Tồn Tại";
    }
    public override void OnJoinedRoom()
    {
        PanelRoom.SetActive(true);
        Room.text = PhotonNetwork.CurrentRoom.Name;
        UpDateplayer();
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        nameJoinRoom.text = "Phòng Đã Đủ Người";
    }
    public void Onclick_Joinroom()
    {
        if(nameJoinRoom.text.Length>=1)
        PhotonNetwork.JoinRoom(nameJoinRoom.text);
    }
    public void Onclick_PlayGame()
    {
       PhotonNetwork.LoadLevel(2);
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        TextnotInternet.text = "Not Internet Vui Long Ket Noi Mang";
        checkinternet= false;
    }
    public override void OnConnectedToMaster()
    {
        checkinternet= true;
        TextnotInternet.text = "";
        PhotonNetwork.JoinLobby();
    }
    private void Update()
    {
        txtplayerValue.text = string.Format($"Số Lượng Người Chơi Hiện Tại: {roomvalue.MaxPlayers}");
        if (roomvalue.MaxPlayers <= 0)
        {
            roomvalue.MaxPlayers = 1;
        }
        if (!checkinternet)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount >=1)
        {
            btnPlayGame.SetActive(true);
        }
        else
        {
            btnPlayGame.SetActive(false);   
        }
    }
    public void OnclickJoinroomWorld(string name)
    {
        PhotonNetwork.JoinRoom(name);
    }
    public void Onclick_LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
    public override void OnLeftRoom()
    {
        PanelRoom.SetActive(false);
    }
   void UpDateplayer()
    {
        foreach(PlayerSprite pl in player11)
        {
            Destroy(pl.gameObject);
        }
        player11.Clear();
        if (PhotonNetwork.CurrentRoom == null)
        {
            return;
        }
        foreach(KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            PlayerSprite aj = Instantiate(playerPrefabs, conten.position, Quaternion.identity,conten);
            aj.Setname(player.Value);
            player11.Add(aj);
        }

    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpDateplayer();
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpDateplayer();
    }
}
