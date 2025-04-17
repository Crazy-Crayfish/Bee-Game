using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoneyBeeIncubator : MonoBehaviour
{
    public bool hasEgg;
    private float incubationTime = 20f;
    private float timeLeft = 20f;
    [SerializeField] private Sprite NoEggSprite;
    [SerializeField] private Sprite YesEggSprite;
    
    // Start is called before the first frame update
    void Awake()
    {
        hasEgg = false;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (hasEgg)
        {
            if (GetComponent<SpriteRenderer>().sprite != YesEggSprite)
            {
                GetComponent<SpriteRenderer>().sprite = YesEggSprite;
            }
            timeLeft -= Time.deltaTime;

            if (timeLeft <= 0.0f)
            {
                GetComponent<SpriteRenderer>().sprite = NoEggSprite;
                spawnBee();
                hasEgg = false;
            }
        }

    }

    private void spawnBee()
    {
        Vector3 pos = gameObject.transform.position + new Vector3(0, 0, -gameObject.transform.position.z);
        FriendlyUnitCreator.Instance.CreateHoney(pos);
    }

    public void startIncubation()
    {
        timeLeft = incubationTime;
        hasEgg = true;
    }
}
