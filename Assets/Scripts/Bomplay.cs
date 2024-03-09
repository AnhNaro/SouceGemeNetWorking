using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Bomplay : MonoBehaviourPunCallbacks
{

    [SerializeField] GameObject Fxbom;
    float time;
    // Update is called once per frame
    void Update()
    {
        if (!base.photonView.IsMine) return;
        time += Time.deltaTime;
        if (time > 3)
        {
            GameObject cl=PhotonNetwork.Instantiate(Fxbom.name,transform.position, Quaternion.identity);    
            time = 0;
            base.photonView.RPC("falsefxbom", RpcTarget.AllViaServer);
        }
    }
    [PunRPC]
    public void falsefxbom()
    {
        Destroy(this.gameObject);
    }
}
