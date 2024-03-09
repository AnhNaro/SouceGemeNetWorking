using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerCpntroller : MonoBehaviourPunCallbacks
{
    [SerializeField] float Speed;
    Animator animplay;
    float a;
    float b;
    public GameObject Bom;
    public Transform Poitspawbom;
    public Text NameNhanVat;
    [SerializeField] Text Hpplayer;
    [SerializeField] Text timeactivebtnbom;
    float timeActive=3;
    [SerializeField] GameObject ButtonBom;
    [SerializeField] GameObject CanvasInterface;
    Rigidbody2D rd;
    int HP;
    private void Start()
    {
        HP = 10;
        animplay = GetComponent<Animator>();
        NameNhanVat.text= GetComponent<PhotonView>().Controller.NickName;
        rd=GetComponent<Rigidbody2D>(); 
    }
    private void FixedUpdate()
    {
        if (!base.photonView.IsMine) return;
        rd.velocity=new Vector2(a, b) * Speed * Time.deltaTime;
    }
    private void Update()
    {
        if (!base.photonView.IsMine) return;
        if (HP <= 0)
        {
            PhotonNetwork.Destroy(gameObject);
        }
        Hpplayer.text = HP.ToString();
        if (a > 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        if(a<0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
            if (!ButtonBom.activeInHierarchy)
            {
                timeActive -= Time.deltaTime;
                timeactivebtnbom.text = timeActive.ToString();
            }
            if (timeActive <= 0)
            {
                ButtonBom.SetActive(true);
                timeActive = 3;
            }
        if (base.photonView.IsMine)
        {
            CanvasInterface.SetActive(true);
        }
        if (!PhotonNetwork.AutomaticallySyncScene)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }
    public void Onclick_Moveleft()
    {
        animplay.SetFloat("Move", 1);
        a = -1;
    }
    public void Onclick_Moveright()
    {
        animplay.SetFloat("Move", 1);
        a = 1;
    }
    public void Onclick_Moveup()
    {
        animplay.SetFloat("Move", 3);
        b = 1f;
    }
    public void Onclick_MoveDown()
    {
        animplay.SetFloat("Move", 2);
        b = -1f;
    }
    public void Exitbutton()
    {
        animplay.SetFloat("Move",0);
        a = 0;
        b = 0;
    }
    public void Onclick_SpawBom()
    {
        GameObject aa = PhotonNetwork.Instantiate(Bom.name, Poitspawbom.position, Quaternion.identity);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.TryGetComponent(out AiEnemy ai);
           base.photonView.RPC("GetDame", RpcTarget.AllBuffered, ai.TakeDame());
        }
    }
  [PunRPC]
    public void GetDame(int dam)
    {
        HP -= dam;
    }
}
