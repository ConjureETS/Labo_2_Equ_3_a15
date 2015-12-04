using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour 
{

	public int startingHealth = 100;                            // The amount of health the player starts the game with.
	public int currentHealth;                                   // The current health the player has.
    public int healthToRecoverOnPickup;
	public Slider healthSlider;                             // Reference to the UI's health bar.
	public AudioClip deathClip;                                 // The audio clip to play when the player dies.
	public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
	public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.
	
	
	Animator anim;                                              // Reference to the Animator component.
	AudioSource playerAudio;                                    // Reference to the AudioSource component.
	PlayerController playerController;                              // Reference to the player's movement.
	//PlayerShoot playerShoot;                              // Reference to the PlayerShooting script.
	bool isDead;                                                // Whether the player is dead.
	bool damaged;                                               // True when the player gets damaged.
	
	
	void Awake ()
	{
		// Setting up the references.
		anim = GetComponent <Animator> ();
		playerAudio = GetComponent <AudioSource> ();
		playerController = GetComponent <PlayerController> ();
		//playerShooting = GetComponentInChildren <PlayerShooting> ();
		
		// Set the initial health of the player.
		currentHealth = startingHealth;
	}
	
	
	void Update ()
	{
		// Reset the damaged flag.
		damaged = false;
	}


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("HealthPickup"))
        {
            Debug.Log("Health Pickup");
            currentHealth = currentHealth + healthToRecoverOnPickup;
            healthSlider.value = currentHealth;
            Destroy(other);
        }
    }

}
