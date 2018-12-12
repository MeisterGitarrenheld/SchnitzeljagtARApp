using UnityEngine;

public class Master : MonoBehaviour
{

    public GameObject Sprechfeld;
    
    private float timer;
    private Transform ThrowingObject;

    void Start()
    {
        Vector2 position = new Vector2(Random.Range(-25.0f, 25.0f), 14.3f);
        Instantiate(Sprechfeld, position, Quaternion.identity);
        ThrowingObject = GameObject.Find("shoutingMonkey").transform;
    }

    void Update()
    {
        float randTimer = Random.Range(3f, 10f);
        Vector2 position;
        if (ThrowingObject == null)
            position = new Vector2(Random.Range(-15.0f, 15.0f), 14.3f);
        else
            position = ThrowingObject.position;
        if (timer > randTimer)
        {
            timer = 0;

            float randNumber = Random.Range(0.0f, 100.0f);
            Instantiate(Sprechfeld, position, Quaternion.identity);
        }

        timer += Time.deltaTime;
    }
}
