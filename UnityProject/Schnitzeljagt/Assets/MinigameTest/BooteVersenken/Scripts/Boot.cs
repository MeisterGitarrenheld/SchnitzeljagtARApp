using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boot : MonoBehaviour {


    public float MoveSpeed;
    [HideInInspector]
    private bool CanBeHit;

    public Sprite Good1;
    public Sprite Good2;
    public Sprite Bad1;
    public Sprite Bad2;
    public float BadPercentage;

    private SpriteRenderer sr;

    void Start () {
        Destroy(gameObject, 30);
        sr = GetComponent<SpriteRenderer>();
        if(Random.Range(0.0f, 100.0f) > BadPercentage)
        {
            CanBeHit = false;
            if (Random.Range(0.0f, 100.0f) > 50.0f)
            {
                sr.sprite = Bad1;
            }
            else
                sr.sprite = Bad2;
        }
        else
        {
            CanBeHit = true;
            if (Random.Range(0.0f, 100.0f) > 50.0f)
            {
                sr.sprite = Good1;
            }
            else
                sr.sprite = Good2;
        }
	}
	
	void Update () {
        transform.position += Vector3.right * Time.deltaTime * MoveSpeed;
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Rock"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
            BoatGameMaster.Instance.BoatHit(CanBeHit? 100 + (int)transform.position.z :-100);
        }
    }
}
