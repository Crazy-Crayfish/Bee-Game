using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialManagerScript : MonoBehaviour
{

    
    public Animator anim;
    private bool beeSelected;
    private bool flowerPolenated;
    private GameObject firstBee;
    public GameObject arrow;

    // Start is called before the first frame update
    void Start()
    {
        beeSelected = false;
        flowerPolenated = false;
        firstBee = FriendlyUnitCreator.Instance.firstBee;
    }

    // Update is called once per frame
    void Update()
    {
        // Wait for bee selection
        if (anim.GetInteger("Change") == 4)
        {
            Debug.Log("select bee");
            if (UnitSelectionManager.Instance.unitsSelected.Count != 0)
            {
                Debug.Log("SELECTED!");
                changeAnimation();
            }
        }
        // Wait for flower polenation
        else if (anim.GetInteger("Change") == 5)
        {
            if (UnitSelectionManager.Instance.unitsSelected.Count == 0)
            {
                Debug.Log("UNSELECTED!");
                anim.SetInteger("Change", anim.GetInteger("Change") - 1);
            }

            if (ResourceCounter.Instance.getNectar() > 0)
            {
                Debug.Log("POLLENATED!");
                changeAnimation();
            }
        }
        // Wait for nectar collection
        else if (anim.GetInteger("Change") == 7)
        {
            if (ResourceCounter.Instance.getNectar() >= 50)
            {
                Debug.Log("COLLECTED!");
                changeAnimation();
            }
        }
        // Wait for hive enter
        else if (anim.GetInteger("Change") == 10)
        {
            if (ScreenManager.Instance.inHive)
            {
                changeAnimation();
            }
        }
        else 
        {
            if ((Input.GetKeyDown("space")))
            {
                changeAnimation();
            }
        }
    }

    void changeAnimation()
    {
        anim.SetInteger("Change", anim.GetInteger("Change") + 1);
        Debug.Log(anim.GetInteger("Change"));
    }
}
