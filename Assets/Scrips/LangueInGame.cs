using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LangueInGame : MonoBehaviour
{
    public GameObject[] FR;
    public GameObject[] EN;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toutFR()
    {
        foreach (GameObject obj in FR)
        {
            obj.SetActive(true);
        }

        foreach (GameObject obj in EN)
        {
            obj.SetActive(false);
        }
    }
    public void toutEN()
    {
        foreach (GameObject obj in EN)
        {
            obj.SetActive(true);
        }

        foreach (GameObject obj in FR)
        {
            obj.SetActive(false);
        }
    }
}
