using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunHolderScript : MonoBehaviour
{
    // Start is called before the first frame update
    public int MaxAmmo,ReamingAmmo,ChipSize;
    public GameObject MainWeapon;





    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void readyForFire(GameObject Weapon)
    {

        MainWeapon=Weapon;
        
    
    
    
    }
}
