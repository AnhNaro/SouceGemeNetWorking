using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class SpawEnemy : MonoBehaviourPun
{
   public List<Transform> TranEne=new List<Transform>();
   public List<GameObject> enemy = new List<GameObject>();
    float Timespaw;
    float capdo=5;
    float tangCD;
    bool checkClient;
    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            tangCD += Time.deltaTime;
            if (tangCD > 30 )
            {
                capdo = Random.Range(1,5);
                tangCD = 0;
            }
            Timespaw += Time.deltaTime;
            if (Timespaw > capdo)
            {
                Spaw();
                Timespaw = 0;
            }
            checkClient = true;
        }
        if (!PhotonNetwork.IsMasterClient && checkClient)
        {
            PhotonNetwork.SetMasterClient(PhotonNetwork.MasterClient);
        }
    }
    public void Spaw()
    {
        GameObject ss=enemy[Random.Range(0,enemy.Count)];
        Transform aa = TranEne[Random.Range(0, TranEne.Count)];
        PhotonNetwork.Instantiate(ss.name, aa.position, Quaternion.identity);
    }
}
