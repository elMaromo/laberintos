using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindPath : MonoBehaviour
{
    public GameObject pathCube;
    private CreateLaberit labe;
    [HideInInspector] public PathScript lastPath;
    public List<GameObject> pathList;
    private Transform parentRotation;

    private void DestroyPathList()
    {
        if (pathList == null)
        {
            pathList = new List<GameObject>();
        }

        for (int i = 0; i < pathList.Count; i++)
        {
            Destroy(pathList[i]);
        }
        pathList.Clear();
    }

    public void findPath(CreateLaberit lab, CasillaScript start, Transform newParentRotation)
    {
        parentRotation = newParentRotation;
        DestroyPathList();
        labe = lab;
        List<CasillaScript> sol = new List<CasillaScript>();
        sol.Add(start);
        sol = findPathR(sol);
        if (pathList.Count > 1)
        {
            pathList[0].SetActive(true);
        }
        drawCubes(sol);
    }

    List<CasillaScript> findPathR(List<CasillaScript> currSol)
    {
        List<Transition> viableTransitions = getViableTransitions(currSol);
        //Debug.Log(viableTransitions.Count);


        for (int i = 0; i < viableTransitions.Count; i++)
        {
            if (currSol[currSol.Count - 1] != viableTransitions[i].casA)
            {
                currSol.Add(viableTransitions[i].casA);
            }
            else
            {
                currSol.Add(viableTransitions[i].casB);
            }

            if (currSol[currSol.Count - 1] == labe.casillas[labe.tamX - 1, labe.tamY - 1, labe.tamZ - 1])
            {
                return currSol;
            }

            int currentNodes = currSol.Count;
            currSol = findPathR(currSol);
            if (currSol.Count == currentNodes)
            {
                currSol.RemoveAt(currSol.Count - 1);
            }
        }

        return currSol;
    }

    List<Transition> getViableTransitions(List<CasillaScript> currSol)
    {
        List<Transition> listTr = new List<Transition>();

        for (int i = 0; i < labe.transitionsSol.Count; i++)
        {
            if (labe.transitionsSol[i].casA == currSol[currSol.Count - 1])
            {
                if (!currSol.Contains(labe.transitionsSol[i].casB))
                {
                    listTr.Add(labe.transitionsSol[i]);
                }
            }
            else if (labe.transitionsSol[i].casB == currSol[currSol.Count - 1])
            {
                if (!currSol.Contains(labe.transitionsSol[i].casA))
                {
                    listTr.Add(labe.transitionsSol[i]);
                }
            }
        }


        return listTr;
    }

    public void drawCubes(List<CasillaScript> sol)
    {
        for (int i = 0; i < sol.Count; i++)
        {
            GameObject newCube = Instantiate(pathCube, parentRotation.position, parentRotation.rotation);
            newCube.transform.Translate(sol[i].posX, sol[i].posY, sol[i].posZ);
            newCube.transform.parent = gameObject.transform;
            pathList.Add(newCube);
            //newCube.transform.eulerAngles = new Vector3(35, 0, 45);

            if (i == sol.Count - 1)
            {
                newCube.transform.localScale += Vector3.one;
                //newCube.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                lastPath = newCube.GetComponent<PathScript>();
            }

            if( pathList.Count == 1)
            {
                pathList[0].SetActive(false);
            }
            
        }
    }


    public IEnumerator VerBonito(float timeToWait)
    {
        float timePerCas = timeToWait / (float)pathList.Count;
        for (int i = 0; i < pathList.Count; i++)
        {
            pathList[i].SetActive(true);
            yield return new WaitForSeconds(timePerCas);
        }
    }

    public void DeactivatePath()
    {
        for (int i = 0; i < pathList.Count; i++)
        {
            pathList[i].SetActive(false);
        }
    }
}
