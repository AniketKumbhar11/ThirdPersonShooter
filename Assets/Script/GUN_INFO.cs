using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUN_INFO : MonoBehaviour
{

    //give the tag of gameobject
    // attached Rigidbody and box collider
    //Gravity is untick
    //on is trigger on 
    //fill the info of script
    //Give rotation is 0 0 0 and scale is 1 1 1






    // Start is called before the first frame update
        public enum GunClassname
    {
        Rifle,Sniper,Mele,Mini

    }
    public enum GunPriority
    {
        Primary,Secondory,Third,None,Melee,
    }
    public GunPriority gunpriority;
    public GunClassname gunclassname;

    public Sprite Gunimage;
    public string Gunname;
    [Range(1, 100)]
    public int Damage;
   public int Chipsize,MaxAmmo,CurrentAmmo;



     public int bulletsPerTap;
       
    public float timeBetweenShooting,spread,range,reloadTime,timeBetweenShots;
    public bool allowButtonHold;


    //Reference
   
    public Transform attackPoint;
   


    //Graphics
    public GameObject bulletHoleGraphic;
   
   public ParticleSystem muzzleFlash;
    public float camShakeMagnitude, camShakeDuration;




    void Start()
    {
        //Debug.Log(gunclassname);
        gunpriority=GunPriority.None;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   

}
