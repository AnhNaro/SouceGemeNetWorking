using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Voice.Unity;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;
public class GameManargerMin : MonoBehaviourPunCallbacks
{
    public Recorder OnOffMic;
    public Button OnMic;
    public Button OffMic;
    public Button btnthoat;
    public GameObject PanelgameLoss;
    public GameObject PanelgameWwin;
    public Text txtTimegame;
    [SerializeField] Button btnpnLoss;
    [SerializeField] Button btnpnwin;
    float time=360f;
    int count;
    PlayerCpntroller player;
    PhotonView view;
    public GameObject SoundMessenger;
    private void Awake()
    {
        OnMic.onClick.AddListener(() =>
        {
            OnOffMic.TransmitEnabled = false;
        });
        OffMic.onClick.AddListener(() =>
        {
            OnOffMic.TransmitEnabled = true;
        });
        btnthoat.onClick.AddListener(VaoSanh);
        btnpnLoss.onClick.AddListener(VaoSanh);
        btnpnwin.onClick.AddListener(VaoSanh);
    }
    private void Start()
    {
        view=GetComponent<PhotonView>();
    }
    private void Update()
    {
        player = FindObjectOfType<PlayerCpntroller>();
        time -= Time.deltaTime;
        count =(int)time;
        view.RPC("texttimeg", RpcTarget.AllViaServer,count);  
        if (player == null && count>0)
        {
            PanelgameLoss.gameObject.SetActive(true);
        }
        if(player != null && count<=0)
        {
            PanelgameWwin.gameObject.SetActive(true);
        }
    }
    [PunRPC]
    public void texttimeg(int time)
    {
        txtTimegame.text=time.ToString();
    }
    public void VaoSanh()
    {
        PhotonNetwork.LeaveRoom();
    }
    public override void OnLeftRoom()
    {
       Destroy(SoundMessenger.gameObject);
        SceneManager.LoadScene(1);
    }
}
