using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIController : MonoBehaviour
{
    public Image soundOff;
    public Image soundOn;
    public AudioSource source;
    private float volume;
    public bool dontDestroy;
    private void Awake()
    {
        if(dontDestroy)
        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
        if(source!=null)
        volume = source.volume;
    }
    public void Play()
    {
        Application.LoadLevel(1);
        gameObject.SetActive(false);
    }
    public void Return()
    {
        Application.LoadLevel(0);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void DisableSound(bool state)
    {
        soundOff.gameObject.SetActive(state);
        soundOn.gameObject.SetActive(!state);
        if (state)
        {
            source.volume = 0;

        }
        else source.volume = volume;
    }
}
