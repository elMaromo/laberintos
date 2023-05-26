using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public int minXsize, minYsize, minZsize, maxXsize, maxYsize, maxZsize;
    public float timeToCreateLab, timeToCreatePath;
    private HasAI hasAi;
    public GameObject labObject, pathObject, ball, cam;
    [Range(0, 1)] public float alphaOfWalls;

    private CreateLaberit lab;
    private FindPath path;
    private AIlaberinto ai;

    private void Awake()
    {
        hasAi = GameObject.FindGameObjectsWithTag("AIManager")[0].GetComponent<HasAI>();
        path = pathObject.GetComponent<FindPath>();
        lab = labObject.GetComponent<CreateLaberit>();
        ai = GetComponent<AIlaberinto>();

        ai.activated = false;
        ai.path = path;

        RestartLab();
        ball = Instantiate(ball, transform.position, transform.rotation);

        ai.ball = ball;
        ai.rbBall = ball.GetComponent<Rigidbody>();

        StartCoroutine(VerBonito());
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
        Vector3 cameraPos = cam.transform.position;
        cameraPos.z = +(lab.tamZ) / 2;
        if (lab.tamZ % 2 == 0)
        {
            cameraPos.z -= 0.5f;
        }
        cam.transform.position = cameraPos;
        lab.CreateLab();
        path.findPath(lab, lab.casillas[0, 0, 0], transform);
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
        path.findPath(lab, lab.casillas[(int)corC.x, (int)corC.y, (int)corC.z], transform);
    }

    Vector3 CasillaMasCerc()
    {
        float minDist = Vector3.Distance(ball.transform.position, lab.casillas[0, 0, 0].transform.position);
        Vector3 coordsCasilla = Vector3.zero;
        for (int i = 0; i < lab.tamX; i++)
        {
            for (int j = 0; j < lab.tamY; j++)
            {
                for (int k = 0; k < lab.tamZ; k++)
                {
                    if (minDist > Vector3.Distance(ball.transform.position, lab.casillas[i, j, k].transform.position))
                    {
                        coordsCasilla.x = i;
                        coordsCasilla.y = j;
                        coordsCasilla.z = k;
                        minDist = Vector3.Distance(ball.transform.position, lab.casillas[i, j, k].transform.position);
                    }
                }
            }
        }

        return coordsCasilla;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ai.activated = !ai.activated;
            hasAi.hasAI = ai.activated;
        }
    }

    private IEnumerator VerBonito()
    {
        path.DeactivatePath();
        StartCoroutine(lab.VerBonito(timeToCreateLab));
        yield return new WaitForSeconds(timeToCreateLab);
        StartCoroutine(path.VerBonito(timeToCreatePath));
        yield return new WaitForSeconds(timeToCreatePath);
        InvokeRepeating(nameof(ResetPath), 0.333f, 0.333f);
        ai.activated = hasAi.hasAI;
    }
}




