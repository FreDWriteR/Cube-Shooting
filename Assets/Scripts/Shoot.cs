using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shoot : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 direction = new Vector3(0f, 0f, 1f);
    private Vector3 StartPosition;
    private bool EndSpawn = true;
    public float Speed = 0f, distance = 0f, SecPerSpawn = 0f;
    private GameObject BulletCube;
    private IEnumerator coroutine;
    public TMP_InputField TMSpeed, TMDistanse, TMTimePerSec;


    void GetParams()
    {
        Speed = float.Parse(TMSpeed.text);
        distance = float.Parse(TMDistanse.text);
        SecPerSpawn = float.Parse(TMTimePerSec.text);
    }

    void Start()
    {
        if (TMSpeed.text != "" && TMDistanse.text != "" && TMTimePerSec.text != "" &&
            float.TryParse(TMSpeed.text, out Speed) && float.TryParse(TMDistanse.text, out distance) && float.TryParse(TMTimePerSec.text, out SecPerSpawn))
        {
            if (gameObject.name != "CubeBase")
            {
                StartPosition = new Vector3(0.14f, 1f, -6.8f);
                gameObject.transform.position = new Vector3(0.14f, 1f, -6.8f);
            }
        }
    }

    IEnumerator SpawmCube()
    {
        yield return new WaitForSeconds(SecPerSpawn);
        BulletCube = Instantiate(gameObject, StartPosition, Quaternion.identity);
        EndSpawn = true;
    }

    void FixedUpdate()
    {
        if (TMSpeed.text != "" && TMDistanse.text != "" && TMTimePerSec.text != "" &&
            float.TryParse(TMSpeed.text, out Speed) && float.TryParse(TMDistanse.text, out distance) && float.TryParse(TMTimePerSec.text, out SecPerSpawn))
        {
            if (gameObject.name != "CubeBase")
            {
                gameObject.GetComponent<Rigidbody>().AddForce(direction * Speed, ForceMode.VelocityChange);
                // Ограничение скорости, иначе объект будет постоянно ускоряться
                if (Mathf.Abs(gameObject.GetComponent<Rigidbody>().velocity.z) > Speed)
                {
                    gameObject.GetComponent<Rigidbody>().velocity = new Vector3(gameObject.GetComponent<Rigidbody>().velocity.x, gameObject.GetComponent<Rigidbody>().velocity.y, Mathf.Sign(gameObject.GetComponent<Rigidbody>().velocity.z) * Speed);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (TMSpeed.text != "" && TMDistanse.text != "" && TMTimePerSec.text != "" &&
            float.TryParse(TMSpeed.text, out Speed) && float.TryParse(TMDistanse.text, out distance) && float.TryParse(TMTimePerSec.text, out SecPerSpawn))
        {
            if (gameObject.transform.position.z - StartPosition.z > distance && gameObject.name != "CubeBase")
            {
                Destroy(gameObject);
            }
            if (gameObject.name == "CubeBase")
            {
                if (EndSpawn) //Если прошло необходимое время между спаунами
                {
                    EndSpawn = false;
                    coroutine = SpawmCube();
                    StartCoroutine(coroutine);
                }
            }
        }
    }
}
