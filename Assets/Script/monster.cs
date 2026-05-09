using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class monster : MonoBehaviour
{
    //1为绿头小兵
    public GameObject Player;
    private NavMeshAgent agent;
    private Animator animator;
    public float MaxHP=3;
    public float HP;
        //以上为公用参数
    public static monster instance;
    private bool isHit=false;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        HP = MaxHP;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();    
    }

    // Update is called once per frame
    void Update()
    {
        //if (!isHit)
        //{
        //    if (Vector3.Distance(Player.transform.position, agent.transform.position) > 8f)
        //    {
        //        agent.isStopped = false;
        //        animator.SetBool("walk", true);
        //        animator.SetBool("attack", false);
        //        //agent.SetDestination(Player.transform.position);
        //    }
        //    else
        //    {
        //        animator.SetBool("attack", true);
        //        animator.SetBool("walk", true);
        //        //agent.isStopped = true;
        //    }
        //}
           
    }
    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.GetComponent<partical>().particalID == 1)//如果是一号特效攻击的话
        {
            AddOrDecreaceHP(1);
        }
    }
    public void AddOrDecreaceHP(int Valua)
    {
        HP-= Valua;
        if (HP <= 0)
        {
            isHit = true;
            //agent.isStopped = true;
            HP = 0;
            animator.CrossFade("death", 0);
            Invoke("death", 1.8f);
        }
        if (HP > 0)
        {
            animator.SetBool("hit", true);
            animator.SetBool("walk", false);
            animator.SetBool("attack", false);
            animator.CrossFade("hit", 0);
            //agent.isStopped = true;
            Invoke("Hit", 0.5f);
            isHit = true;
        }
    }
    public void Hit()
    {
        animator.SetBool("hit", false);
        //agent.isStopped = false;
        isHit = false;
    }
    public void death()
    {
        Destroy(gameObject);
    }
}
