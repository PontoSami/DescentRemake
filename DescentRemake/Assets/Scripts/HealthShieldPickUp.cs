using UnityEngine;
using System.Collections;

/// <summary>
///  Script for health and shield pick ups. This script is attached to the pick ups, and the
///  pick up type is selected from the editor using the pickupType-variable. The player cannot pick these up
///  if his health/shield stats are full. The pick ups dissappear if the player collides with them when not at full
///  health/shield. 
/// 
/// The player must have a collider.
/// 
/// </summary>

public class HealthShieldPickUp : MonoBehaviour {

    public int pickupType; //1 = Health pick up, 2 = Shield pick up
    public int healamount; //The amount of health that is gained from the pickup
    public int shieldamount; //The amount of shield that is gained from the pickup
    public float timetorespawn; //Pickup respawn time in seconds
    public int respawnable; //Pickup respawns if 1
    private float respawntimer;
   
    void Update()
    {
        if (respawnable == 1)
        {

            if (this.gameObject.GetComponent<Renderer>().enabled == false)
            {
                respawntimer += Time.deltaTime;
                if (respawntimer >= timetorespawn)
                {
                    RespawnPickup();
                }
            }
        }
    }

    void RespawnPickup()
    {    
        this.gameObject.GetComponent<Renderer>().enabled = true;
        respawntimer = 0.0f;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            
            if (pickupType == 1)
            {
                GameObject Player = GameObject.Find("PlayerPrefab");
                HealthShield hsclass = Player.GetComponent<HealthShield>();
                
                if (hsclass.health < hsclass.maxHealth && this.gameObject.GetComponent<Renderer>().enabled == true)
                {

                    this.gameObject.GetComponent<Renderer>().enabled = false;

                    hsclass.health += healamount;
                    if (hsclass.health >= hsclass.maxHealth)
                    {
                        hsclass.health = hsclass.maxHealth;
                    }           
                   
                }
            }

            else if(pickupType == 2)
            {
                GameObject Player = GameObject.Find("PlayerPrefab");
                HealthShield hsclass = Player.GetComponent<HealthShield>();

                if (hsclass.shield < hsclass.maxShield && this.gameObject.GetComponent<Renderer>().enabled == true)
                {

                    this.gameObject.GetComponent<Renderer>().enabled = false;
                    
                        hsclass.shield += shieldamount;
                        if (hsclass.shield >= hsclass.maxShield)
                        {
                            hsclass.shield = hsclass.maxShield;
                        }
                        
                }
            }
        }
    }
  
}
