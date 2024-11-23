using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{

    public float scoreCount;
    TextMeshProUGUI score;
    // Start is called before the first frame update
    void Start()
    {

        score = GetComponent<TextMeshProUGUI>();
        
    }

    // Update is called once per frame
    void Update()
    {
        score.text = ""+ scoreCount;
    }
}
