using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyPanel : MonoBehaviour
{
    public Image img;
    public Sprite sp;
    public List<Sprite> sprites; 
    public int charges = 5;
    public float timer;
    
    // Start is called before the first frame update
    void Start()
    {
        sp = img.GetComponent<Image>().sprite;
    }

    // Update is called once per frame
    void Update()
    {
        switch (charges)
        {
            case 0:
                img.GetComponent<Image>().sprite = sprites[0];
            break;
            case 1:
                img.GetComponent<Image>().sprite = sprites[1];
            break;
            case 2:
                img.GetComponent<Image>().sprite = sprites[2];
            break;
            case 3:
                img.GetComponent<Image>().sprite = sprites[3];
            break;
            case 4:
                img.GetComponent<Image>().sprite = sprites[4];
            break;
            case 5:
                img.GetComponent<Image>().sprite = sprites[5];
            break;
        }  
        timer += Time.deltaTime;
        if (timer > 1.5 && charges < 5)
            {
                timer = 0;
                charges++;
            }
    }
}
