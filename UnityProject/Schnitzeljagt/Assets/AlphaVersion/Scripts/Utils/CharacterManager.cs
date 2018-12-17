using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{

    public Dictionary<string, GameObject> Characters { get; private set; }
        
    void Start()
    {
        Characters = new Dictionary<string, GameObject>();
    }

}
