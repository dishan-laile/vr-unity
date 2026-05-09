using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exam : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.GetComponent<partical>().particalID == 1)//如果是一号特效攻击的话
        {
            Debug.Log("1");
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        Debug.Log(collision.gameObject);
    }
}
