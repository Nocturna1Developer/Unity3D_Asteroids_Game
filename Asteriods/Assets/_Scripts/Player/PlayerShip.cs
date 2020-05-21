using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]

public class PlayerShip : MonoBehaviour
{
    // This is a singleton - this mean that it is a design pattern that is it's only instance of a class.
    // Checks if the class is not set twice
    static private PlayerShip _S;

    static public PlayerShip S 
    {
        get
        {
            return _S;
        }   

        private set 
        {
            if(_S != null)
            {
                Debug.LogWarning("Second Attempt to set PlayerShip Singleton _S");
            }
            _S = value;
        }
    }

    static public float MAX_SPEED
    {
        get
        {
            return S.moveSpeed;
        }
    }

    [Header("Ship Physics")]
    [SerializeField] public float moveSpeed = 100f;
    [SerializeField] public float bulletForce = 20f;
    [SerializeField] public float deathForce = 3f;

    [Header("Bullet")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] public Transform shootPoint;
    
    [Header("Rigidbody")]
    Rigidbody rb;

    [Header("Audio")]
    [SerializeField] public AudioSource audioSource;
    [SerializeField] public AudioClip shootSound;

    [Header("Particles")]
    [SerializeField] public ParticleSystem muzzleFlash;
    [SerializeField] ParticleSystem mainEngineParticles;

    [Header("UI")]
    [SerializeField] GameObject gameOverPanel;

    private void Start() 
    {
        S = this;

        rb = GetComponent<Rigidbody>();

        gameOverPanel.SetActive(false);

        audioSource = GetComponent<AudioSource>();

        mainEngineParticles.Play();
    }

    private void Update()
    {
        // Allows us to move left and right
        float movementX = CrossPlatformInputManager.GetAxis("Horizontal");
        float movementY = CrossPlatformInputManager.GetAxis("Vertical");  

        // Stores the movement into a vector
        Vector3 vel = new Vector3(movementX, movementY);

        // Avoids speed multiplying by 1.414 when moving at a diagonal, normalizes back to 1
        if(vel.magnitude > 1)
        {
            vel.Normalize();
        }

        // Multiplies Velocity by the move speed
        rb.velocity = vel * moveSpeed * Time.deltaTime;

        // Calls Fire Function
        if (CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
            Fire();
        }
    }

    void Fire()
    {
        // Spawns in our bullet at out shoot point
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);

        // Assigns it to a rigid body
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        // Adds a force to our bullet at high speeds
        rb.AddForce(shootPoint.up * bulletForce, ForceMode.Impulse);

        // Destroys Bullet
        Destroy(bullet, 2f);

        // Plays the muzzle flash
        muzzleFlash.Play();

        // Plays the shoot sound
        PlayShootSound();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Astroids")
        {
            Lives.livesAmount--;
            TeleportShip();
        }
    }

    private void TeleportShip()
    {
        if(Lives.livesAmount > 0)
        {
            // Gets the current position
            Vector3 newPosition = transform.position;

            // Randomizes the spawn location of the ship
            newPosition.x += 5;
            newPosition.y += 5;

            // Sets the position of the ship to another location
            Vector3 jump = new Vector3(UnityEngine.Random.Range(-newPosition.x, newPosition.x),
                                      UnityEngine.Random.Range(-newPosition.y, newPosition.y));


            // The updated position will now be the new position
            transform.position = newPosition;
        }
        else
        {
            Invoke("RestartLevel", 2f);
            GameOver();
        }
    }

    // Audio for ship
    private void PlayShootSound()
    {
        audioSource.PlayOneShot(shootSound);
    }

    // Game Manager functions
    private void RestartLevel()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(1);
        Lives.livesAmount += 3;
    }

    private void GameOver()
    {
        gameOverPanel.SetActive(true);
    }
}