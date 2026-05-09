using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;


public class weapon : MonoBehaviour
{
    public static weapon instance;
    public List<bool> WeaponIndex = new List<bool> {false,false};
    public int CheakAttackId;
    public int CheakFormerId;
    public float AttackSpeed;
    public float shakeHandSpeed;//手的挥动速度
    public GameObject ParticleTrans;
    public GameObject swordAttack;
    public GameObject swordPartical;
    public bool StartAttack;
    //1为剑 以上是剑所需要用到的参数
    public Ray GunRay;
    public Animator GunAnimator;
    private Vector3 dir;
    public InputDevice RightCotroller;
    public Transform StartPos;
    public Transform EndPos;
    public float GunInterval;
    public Transform BulletPrefab;
    public Transform BulletCrash;
    public float bulletAmount;
    public float MaxBullet;
    public bool IsHaveBullet;
    public AudioSource mainSource;
    public AudioClip shootSound;//开火音效
    public AudioClip silencerShoot;//消音器
    public AudioClip reloadShoot;//换子弹
    public AudioClip reloadSoundAmmo;//换子弹拉枪栓
    public AudioClip AimSound;//瞄准音效
    public GameObject CheakWay;
    public bool AimState;//当前的开镜状态
    public Text BulletText;
    public Gun Gunanim;
    public Gun gunAnim 
    {
        set
        {
            Gunanim = value;
            switch (Gunanim)
            {
                case Gun.Fire:
                    GunAnimator.Play("fire", -1, 0);
                    GunAnimator.SetBool("fire", true);
                    break;
                case Gun.Reload:
                    GunAnimator.SetBool("reload", true);
                    break;
                case Gun.Idle:
                    GunAnimator.SetBool("fire", false);
                    GunAnimator.SetBool("reload", false);
                    GunAnimator.SetBool("walk", false);
                    break;
                case Gun.Walk:
                    GunAnimator.SetBool("walk", true);
                    break;
                case Gun.Aim:
                    GunAnimator.SetBool("aim", true);
                    break;
                case Gun.AimOut:
                    GunAnimator.SetBool("aim", false);
                    break;
            }
        }
    }
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (WeaponIndex[1] == true)
        {
            GunRay = new Ray(GameObject.Find("[Right Controller] Ray Origin").transform.position,dir);
            dir = EndPos.position - StartPos.position;
            mainSource = GetComponent<AudioSource>();
            bulletAmount = MaxBullet;
            GunAnimator = GetComponent<Animator>();
            AimState = false;
            //IsHaveGun = true;
        }
        RightCotroller = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (WeaponIndex[1] == true)
        {
            if (GunInterval < 0.11f)//这是枪的计时
            {
                GunInterval += Time.fixedDeltaTime;
            }
            if (GunInterval >= 0.11f)
            {
                GunInterval =0.11f;
            }
        }
    }
    void Update()
    {
        //Debug.Log(Vector3.Distance(CheakWay.transform.position, transform.position) + "qiang");
        if (WeaponIndex[1] == true)
        {
            gunAnim = Gun.Idle;
            BulletText.text = "子弹数量" + bulletAmount;
        }
       
        if (1.3f-AttackSpeed>0.1f)
        {
            AttackSpeed += Time.deltaTime;
        }
        else
        {
            AttackSpeed = 1.3f;
        }
        if (StartAttack)
        {
            shakeHandSpeed += Time.deltaTime;
            if (shakeHandSpeed > 0.5f)
            {
                shakeHandSpeed = 0;
                StartAttack = false;
            }
        }
        if (WeaponIndex[1] == true&&GunInterval==0.11f&&Input.GetMouseButton(0)&&!GunAnimator.GetCurrentAnimatorStateInfo(0).IsName("takeOut"))//后面加触发
        {
            GunInterval = 0;
            CheackRay();
        }
        if (bulletAmount <= 0)
        {
            
        }
        
    }
    public void CheackRay()//这是开枪的逻辑
    {
        bulletAmount -= 1;
        if (bulletAmount <= 0)
        {
            return;
        }
        GunRay.direction = EndPos.position - StartPos.position;
        GunRay.origin = StartPos.position;
        mainSource.clip = shootSound;//添加音效
        mainSource.Play();
        gunAnim = Gun.Fire;//改变动画
        RaycastHit hit;
        if(Physics.Raycast(GunRay,out hit))
        {
            if (hit.transform.CompareTag("enemy"))
            {
                hit.transform.GetComponent<monster>().AddOrDecreaceHP(1);
            }
            if (hit.transform.CompareTag("pick"))
            {
                Debug.Log("wa");
                
            }
        }
        Instantiate(BulletCrash, StartPos);//蛋壳掉落特效
        Transform bullet;
        bullet=Instantiate(BulletPrefab, StartPos);//子弹前进特效
        bullet.GetComponent<Rigidbody>().velocity = transform.forward * 200;
    }
   
    private void OnTriggerEnter(Collider other)//这是检测剑攻击的
    {
        
        if (other.gameObject.CompareTag("check"))
        {
            if (WeaponIndex[0] == true)//如果是剑的下标
            {
               
                CheakFormerId = CheakAttackId;
                CheakAttackId = other.GetComponent<item>().HandID;
                if (Mathf.Abs(CheakFormerId - CheakAttackId) == 1&&StartAttack ==true)
                {
                    SwordAttack();
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)//检测剑的攻击
    {
        if (other.gameObject.CompareTag("check"))
        {
            shakeHandSpeed = 0;
            StartAttack = true;
        }
    }
    public void SwordAttack()
    {
        if (AttackSpeed == 1.3f)
        {
            Debug.Log("sheng");
            AttackSpeed = 0;
            swordAttack =  Instantiate(swordPartical);
            swordAttack.transform.position= ParticleTrans.transform.position;
            swordAttack.transform.rotation= Quaternion.LookRotation(Player.instance.Ciname.transform.forward);
        }
    }
    public enum Gun
    {
        Fire,
        Reload,
        Idle,
        Walk,
        Aim,
        AimOut,
    }

    //public enum Weapom
    //{
    //    sword,
    //    pi,
    //}
    //public Weapom Weapon;
    //public Weapom weapontype 
    //{
    //    set
    //    {
    //        Weapon = value;
    //        switch (Weapon)
    //        {
    //            case Weapom.sword:
    //                break;
    //            case Weapom.pi:
    //                break;

    //        }
    //    }
    //}
    //public bool IsHaveGun;
    //以上是枪所用的参数
    //[System.Serializable]
    //public class SoundClips
    //{
    //    public AudioClip shootSound;//开火音效
    //    public AudioClip audio;
    //}

}
