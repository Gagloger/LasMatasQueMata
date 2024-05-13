using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaUI : MonoBehaviour
{
    private void Start()
    {
        
    }
    private void Update()
    {
        transform.Find("Bar").localScale = new Vector3(GetComponent<SistemaCombate>().GetHealthPercent(), 1);
        
    }

}
