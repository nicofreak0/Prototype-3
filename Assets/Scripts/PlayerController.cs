using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;               // Reference to the Animator component
    public float jumpForce = 5.0f;             // Set the jump force for the player
    public float gravityModifier = 1.0f;       // Set a custom gravity modifier
    public bool isOnGround = true;             // Track if the player is on the ground
    public bool gameOver = false;              // Track if the game is over
    public ParticleSystem explosionParticle;    // Reference to the explosion particle system
    public ParticleSystem dirtParticle;         // Reference to the dirt particle system
    public AudioClip jumpSound;                 // Reference to the jump sound effect
    public AudioClip crashSound;                // Reference to the crash sound effect
    private AudioSource playerAudio;            // Reference to the AudioSource component

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();  // Initialize the Animator component
        playerAudio = GetComponent<AudioSource>(); // Initialize the AudioSource component

        // Adjust the gravity by multiplying it with the gravityModifier
        Physics.gravity *= gravityModifier;

        // Make the player jump at the start of the game
        playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isOnGround = false;  // Player is not on the ground after the initial jump
        playerAnim.SetTrigger("Jump_trig");  // Trigger jump animation at the start
        dirtParticle.Stop();  // Stop dirt particle initially
    }

    void Update()
    {
        // Check if the spacebar is pressed, if the player is on the ground, and the game is not over
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            // Make the player jump
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;  // Set isOnGround to false, as the player is now in the air

            // Play the jump sound effect
            playerAudio.PlayOneShot(jumpSound, 1.0f); // Play jump sound at full volume

            // Trigger the jump animation
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();  // Stop the dirt particle when jumping
        }
       if (gameOver)
        {
            dirtParticle.Stop();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the player has collided with the Ground or an Obstacle
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticle.Play();  // Play the dirt particle effect when landing on the ground
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            Debug.Log("Game Over!"); // Optional: Log a message to indicate game over

            // Trigger the death animation
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);

            // Play the explosion particle effect
            explosionParticle.Play();
            dirtParticle.Stop();  // Stop the dirt particle effect when colliding with an obstacle

            // Play the crash sound effect
            playerAudio.PlayOneShot(crashSound, 1.0f); // Play crash sound at full volume
        }
    }
}


