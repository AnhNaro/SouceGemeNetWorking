using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class SpawPlayer : MonoBehaviour
{
    public List<GameObject> Playerprefabs=new List<GameObject>();
    public List<Transform> Spawns = new List<Transform>();
    GameObject Clone;
    void Start()
    {
        int Apoit=Random.Range(0,Spawns.Count);
        Clone = Playerprefabs[(int)PhotonNetwork.LocalPlayer.CustomProperties["Ss"]];
        PhotonNetwork.Instantiate(Clone.name, Spawns[Apoit].position, Quaternion.identity);
    }
  
}
