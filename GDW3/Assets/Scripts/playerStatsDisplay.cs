using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class playerStatsDisplay : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Text>().text = 
            "hitAccuracy: " + saveLoadingManager.singleton.getData()[0].ToString() + 
            "\nnumberOfChargedAttacks: " + saveLoadingManager.singleton.getData()[1].ToString() + 
            "\nnumberOfTimesHit: " + saveLoadingManager.singleton.getData()[2].ToString() + 
            "\nnumberOfKills: " + saveLoadingManager.singleton.getData()[3].ToString() + 
            "\nhealthHealed: " + saveLoadingManager.singleton.getData()[4].ToString() + 
            "\nnumberOfDeaths: " + saveLoadingManager.singleton.getData()[5].ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
