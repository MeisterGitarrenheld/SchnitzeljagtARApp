using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetManager : MonoBehaviour {

    public GameObject[] Characters;
    
    public GameObject GetCharacter(string name)
    {
        switch(name)
        {
            case "Eberhard_Guide":
                return Characters[0];
                break;
            case "Hoelderlin":
                return Characters[1];
                break;
            case "Auktorialer_Guide":
                return Characters[2];
                break;
            default:
                break;
        }

        return Characters[0];
    }
}
