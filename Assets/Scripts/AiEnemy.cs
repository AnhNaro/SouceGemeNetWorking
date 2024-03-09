using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class AiEnemy : MonoBehaviourPun
{
    Vector2 origin;
    [SerializeField] float Speedmove;
    PlayerCpntroller[] player=new PlayerCpntroller[] {};
    int dame = 1;
    public AudioSource audi;
    int i=0;
    float time;
    private void Start()
    {
        origin = transform.position;
        player = FindObjectsOfType<PlayerCpntroller>();
        i = Random.Range(0, player.Length);
    }
    void Update()
    {
          player = FindObjectsOfType<PlayerCpntroller>();
        if (base.photonView.IsMine)
        {
            time += Time.deltaTime;
            if (player != null&&time>30)
            {
                i = Random.Range(0,player.Length);
                time = 0;
            }
            transform.position = Vector2.MoveTowards(transform.position, player[i].transform.position, Speedmove * Time.deltaTime);
            if (player[i] == null)
            {
                i = Random.Range(0, player.Length);
            }
            if (player == null)
            {
                transform.position=new Vector3 (0,0,0); 
            }
            if (transform.position.x > origin.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                origin.x = transform.position.x;
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
                origin.x = transform.position.x;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bom"))
        {
            PhotonNetwork.Destroy(gameObject);
        }
        if (collision.CompareTag("Player"))
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }
    public int TakeDame() => dame;
}
