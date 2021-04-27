using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Playerinputs : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator playeranim;
    private bool grabweapon;
    GameObject Gunobj;
    public Transform gunattachedright,gunattachedleft,gunholder,gundroppoint,MeleeHolder,Meleeattacher;
    public bool gunattachedrightbool,gunattachedleftbool,gunholderbool,MeleeAttachedbool;
    public GameObject PrimaryWeapon,SecondoryWeapon,MeleeWeapon;
 
    public bool Chnagepriority,Pick_Weapon_in_handbool,Pick_mele_in_handbool,hand_is_empty;
     [HideInInspector]
   public GameObject PweaponsImages,SweaponsImages,TweaponsImages,MweaponsImages;

   public CanvasUpdateScript objCanvasUpdateScript;
   public GunHolderScript ObjGunHolderScript;



    void Start()
    {
        playeranim=GetComponent<Animator>();
        DesableUI();
        PweaponsImages.SetActive(false);
        SweaponsImages.SetActive(false);
        TweaponsImages.SetActive(false);
        MweaponsImages.SetActive(false);
        
   
    }

    // Update is called once per frame
    void Update()
    {
         
        if (Input.GetKeyDown(KeyCode.E))                                            // pick weapon to hand
        {
            // Changging_Gun_Priority();
            Pick_Weapon_in_hand();
         
        }  

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Changging_Gun_Priority();                                       // Drop and Switch weapons
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
           Pick_mele_in_hand();                                    // Drop and Switch weapons
        }




        if (grabweapon)
        {
          
            if (Input.GetKey(KeyCode.F))                                                //Grab weapon from ground
              {
                   DesableUI();
 
                  if (Gunobj!=null&&(Gunobj.gameObject.tag == "Rifle"||Gunobj.gameObject.tag == "Sniper"||Gunobj.gameObject.tag == "Mini"))
                  { 

                       if (!gunattachedrightbool)
                       {   
                                                                             //gun take on right solder;
                            Gunobj.GetComponent<GUN_INFO>().gunpriority = GUN_INFO.GunPriority.Primary;
                            PrimaryWeapon=Gunobj;                     
                            TakeGun(gunattachedright);
                            gunattachedrightbool=true;   
                            WeaponimageAttachedRightofMiniMap(1);
                        

                                                                   
                        }
                        else if(!gunattachedleftbool)
                        {                                                                                  //gun take on Left solder;
                            Gunobj.GetComponent<GUN_INFO>().gunpriority= GUN_INFO.GunPriority.Secondory;
                            SecondoryWeapon=Gunobj;                     
                            TakeGun(gunattachedleft);
                            gunattachedleftbool=true;  
                            WeaponimageAttachedRightofMiniMap(2); 
                 
                        }else
                        {                        
                               Dropgun_and_takeotherone();    
                               WeaponimageAttachedRightofMiniMap(1);
                             Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAa");                                                //drop curent gun and new gun attached to right side
                           // Drop_and_take_weapon(PrimaryWeapon,gunattachedright,"Rifle");
                        }  
                }else if (Gunobj!=null&&Gunobj.gameObject.tag == "Melee")
                {
                        if (!MeleeAttachedbool)
                       {   
                                                                                                           //gun take on Middle solder;
                       
                            Gunobj.GetComponent<GUN_INFO>().gunpriority = GUN_INFO.GunPriority.Melee;
                            MeleeWeapon=Gunobj;                     
                            TakeGun(Meleeattacher);
                            MeleeAttachedbool=true;     
                            WeaponimageAttachedRightofMiniMap(4);    
                                 
                        }else
                        {
                           Dropmelee_and_takeontherone();
                           WeaponimageAttachedRightofMiniMap(4);
                           //  Drop_and_take_weapon(MeleeWeapon,Meleeattacher,"Melee");
                        }




                    
                }

            }
        }
        
    }

    public void Dropgun_and_takeotherone()                 //gun drop ground and pick another weapon from ground
    {
            Debug.Log("Drop gun");
           

            PrimaryWeapon.transform.position=gundroppoint.position; 
            PrimaryWeapon.transform.rotation = Quaternion.Euler(0, 0, 0);
            PrimaryWeapon.GetComponent<GUN_INFO>().gunpriority = GUN_INFO.GunPriority.None;
            PrimaryWeapon.GetComponent<BoxCollider>().enabled = true;
          
            PrimaryWeapon.transform.parent = null;
          
            string name= PrimaryWeapon.GetComponent<GUN_INFO>().gunclassname.ToString();    
            animationlayermaskselect(name,0);
            Gunobj.GetComponent<GUN_INFO>().gunpriority = GUN_INFO.GunPriority.Primary;
            PrimaryWeapon=Gunobj; 
            TakeGun(gunattachedright);                       
            gunattachedrightbool=true;  







                    
    }


    public void WeaponimageAttachedRightofMiniMap(int gunpriority)
    {
        switch (gunpriority)
        {
            case 1:

                            PweaponsImages.SetActive(true); 
                            PweaponsImages.GetComponent<Image>().sprite = PrimaryWeapon.GetComponent<GUN_INFO>().Gunimage;
              
                            objCanvasUpdateScript.updatePrimaryWeaponImage(PrimaryWeapon);
            break;
            case 2:

                            SweaponsImages.SetActive(true); 
                            SweaponsImages.GetComponent<Image>().sprite = SecondoryWeapon.GetComponent<GUN_INFO>().Gunimage; 
            break;
            case 3:

            break;
            case 4:

                            MweaponsImages.SetActive(true); 
                            MweaponsImages.GetComponent<Image>().sprite = MeleeWeapon.GetComponent<GUN_INFO>().Gunimage;    
            break;
            
            default:
            break;
        }

                         
    }

    public void DesableUI()
    {
         GameObject.Find("Gunimage").GetComponent<Image>().color = new Color(0,0,0,0);
        GameObject.Find("GunText").GetComponent<Text>().text = "";
        GameObject.Find("POPUPText").GetComponent<Text>().text="";
        GameObject.Find("Gundamagetext").GetComponent<Text>().text = "";
        GameObject.Find("Gundamage").GetComponent<Text>().text = "";
    }
    public void EnableUI(GameObject other)
    {

            GameObject.Find("Gunimage").GetComponent<Image>().color = new Color(1, 1, 1, 1);
            GameObject.Find("Gunimage").GetComponent<Image>().sprite = other.gameObject.GetComponent<GUN_INFO>().Gunimage;
            GameObject.Find("GunText").GetComponent<Text>().text = other.gameObject.GetComponent<GUN_INFO>().Gunname;
            GameObject.Find("Gundamagetext").GetComponent<Text>().text = other.gameObject.GetComponent<GUN_INFO>().Damage.ToString();
            GameObject.Find("Gundamage").GetComponent<Text>().text = "DAMAGE";

    }

    public void Dropmelee_and_takeontherone()               //melee drop ground and pick another melee from ground
    
    {
            Debug.Log("Drop Melee");
           

            MeleeWeapon.transform.position=gundroppoint.position; 
            MeleeWeapon.transform.rotation = Quaternion.Euler(0, 0, 0);
            MeleeWeapon.GetComponent<GUN_INFO>().gunpriority = GUN_INFO.GunPriority.None;
            MeleeWeapon.GetComponent<BoxCollider>().enabled = true;
          
            MeleeWeapon.transform.parent = null;
            MeleeWeapon=Gunobj;  
            string name= MeleeWeapon.GetComponent<GUN_INFO>().gunclassname.ToString();    
            animationlayermaskselect(name,0);
            Gunobj.GetComponent<GUN_INFO>().gunpriority = GUN_INFO.GunPriority.Melee;
       
            TakeGun(Meleeattacher);                       
            MeleeAttachedbool=true; 
    }






    private void TakeGun(Transform arm)                     
    {


        Gunobj.transform.position = arm.position;
        Gunobj.transform.localPosition = Vector3.zero;
        Gunobj.transform.SetParent(arm.transform,false);
        Gunobj.GetComponent<BoxCollider>().enabled = false;
        Gunobj=null;


    }

    private void Pick_Weapon_in_hand()//E
    {


        if (gunattachedrightbool)       ///pathivar gun ahe ki nahi
        {
           
                             
            
                    if (Pick_Weapon_in_handbool)                                                        //if bool true idle
                    {   Debug.Log("  Weapon  true");
                      playeranim.SetBool("putback", true);
                        Pick_Weapon_in_handbool=false;
                        Pick_Weapon_in_handmethod(PrimaryWeapon,gunattachedright,0);
                         playeranim.SetBool("putback", false);

                    }
                    else
                    {
                           if (Pick_mele_in_handbool)                                                       //if bool true then gun is in hand
                            {
                                Debug.Log("melee  true");
                                Pick_mele_in_handbool=false;
                                 Pick_Weapon_in_handmethod(MeleeWeapon,Meleeattacher,0);
                                // playeranim.SetBool("Jump", true);
                            }

                 
                        playeranim.SetBool("grab", true); 
                        Pick_Weapon_in_handbool=true;                                                                                                                //else pos
                        Pick_Weapon_in_handmethod(PrimaryWeapon,gunholder,1);  
                       
                       // playeranim.SetBool("grab", false); 
                     ObjGunHolderScript.readyForFire(PrimaryWeapon);
                          
                    }
                
        }       
        

    }

    public void Pick_mele_in_hand()
    {
        if (MeleeAttachedbool)
        {
            
                if (Pick_mele_in_handbool)                                                       //if bool true then gun is in hand
                {

                    Debug.Log("melee  true");
                    Pick_mele_in_handbool=false;
                     //playeranim.SetBool("Jump", true);
                     Pick_Weapon_in_handmethod(MeleeWeapon,Meleeattacher,0);

                }   
                else                                                                                         //if bool is false then idel
                {
                            if (Pick_Weapon_in_handbool)                                                        //if bool true idle
                            {   Debug.Log("  Weapon  true");
                                Pick_Weapon_in_handbool=false;
                                  playeranim.SetBool("grab", true); 
                                Pick_Weapon_in_handmethod(PrimaryWeapon,gunattachedright,0);
                            }
                    Debug.Log("melee  false");
                    Pick_mele_in_handbool=true;
                  //   playeranim.SetBool("Jump", true);
                    Pick_Weapon_in_handmethod(MeleeWeapon,MeleeHolder,1);              
                }
        
       

        }

    }
    public void Pick_Weapon_in_handmethod(GameObject Wepaon, Transform weaponparent,int animationmaskvalue)
    {
        
        Wepaon.transform.position=weaponparent.position;   
        Wepaon.transform.SetParent(weaponparent,false);
        name= Wepaon.GetComponent<GUN_INFO>().gunclassname.ToString();   
        animationlayermaskselect(name,animationmaskvalue);
        Wepaon.transform.localPosition=Vector3.zero;

    }



    private void animationlayermaskselect(string name,float value)
    {
        playeranim.SetLayerWeight(playeranim.GetLayerIndex(name), value);
    }

    private void OnTriggerEnter(Collider other)
    {
       if(other.gameObject.tag == "Rifle")
        {
            Gunobj=other.gameObject;
            EnableUI(other.gameObject);

             grabweapon=true;
            if (gunattachedrightbool&&gunattachedleftbool)
            {
           GameObject.Find("POPUPText").GetComponent<Text>().text ="Press F to swtich Weapon";

            }
             else
            {
                GameObject.Find("POPUPText").GetComponent<Text>().text ="Press F to grab Weapon";
            } 

        }
        if (other.gameObject.tag == "Melee")
        {
            Gunobj=other.gameObject;
                grabweapon=true;
                EnableUI(other.gameObject);
            if (MeleeAttachedbool)
            {
             GameObject.Find("POPUPText").GetComponent<Text>().text ="Press F to swtich Weapon";

            }else
            {
                GameObject.Find("POPUPText").GetComponent<Text>().text ="Press F to grab Weapon";
            }
            
        }




    }
    private void OnTriggerExit(Collider other)
    {
        DesableUI();

         grabweapon=false;
    }
    public void Changging_Gun_Priority()
    {
        if (gunattachedrightbool&&gunattachedleftbool)
        {
           

                if (Chnagepriority)
                {
                     Chnagepriority=false;
                     SwapPos_of_guns();
                }
                else
                {
                       Chnagepriority=true;
                        SwapPos_of_guns();
                }
                     
        }
        else
        {
            Debug.Log("You have only one wepaon so cannot swap it");
        }

    }
    public void SwapPos_of_guns()

    {
                        Pick_Weapon_in_handbool=false;
                        SecondoryWeapon.transform.position =  gunattachedright.position;                       
                        SecondoryWeapon.transform.SetParent(gunattachedright,false);
                        SecondoryWeapon.GetComponent<GUN_INFO>().gunpriority = GUN_INFO.GunPriority.Primary;

                        
                     
                 
                        PrimaryWeapon.transform.position=  gunattachedleft.position;                    
                        PrimaryWeapon.transform.SetParent(gunattachedleft,false);
                        PrimaryWeapon.GetComponent<GUN_INFO>().gunpriority = GUN_INFO.GunPriority.Secondory;
                     
                        PrimaryWeapon=gunattachedright.transform.GetChild(0).gameObject;
                        SecondoryWeapon=gunattachedleft.transform.GetChild(0).gameObject;   
                        PrimaryWeapon.transform.localPosition=Vector3.zero;
                        SecondoryWeapon.transform.localPosition=Vector3.zero;
                         WeaponimageAttachedRightofMiniMap(1);
                          WeaponimageAttachedRightofMiniMap(2);
                       // Debug.Log("SecondoryWeapon position  =====>>>>"+    SecondoryWeapon.transform.position    +" PrimaryWeapon.transform.position===================++>>>>>>>"+PrimaryWeapon.transform.position  );
                        Pick_Weapon_in_hand();
    }


}
