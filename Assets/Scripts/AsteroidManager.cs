using System.Collections;
using UnityEngine;

public class SpawnerDeAsteroides : MonoBehaviour
{
    public GameObject[] prefabsDeAsteroides; 
    public Transform contenedorDeAsteroides; 

    private void Start()
    {
        IniciarGeneracionDeAsteroides();
    }

    public void IniciarGeneracionDeAsteroides()
    {
        
        StartCoroutine(RutinaGenerarAsteroides());
    }

    private IEnumerator RutinaGenerarAsteroides()
    {
      
        while (true)
        {
            CrearAsteroide(prefabsDeAsteroides[Random.Range(0, prefabsDeAsteroides.Length)], ObtenerPosicionAleatoria(), Quaternion.identity);
            yield return new WaitForSeconds(2f); 
        }
    }

    private void CrearAsteroide(GameObject prefabDeAsteroide, Vector3 posicion, Quaternion rotacion)
    {
        
        GameObject instanciaDeAsteroide = Instantiate(prefabDeAsteroide, posicion, rotacion, contenedorDeAsteroides);
        
    }

    private Vector3 ObtenerPosicionAleatoria()
    {
      
        float x = Random.Range(-10f, 10f);
        float y = Random.Range(-10f, 10f);
        return new Vector3(x, y, 0);
    }
}