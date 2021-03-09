using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonScript : MonoBehaviour
{
    public List<GameObject> dests;

    // Start is called before the first frame update
    void Start()
    {
        dests[0].transform.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GoNextDest() 
    {
        for(int i = 0; i < dests.Count-1; i++)
        {
            Debug.Log(gameObject.name + transform.position);
            
            if (transform.position == dests[i].transform.position)
            {
 
                //Debug.Log(gameObject.name + " Transform");
                transform.position = dests[i + 1].transform.position;
                break;
            }
        }
    }
}
