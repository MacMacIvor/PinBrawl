using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class toEndScene : MonoBehaviour
{
    public static toEndScene singleton = null;
    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
            return;
        }
        Destroy(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void callNext()
    {
        SceneManager.LoadScene("EndScene");
    }

}
