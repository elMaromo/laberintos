using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIlaberinto : MonoBehaviour
{
    public float speed;
    [HideInInspector] public FindPath path;
    [HideInInspector] public GameObject ball;
    public bool activated;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        if (activated)
        {
            Vector3 pahMasCerc = CasMasCerc();

            Vector3 targetDir = pahMasCerc - ball.transform.position;
            float angle = Vector3.Angle(targetDir, Vector3.down);
            Vector3 perpendicularVector = Vector3.Cross(targetDir, Vector3.down);

            rb.AddTorque(perpendicularVector * speed * Time.deltaTime);
        }
    }

    public Vector3 CasMasCerc()
    {
        Vector3 target = Vector3.zero;

        if ((path.pathList.Count > 1))
        {
            target = path.pathList[1].transform.position;
        }

        if ((path.pathList.Count > 2))
        {
            target.x = target.x + path.pathList[1].transform.position.x;
            target.x = target.x / 2;
        }

        //if ((path.pathList.Count > 3))
        //{
        //    target.x = target.x + path.pathList[3].transform.position.x;
        //    target.x = target.x / 2;
        //}

        return target;
    }
}
