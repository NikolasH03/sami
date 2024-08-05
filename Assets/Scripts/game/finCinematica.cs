using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class finCinematica : MonoBehaviour
{

    [SerializeField] VideoPlayer videoPlayer;  
    [SerializeField] string sceneName;       

    void Start()
    {
        // Asegúrate de que el Video Player no se detenga al finalizar
        videoPlayer.isLooping = false;

        // Suscribirse al evento que se dispara cuando el video termina
        videoPlayer.loopPointReached += OnVideoEnd;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangeScene();
        }
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // Cambiar a la escena especificada
        ChangeScene();
    }
    void ChangeScene()
    {
        // Cambiar a la escena especificada
        SceneManager.LoadScene(sceneName);
    }
}
