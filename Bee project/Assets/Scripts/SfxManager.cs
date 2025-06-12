using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxManager : MonoBehaviour
{
    public static SfxManager Instance;
    
    private AudioSource audioSource;

    [SerializeField] private AudioClip beeBuzz;
    [SerializeField] private AudioClip beeSlurp;
    [SerializeField] private AudioClip pop;
    [SerializeField] private AudioClip hivePop;

    [SerializeField] private AudioClip antAttack;
    [SerializeField] private AudioClip workerAttack;
    [SerializeField] private AudioClip soldierAttack;
    [SerializeField] private AudioClip spiderAttack;
    [SerializeField] private AudioClip badgerAttack;

    [SerializeField] private AudioClip buttonClick;

    
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

    //currently used when pick up /put down egg in in WorkerBeeLogic, MovementAnimationUpdate
    public void playPopSound() {
        audioSource.PlayOneShot(pop);
    }

    //currenlty in all Bees' Update method when teleporting in/out
    public void playHivePopSound() {
        audioSource.PlayOneShot(hivePop);
    }

    //currently used when start to attack in EnemyAntLogic, Attack
    public void playAntAttackSound() {
        audioSource.PlayOneShot(antAttack);
    }

    //currently used when start to attack in WorkerBeeLogic, TaskAttacking
    public void playWorkerAttackSound() {
        audioSource.PlayOneShot(workerAttack);
    }
    
    //currently used when start to attack in WorkerBeeLogic, TaskAttacking
    public void playSoldierAttackSound() {
        audioSource.PlayOneShot(soldierAttack);
    }

    //currently used when nectar counter goes up in WorkerBeeLogic, TaskCollectNectar
    public void playSlurpSound() {
        audioSource.PlayOneShot(beeSlurp);
    }

    //currently used when attacks in EnemySpiderLogic, Attack
    public void playSpiderAttackSound() {
        audioSource.PlayOneShot(spiderAttack);
    }

    //currently used when attacks in EnemyBadgerLogic, Attack
    public void playBadgerAttackSound() {
        audioSource.PlayOneShot(badgerAttack);
    }

    public void playButtonClickSound() {
        audioSource.PlayOneShot(buttonClick);
    }

    //template
    // public void play() {
    //     audioSource.PlayOneShot();
    // }
}