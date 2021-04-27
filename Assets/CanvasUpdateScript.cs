using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CanvasUpdateScript : MonoBehaviour
{
    public Text MaxAmmotext, CurrentAmmotext;
    public int MaxAmmo,  ChipSize,CurrentAmmo;
    public Image PrimoryWeaponWeapon;
    public GameObject PrimoryWeaponWeaponPanel;
    public Playerinputs objplayerinut;
     [HideInInspector]
    public GameObject Primoryweapon, SecondoryWeapon, MeleeWeapon;
    public PLayerScript playerscript;
    public Camera fpsCam;



    public bool shooting,reloading,readyToShoot;
     public RaycastHit rayHit;
    public LayerMask whatIsEnemy;
  public GameObject  bulletHoleGraphic;
   public Transform attackPoint;
  public ParticleSystem muzzleFlash;


    public int magazineSize, bulletsPerTap,bulletsLeft, bulletsShot;
       
    public float timeBetweenShooting,spread,range,reloadTime,timeBetweenShots;
    
    void Start()
    {

        PrimoryWeaponWeaponPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
      if (objplayerinut.Pick_Weapon_in_handbool)
      {
        if (Input.GetMouseButton(0))                                            // pick weapon to hand
        {
          
         playerscript.isvalid=false;
         playerscript.LeftShipressed=false;

Invoke("Fire", timeBetweenShots);
         
        }
        if (Input.GetMouseButtonUp(0))                                     // pick weapon to hand
        {        
            playerscript.isvalid=true;
             playerscript.LeftShipressed=Input.GetKey(KeyCode.LeftShift);
        }


      if (Input.GetKeyDown(KeyCode.R)&&CurrentAmmo<ChipSize&&!reloading)  
        {
              reloading = true;
             Invoke("Reload", reloadTime); 

        }
     
                     



       
      }




   
            
        


      
    }











    public void updatePrimaryWeaponImage(GameObject Weapon)
    {

        PrimoryWeaponWeaponPanel.SetActive(true);
        Primoryweapon = Weapon;
        PrimoryWeaponWeapon.sprite=Primoryweapon.GetComponent<GUN_INFO>().Gunimage;
        MaxAmmo = Primoryweapon.GetComponent<GUN_INFO>().MaxAmmo;
        ChipSize = Primoryweapon.GetComponent<GUN_INFO>().Chipsize;
        CurrentAmmo= Primoryweapon.GetComponent<GUN_INFO>().CurrentAmmo;
        bulletsPerTap=Primoryweapon.GetComponent<GUN_INFO>().bulletsPerTap;
           reloadTime=Primoryweapon.GetComponent<GUN_INFO>().reloadTime;
        timeBetweenShooting=Primoryweapon.GetComponent<GUN_INFO>().timeBetweenShooting;
        timeBetweenShots=Primoryweapon.GetComponent<GUN_INFO>().timeBetweenShots;
         muzzleFlash=Primoryweapon.GetComponent<GUN_INFO>().muzzleFlash;
         bulletHoleGraphic=Primoryweapon.GetComponent<GUN_INFO>().bulletHoleGraphic;
         attackPoint=Primoryweapon.GetComponent<GUN_INFO>().attackPoint;
           spread=Primoryweapon.GetComponent<GUN_INFO>().spread;
  
        readyToShoot=true;

        UpdateCanvasUI();
    }
    public void UpdateCanvasUI()
    {
        MaxAmmotext.text=MaxAmmo.ToString();
        CurrentAmmotext.text= CurrentAmmo.ToString();
        UpdateGunScriptValue();

    }
    public void UpdateGunScriptValue()
    {
        Primoryweapon.GetComponent<GUN_INFO>().MaxAmmo= MaxAmmo;
        Primoryweapon.GetComponent<GUN_INFO>().CurrentAmmo=CurrentAmmo;
    }

   public void Fire()
    {
        if (!reloading&&readyToShoot)
        {

            
          float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);
       





        readyToShoot = false;
        Invoke("ResetShot", timeBetweenShooting);
        if(CurrentAmmo>0)
        {
            CurrentAmmo=CurrentAmmo-bulletsPerTap;
        if (Physics.Raycast(attackPoint.transform.position, transform.TransformDirection(Vector3.forward)+ new Vector3(x, y, 0), out rayHit, Mathf.Infinity, whatIsEnemy))
        {
            Debug.DrawRay(attackPoint.transform.position, transform.TransformDirection(Vector3.forward) * rayHit.distance, Color.yellow);
            Debug.Log("Did Hit");
              Instantiate(bulletHoleGraphic, rayHit.point, Quaternion.Euler(0, 180, 0));
        }   
        
            muzzleFlash.Play();
              Invoke("Fire", timeBetweenShots);
               UpdateCanvasUI();
           
        }
        else
        {
              reloading = true;
               Invoke("Reload", reloadTime); 
         

        }
    } 
    }   
   
        
    private void ResetShot()
    {
        readyToShoot = true;
    }






     public void Reload()
    {
       
     
            Debug.Log("reload");
            if (MaxAmmo!=0)
            {
    
                int requedammo=ChipSize-CurrentAmmo;
                if (requedammo<MaxAmmo)
                {
                MaxAmmo-=requedammo;
                CurrentAmmo+=requedammo;
                }else
                {
                      CurrentAmmo+=MaxAmmo;
                    MaxAmmo-=MaxAmmo;
                  
                }


            }else
            {
                Debug.Log("Ammo out of Stoke");
            }
               reloading = false;
         UpdateCanvasUI();
        
    } 



}
