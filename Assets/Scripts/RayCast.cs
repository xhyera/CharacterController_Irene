using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RayCast : MonoBehaviour
{
    public Text cuentaAtras;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Raycast();
    }

    void Raycast(){ //Poner en empty;
        if(Input.GetButtonDown("Fire1")){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if(Physics.Raycast(ray, out hit)){
                if(hit.transform.tag == "Scene1")  StartCoroutine(CuentaAtras("Scene 1"));

            };
        }
    }

    IEnumerator CuentaAtras(string Scene){
        for (int i = 0; i < 5; i++){
            cuentaAtras.text= i.ToString();
            yield return new WaitForSeconds(1);
        }
        SceneManager.LoadScene("Scene 1");
    }
}
