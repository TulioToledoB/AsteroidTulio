using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject prefabBullet;
    public float speedThrusting = 2.0f;
    public float speedTurn = 1.0f;
    public float turnDirection = 0.0f;
    private bool thrusting = false;
    public Rigidbody2D rb;
    public AudioClip engineSound; // Clip de sonido para el motor
    private AudioSource engineAudioSource; // Fuente de audio para el motor

    void Start()
    {
        engineAudioSource = gameObject.AddComponent<AudioSource>();
        engineAudioSource.clip = engineSound;
        engineAudioSource.loop = true; // Hace que el sonido se repita
        engineAudioSource.playOnAwake = false;
    }

    void Update()
    {
        thrusting = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            turnDirection = 1.0f;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            turnDirection = -1.0f;
        }
        else
        {
            turnDirection = 0.0f;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Shot();
        }
        Teleport();   

        // Reproduce el sonido del motor cuando la nave se mueve
        if (thrusting || turnDirection != 0)
        {
            if (!engineAudioSource.isPlaying)
            {
                engineAudioSource.Play();
            }
        }
        else
        {
            if (engineAudioSource.isPlaying)
            {
                engineAudioSource.Stop();
            }
        }
    }

    private void FixedUpdate()
    {
        if (thrusting)
        {
            rb.AddForce(transform.up * speedThrusting);
        }

        if (turnDirection != 0)
        {
            rb.AddTorque(turnDirection * speedTurn);    
        }
    }

    private void Shot()
    {
        GameObject o = Instantiate(prefabBullet, transform.position, transform.rotation, transform);
        Bullet b = o.GetComponent<Bullet>();
        b.Shot(transform.up);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid")) // Asegúrate de que el tag de tu asteroide es "Asteroid"
        {
            GameManager.instance.LoseLife();
            // Opcional: Agregar lógica adicional como una breve invulnerabilidad, efectos visuales, etc.
        }
    }

    private void Teleport()
    {
        var viewportPosition = Camera.main.WorldToViewportPoint(transform.position);
        bool isTeleported = false;

        // Verificar límites horizontales y ajustar la posición X
        if (viewportPosition.x > 1)
        {
            viewportPosition.x = 0;
            isTeleported = true;
        }
        else if (viewportPosition.x < 0)
        {
            viewportPosition.x = 1;
            isTeleported = true;
        }

        // Verificar límites verticales y ajustar la posición Y
        if (viewportPosition.y > 1)
        {
            viewportPosition.y = 0;
            isTeleported = true;
        }
        else if (viewportPosition.y < 0)
        {
            viewportPosition.y = 1;
            isTeleported = true;
        }

        // Si el jugador fue teletransportado, actualizamos su posición en el mundo
        if (isTeleported)
        {
            transform.position = Camera.main.ViewportToWorldPoint(viewportPosition);
        }
    }
}