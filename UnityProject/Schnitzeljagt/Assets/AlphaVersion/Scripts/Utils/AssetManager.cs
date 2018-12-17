using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetManager : MonoBehaviour {

    public GameObject[] Characters;
    
    public GameObject GetCharacter(string name)
    {
        switch(name)
        {
            case "":
                return Characters[0];
                break;
            default:
                break;
        }

        return Characters[0];
    }
}
