using UnityEngine;

public class Asteroid : MonoBehaviour {
    public GameObject subRockPrefab; // Prefab para los asteroides más pequeños
    public int rockSize; // Tamaño del asteroide
    public int pointsValue; // Puntos otorgados por el asteroide
    public float thrustForce = 2f; // Fuerza aplicada al movimiento
    public Transform rocksContainer; // Contenedor para los asteroides instanciados
    public float lifeTime = 20f;
    private Rigidbody2D rockRb;
    public AudioClip collisionSound; // Clip de sonido para la colisión
    private AudioSource audioSource; // Fuente de audio

    public void Start(){
        Destroy(gameObject, lifeTime);
       
    }

    private void Awake() {
         audioSource =GetComponent<AudioSource>();
        // Inicializa el componente Rigidbody2D
        rockRb = GetComponent<Rigidbody2D>();
        // Calcula dirección hacia el jugador y aplica fuerza
        SeekPlayer();

    }

    private void SeekPlayer() {
        // Busca al jugador y calcula la dirección
        Player targetPlayer = FindObjectOfType<Player>();
        if (targetPlayer != null) {
            Vector2 targetDirection = (targetPlayer.transform.position - transform.position).normalized;
            rockRb.AddForce(targetDirection * thrustForce, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D impact) {
        if (impact.gameObject.CompareTag("Projectile")) { // Verifica el impacto con un proyectil
            HandleCollisionWithProjectile(impact);
        }
    }

    private void HandleCollisionWithProjectile(Collision2D impact) {
        // Suma puntos y destruye el asteroide y la bala
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
        // Crea dos fragmentos de asteroide y les aplica fuerza
        var fragmentOne = CreateRockFragment();
        var fragmentTwo = CreateRockFragment();
        ApplyForceToFragment(fragmentOne);
        ApplyForceToFragment(fragmentTwo);
    }

    private GameObject CreateRockFragment() {
        // Instancia un fragmento de asteroide
        return Instantiate(subRockPrefab, transform.position, Quaternion.Euler(0, 0, Random.Range(0f, 360f)), rocksContainer);
    }

    private void ApplyForceToFragment(GameObject fragment) {
        // Aplica una fuerza aleatoria al fragmento de asteroide
        Vector2 randomForce = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        Rigidbody2D fragmentRb = fragment.GetComponent<Rigidbody2D>();
        fragmentRb.AddForce(randomForce * thrustForce, ForceMode2D.Impulse);
    }
}