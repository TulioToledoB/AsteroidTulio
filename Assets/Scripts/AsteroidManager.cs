using System.Collections;
using UnityEngine;

public class SpawnerDeAsteroides : MonoBehaviour
{
    public GameObject[] prefabsDeAsteroides; // Array de prefabs de asteroides
    public Transform contenedorDeAsteroides; // Contenedor de asteroides

    private void Start()
    {
        IniciarGeneracionDeAsteroides();
    }

    public void IniciarGeneracionDeAsteroides()
    {
        // Inicia la Coroutine para generar asteroides
        StartCoroutine(RutinaGenerarAsteroides());
    }

    private IEnumerator RutinaGenerarAsteroides()
    {
        // Continúa generando asteroides indefinidamente
        while (true)
        {
            CrearAsteroide(prefabsDeAsteroides[Random.Range(0, prefabsDeAsteroides.Length)], ObtenerPosicionAleatoria(), Quaternion.identity);
            yield return new WaitForSeconds(2f); // Intervalo entre la generación de asteroides
        }
    }

    private void CrearAsteroide(GameObject prefabDeAsteroide, Vector3 posicion, Quaternion rotacion)
    {
        // Crea una instancia del asteroide y lo coloca dentro del contenedor
        GameObject instanciaDeAsteroide = Instantiate(prefabDeAsteroide, posicion, rotacion, contenedorDeAsteroides);
        // Configuraciones adicionales si son necesarias
    }

    private Vector3 ObtenerPosicionAleatoria()
    {
        // Genera una posición aleatoria fuera de los límites de la pantalla
        float x = Random.Range(-10f, 10f);
        float y = Random.Range(-10f, 10f);
        return new Vector3(x, y, 0);
    }
}