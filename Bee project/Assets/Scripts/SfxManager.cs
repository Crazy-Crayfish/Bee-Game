using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxManager : MonoBehaviour
{
    public static SfxManager Instance;
    
    private AudioSource audioSource;

    [SerializeField] private AudioClip beeBuzz;
    [SerializeField] private AudioClip pop;
    [SerializeField] private AudioClip click;
    [SerializeField] private AudioClip workerAttack;
    [SerializeField] private AudioClip soldierAttack;
    [SerializeField] private AudioClip slurp;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    //currently used when switch X direction in WorkerBeeLogic, MovementAnimationUpdate
    public void playBuzzSound()
    {
        audioSource.PlayOneShot(beeBuzz);
    }

    //currently NOT IMPLEMENTED - for teleporting to hive?
    public void playPopSound() {
        audioSource.PlayOneShot(pop);
    }

    //currently used when start to attack in EnemyAntLogic, Attack
    public void playClickSound() {
        audioSource.PlayOneShot(click);
    }

    //currently used when start to attack in WorkerBeeLogic, TaskAttacking
    public void playWorkerAttackSound() {
        audioSource.PlayOneShot(workerAttack);
    }
    
    //currently NOT IMPLEMENTED - for soldier attack
    public void playSoldierAttackSound() {
        audioSource.PlayOneShot(soldierAttack);
    }

    //currently used when nectar counter goes up in WorkerBeeLogic, TaskCollectNectar
    public void playSlurpSound() {
        audioSource.PlayOneShot(slurp);
    }

    //template
    // public void play() {
    //     audioSource.PlayOneShot();
    // }
}