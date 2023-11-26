using UnityEngine;

public class Asteroid : MonoBehaviour {
    public GameObject subRockPrefab; 
    public int rockSize; 
    public int pointsValue; 
    public float thrustForce = 2f;
    public Transform rocksContainer; 
    public float lifeTime = 20f;
    private Rigidbody2D rockRb;
    public AudioClip collisionSound;
    private AudioSource audioSource; 

    public void Start(){
        Destroy(gameObject, lifeTime);
       
    }

    private void Awake() {
         audioSource =GetComponent<AudioSource>();
        
        rockRb = GetComponent<Rigidbody2D>();
        
        SeekPlayer();

    }

    private void SeekPlayer() {
        
        Player targetPlayer = FindObjectOfType<Player>();
        if (targetPlayer != null) {
            Vector2 targetDirection = (targetPlayer.transform.position - transform.position).normalized;
            rockRb.AddForce(targetDirection * thrustForce, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D impact) {
        if (impact.gameObject.CompareTag("Projectile")) { 
            HandleCollisionWithProjectile(impact);
        }
    }

    private void HandleCollisionWithProjectile(Collision2D impact) {
       
        GameManager.instance.AddScore(pointsValue);
        Destroy(impact.gameObject);

        if(audioSource && collisionSound){
            audioSource.PlayOneShot(collisionSound);
        }
        
        if (rockSize > 1) {
            FragmentRock();
        }
        else{
            
            
        Destroy(gameObject);
        }
        
        
    }
    

    private void FragmentRock() {
        
        var fragmentOne = CreateRockFragment();
        var fragmentTwo = CreateRockFragment();
        ApplyForceToFragment(fragmentOne);
        ApplyForceToFragment(fragmentTwo);
    }

    private GameObject CreateRockFragment() {
        
        return Instantiate(subRockPrefab, transform.position, Quaternion.Euler(0, 0, Random.Range(0f, 360f)), rocksContainer);
    }

    private void ApplyForceToFragment(GameObject fragment) {
        
        Vector2 randomForce = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        Rigidbody2D fragmentRb = fragment.GetComponent<Rigidbody2D>();
        fragmentRb.AddForce(randomForce * thrustForce, ForceMode2D.Impulse);
    }
}