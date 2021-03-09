using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonScript : MonoBehaviour
{
    public static ButtonScript Instance { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChooseNum()
    {
        if (gameObject.CompareTag(GameManager.Instance.turnOrder[GameManager.Instance.curTurnOrderIndex]))
            GameManager.Instance.SetEquation(gameObject.GetComponentInChildren<TextMeshProUGUI>().text);
    }

   
}
