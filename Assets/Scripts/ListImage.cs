using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class ListImage : MonoBehaviourPunCallbacks
{
    public List<Sprite> imageplayer = new List<Sprite>();
    public List<Button> buttonplayer=new List<Button>();
    GameObject aa;
    public void Onclicj_Setspriteplayer(int index)
    {
        aa=FindObjectOfType<PlayerSprite>().gameObject;
        if(aa!=null)
        {
            aa.TryGetComponent(out PlayerSprite ss);
            ss.SetImage(index);
        }
       
    }
    private void Start()
    {
        for(int i = 0; i < imageplayer.Count; i++)
        {
            buttonplayer[i].GetComponent<Image>().sprite= imageplayer[i];
        }
    }
}
