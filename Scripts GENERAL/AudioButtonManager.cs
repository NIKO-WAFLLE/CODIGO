using UnityEngine;
using UnityEngine.UI;

public class AudioButtonManager : MonoBehaviour
{
    public Button button1;
    public Button button2;
    public Button button3;
    public AudioSource audioSource1;
    public AudioSource audioSource2;
    public AudioSource audioSource3;

    void Start()
    {
        // Asignamos las funciones a los botones
        button1.onClick.AddListener(() => PlayAudio(audioSource1));
        button2.onClick.AddListener(() => PlayAudio(audioSource2));
        button3.onClick.AddListener(() => PlayAudio(audioSource3));
    }

    // Función para reproducir el audio correspondiente y detener los demás
    void PlayAudio(AudioSource audioToPlay)
    {
        // Detenemos todos los audios
        audioSource1.Stop();
        audioSource2.Stop();
        audioSource3.Stop();

        // Activamos el audio que corresponde al botón presionado
        audioToPlay.Play();
    }
}
