using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    
    public int minXsize, minYsize, minZsize, maxXsize, maxYsize, maxZsize;
    public GameObject labObject, pathObject, ball, camera;
    [Range(0, 1)] public float alphaOfWalls;

    private CreateLaberit lab;
    private FindPath path;
    private AIlaberinto ai;

    private void Awake()
    {
        path = pathObject.GetComponent<FindPath>();
        lab = labObject.GetComponent<CreateLaberit>();
        ai = GetComponent<AIlaberinto>();
        
        ai.activated = false;
        ai.path = path;
        
        RestartLab();
        ball = Instantiate(ball, transform.position, transform.rotation);

        ai.ball = ball;
        ai.rbBall = ball.GetComponent<Rigidbody>();
        InvokeRepeating( nameof(ResetPath), 0.333f, 0.333f);
    }

    private void LateUpdate()
    {
        if (path.pathList.Count == 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void RestartLab()
    {
        RandomizeSize();
        Vector3 cameraPos = camera.transform.position;
        cameraPos.z =+ (lab.tamZ)/2;
        if( lab.tamZ%2 == 0)
        {
            cameraPos.z -= 0.5f;
        }
        camera.transform.position = cameraPos;
        lab.CreateLab();
        path.findPath(lab, lab.casillas[0,0,0], transform);
    }


    private void RandomizeSize()
    {
        long milliseconds = System.DateTime.Now.Ticks / System.TimeSpan.TicksPerMillisecond;
        Random.seed = (int)milliseconds;
        lab.tamX = Random.Range(minXsize, maxXsize);
        lab.tamY = Random.Range(minYsize, maxYsize);
        lab.tamZ = Random.Range(minZsize, maxZsize);
    }

    private void ResetPath()
    {
        Vector3 corC = CasillaMasCerc();
        path.findPath(lab, lab.casillas[(int)corC.x,(int)corC.y,(int)corC.z], transform);
    }

    Vector3 CasillaMasCerc()
    {
        float minDist = Vector3.Distance( ball.transform.position, lab.casillas[0,0,0].transform.position);
        Vector3 coordsCasilla = Vector3.zero;
        for (int i = 0; i < lab.tamX; i++)
        {
            for (int j = 0; j < lab.tamY; j++)
            {
                for (int k = 0; k < lab.tamZ; k++)
                {
                    if( minDist> Vector3.Distance( ball.transform.position, lab.casillas[i,j,k].transform.position) )
                    {
                        coordsCasilla.x = i;
                        coordsCasilla.y = j;
                        coordsCasilla.z = k;
                        minDist = Vector3.Distance( ball.transform.position, lab.casillas[i,j,k].transform.position);
                    }
                }
            }
        }

        return coordsCasilla;
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            ai.activated = !ai.activated;
        }
    }
}
