using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camerafollow : MonoBehaviour
{
    Vector3 ori;
    Vector3 zz;
    public Vector2 Limitx;
    public Vector2 Limity;
    [Range(0, 10)]
    [SerializeField] float speed;   
     PlayerCpntroller aa;
    bool check=false;
    void Start()
    {
        aa = FindObjectOfType<PlayerCpntroller>();
        transform.position = new Vector3(aa.transform.position.x, aa.transform.position.y, transform.position.z);
        ori = transform.position - aa.transform.position;
    }
    private void FixedUpdate()
    {
        if (aa == null)
        {
            check = true;
        }
        if (check)
        {
            aa = FindObjectOfType<PlayerCpntroller>();
            transform.position = new Vector3(aa.transform.position.x, aa.transform.position.y, transform.position.z);
            ori = transform.position -  aa.transform.position;
            check= false;  
        }
            Vector3 post = aa.transform.position + ori;
            post.x = Mathf.Clamp(post.x, Limitx.x, Limitx.y);
            post.y = Mathf.Clamp(post.y, Limity.x, Limity.y);
            transform.position = Vector3.SmoothDamp(post, ori, ref zz, speed);
    }
}
