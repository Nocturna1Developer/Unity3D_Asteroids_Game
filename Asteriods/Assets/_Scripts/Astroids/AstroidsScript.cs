using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class AstroidsScript : MonoBehaviour
{
    [Header("Astroid Physics")]
    [SerializeField] public float maxThrust;
    [SerializeField] public float maxTorque;

    [Header("Astroid Sizes")]
    [SerializeField] public int astroidSize;
    [SerializeField] GameObject astroidMedium;
    [SerializeField] GameObject astroidSmall;

    [Header("Rigidbody")]
    [SerializeField] public Rigidbody rb;

    [Header("Astroid Sounds")]
    [SerializeField] public AudioClip collideSoundLarge;
    [SerializeField] public AudioClip collideSoundMedium;
    [SerializeField] public AudioClip collideSoundSmall;
    private AudioSource audioSource;

    //[Header("Particles")]
    //[SerializeField] ParticleSystem explosionParticles;

    // The physics of the astroids
    void Start()
    {
        // Makes the astroids bounce all over the screen as soon as the game starts
        Vector3 thrust = new Vector3(Random.Range(-maxThrust, maxThrust), Random.Range(-maxThrust, maxThrust));

        float torque = Random.Range(-maxTorque, maxTorque);

        // Adds force to both of the thrust and torque
        rb.AddForce(thrust, ForceMode.Impulse);
        rb.AddTorque(thrust);

        // Gets the audio souce component
        audioSource = GetComponent<AudioSource>();
    }

    // If we trigger with the astroid then it will split up or dissapear depending on the size 
    private void OnTriggerEnter(Collider other) 
    {
        // Bullet will break astroid and will add points
        if (other.gameObject.tag == "Bullet")
        {
            if(astroidSize == 3)
            {
                GameObject astriod1 = Instantiate(astroidMedium, transform.position, transform.rotation);
                astriod1.GetComponent<AstroidsScript>().astroidSize = 2;
                Score.scoreAmount += 10;
                other.GetComponent<AudioSource>().PlayOneShot(collideSoundLarge);
                audioSource.Play();
                //explosionParticles.Play();
                Destroy(gameObject);
            }
            else if (astroidSize == 2)
            {
                GameObject astriod2 = Instantiate(astroidSmall, transform.position, transform.rotation);
                astriod2.GetComponent<AstroidsScript>().astroidSize = 1;
                other.GetComponent<AudioSource>().PlayOneShot(collideSoundMedium);
                Score.scoreAmount += 20;
                audioSource.Play();
                //explosionParticles.Play();
                Destroy(gameObject);
            }
            else if (astroidSize == 1)
            {
                other.GetComponent<AudioSource>().PlayOneShot(collideSoundSmall);
                Score.scoreAmount += 40;
                audioSource.Play();
                //explosionParticles.Play();
                Destroy(gameObject);
            }
        }

        // Player ship will break astroid but wont add points
        if (other.gameObject.tag == "PlayerShip")
        {
            if (astroidSize == 3)
            {
                GameObject astriod1 = Instantiate(astroidMedium, transform.position, transform.rotation);
                astriod1.GetComponent<AstroidsScript>().astroidSize = 2;
                other.GetComponent<AudioSource>().PlayOneShot(collideSoundLarge);
                
                Destroy(gameObject);
            }
            else if (astroidSize == 2)
            {
                GameObject astriod2 = Instantiate(astroidSmall, transform.position, transform.rotation);
                astriod2.GetComponent<AstroidsScript>().astroidSize = 1;
                other.GetComponent<AudioSource>().PlayOneShot(collideSoundMedium);
                Destroy(gameObject);
            }
            else if (astroidSize == 1)
            {
                other.GetComponent<AudioSource>().PlayOneShot(collideSoundSmall);
                Destroy(gameObject);
            }
        }
    }
}