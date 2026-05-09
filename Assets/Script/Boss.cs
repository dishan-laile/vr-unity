using System;
using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Animator Animator;
    public static Boss instance;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
        Animator.SetBool("roar", true);
        Invoke("BossOneAttack", 2.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void BossOneAttack()
    {
        Animator.SetBool("roar", false);
        Animator.SetBool("attack", true);
        Animator.SetBool("fly", true);
        Invoke("SceneShake", 1.5f);
        Invoke("MoveMontain",1.4f);
    }
    public void SceneShake()
    {
        SceneManage.Instance.isShake=true;
    }
    public void MoveMontain()
    {
        SceneManage.Instance.MoveWay();
    }
}
