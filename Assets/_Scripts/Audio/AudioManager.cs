using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class AudioManager : MonoBehaviour
{
    //tao bien luu tru audio source
    public AudioSource musicAudioSource;
    public AudioSource vfxAudioSource;
    public AudioSource moveSource;
    //audio clip
    public AudioClip[] footSteps;
    public AudioClip music;
    public AudioClip miss;
    public AudioClip hit;
    public AudioClip hurt;
    public AudioClip jump;
    public AudioClip ground;
    public AudioClip die;
    public AudioClip throwKnife;
    public AudioClip enemyMiss;
    public AudioClip enemyHit;
    public AudioClip enemyHurt;
    public AudioClip enenyDie;
    public AudioClip surfers;
    public AudioClip drink;
    public AudioClip potion;
    public AudioClip buff;
    public AudioClip shield;
    
    private Animator anim;

    void Start() {
        musicAudioSource.clip = music;
        musicAudioSource.loop = true;
        musicAudioSource.volume = 0.1f;
        moveSource.volume = 0.025f;
        vfxAudioSource.volume = 0.1f;
        anim = GameObject.Find("player").GetComponent<Animator>();
        musicAudioSource.Play();
    }

    public void playSteps(){
        AudioClip randomClip = footSteps[Random.Range(0, footSteps.Length)];
        moveSource.clip = randomClip;
        moveSource.PlayOneShot(randomClip);
    }
    public void playSfx(AudioClip sfxClip){
        vfxAudioSource.clip = sfxClip;
        vfxAudioSource.PlayOneShot(sfxClip);
    }
}
