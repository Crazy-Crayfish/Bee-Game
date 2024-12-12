using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private GameObject highlight;
    // public void Init(bool isOffset){
    //     renderer.color = isOffset ? alternateColor : baseColor;

    // }
    
<<<<<<< Updated upstream:Assets/Scripts/Flower.cs
=======
    void OnMouseEnter() {
        highlight.SetActive(true);
        
    }
    void OnMouseExit() {
        highlight.SetActive(false);
    }
>>>>>>> Stashed changes:Bee project/Assets/Scripts/Flower.cs
}
