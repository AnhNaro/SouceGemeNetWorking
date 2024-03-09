using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerSprite : MonoBehaviourPunCallbacks
{
    [SerializeField] Text nameplayersprite;
    public List<Sprite> sprites=new List<Sprite>();
    ExitGames.Client.Photon.Hashtable chancharacter = new ExitGames.Client.Photon.Hashtable();
    public Image image;
    Player player;
    public void SetImage(int indexsprite)
    {
        chancharacter["Ss"]= indexsprite;
        PhotonNetwork.SetPlayerCustomProperties(chancharacter);
    }
    public void Setname(Player pp)
    {
        nameplayersprite.text = pp.NickName;
        player = pp;
        updateplayeritem(player);
    }
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if (player == targetPlayer)
        {
            updateplayeritem(targetPlayer);
        }

    }

    private void updateplayeritem(Player player)
    {
        if (player.CustomProperties.ContainsKey("Ss"))
        {
            image.sprite = sprites[(int)player.CustomProperties["Ss"]];
            chancharacter["Ss"] = (int)player.CustomProperties["Ss"];
        }
        else
        {
            chancharacter["Ss"] = 6;
            player.CustomProperties["Ss"] = chancharacter["Ss"];
            image.sprite =sprites[(int)player.CustomProperties["Ss"]];
        }
    }
}
