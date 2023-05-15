// desarrollado en el master de programacion de videojuegos de la uma marzo 2023
// Juan Hern�ndez Mart�n

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLaberit : MonoBehaviour
{
    public GameObject casilla;
    public float alphaOfWalls;
    
    [HideInInspector] public int tamX, tamY, tamZ;
    [HideInInspector] public List<List<CasillaScript>> solutions;
    [HideInInspector] public List<Transition> transitions;
    [HideInInspector] public CasillaScript[,,] casillas;
    [HideInInspector] public List<Transition> transitionsSol;

    public void InitializeCas()
    {
        casillas = new CasillaScript[tamX, tamY, tamZ];
        solutions = new List<List<CasillaScript>>();

        for (int i = 0; i < tamX; i++)
        {
            for (int j = 0; j < tamY; j++)
            {
                for (int k = 0; k < tamZ; k++)
                {
                    Vector3 newCasPos = transform.position;
                    newCasPos.x += i;
                    newCasPos.y += j;
                    newCasPos.z += k;
                    GameObject newCas = Instantiate(casilla, newCasPos, transform.rotation);
                    newCas.transform.SetParent(transform);
                    CasillaScript newCasScrip = newCas.GetComponent<CasillaScript>();
                    newCasScrip.posX = i;
                    newCasScrip.posY = j;
                    newCasScrip.posZ = k;

                    newCasScrip.colorate(tamX, tamY, tamZ);
                    newCasScrip.removeOutsideWalls(tamX, tamY, tamZ, alphaOfWalls);

                    casillas[i, j, k] = newCasScrip;
                    List<CasillaScript> newForest = new List<CasillaScript>();
                    newForest.Add(newCasScrip);
                    solutions.Add(newForest);
                }
            }
        }
    }

    public void InitializeTransitions()
    {
        transitionsSol = new List<Transition>();
        transitions = new List<Transition>();

        for (int i = 0; i < tamX; i++)
        {
            for (int j = 0; j < tamY; j++)
            {
                for (int k = 0; k < tamZ; k++)
                {
                    if (i != 0)
                    {
                        transitions.Add(new Transition(casillas[i-1, j, k], casillas[i, j, k]));
                    }

                    if (j != 0)
                    {
                        transitions.Add(new Transition(casillas[i, j-1, k], casillas[i, j, k]));
                    }

                    if (k != 0)
                    {
                        transitions.Add(new Transition(casillas[i, j, k-1], casillas[i, j, k]));
                    }
                }
            }
        }
    }

    public void CreateLab()
    {
        InitializeCas();
        InitializeTransitions();
        while( solutions.Count > 1 )
        {
            IterateSolution();
        }
    }

    public void IterateSolution()
    {
        bool succes = false;
        while (succes == false)
        {
            int nextTrans = Random.Range(0, transitions.Count);
            if (nextTransitionValid(transitions[nextTrans]))
            {
                transitionsSol.Add(transitions[nextTrans]);
                succes = true;
                transitions[nextTrans].Activate();
                joinSolutions(getFirstSol(transitions[nextTrans]), getSecondSol(transitions[nextTrans]));
            }
            transitions.RemoveAt(nextTrans);
        }
    }

    private void joinSolutions(List<CasillaScript> firstSol, List<CasillaScript> secondSol)
    {
        for (int i = 0; i < secondSol.Count; i++)
        {
            firstSol.Add(secondSol[i]);
        }

        solutions.Remove(secondSol);
    }

    private List<CasillaScript> getFirstSol(Transition currTrans)
    {
        for (int i = 0; i < solutions.Count; i++)
        {
            if(solutions[i].Contains(currTrans.casA))
            {
                return solutions[i];
            }
        }

        return null;
    }

    private List<CasillaScript> getSecondSol(Transition currTrans)
    {
        for (int i = 0; i < solutions.Count; i++)
        {
            if (solutions[i].Contains(currTrans.casB))
            {
                return solutions[i];
            }
        }

        return null;
    }

    private bool nextTransitionValid(Transition currTrans)
    {
        if (getFirstSol(currTrans).Contains(currTrans.casB))
        {
            return false;
        }

        if (getSecondSol(currTrans).Contains(currTrans.casA))
        {
            return false;
        }

        return true;
    }
}

public class Transition
{
    public CasillaScript casA, casB;
    public Transition(CasillaScript casAIN, CasillaScript casBIN)
    {
        casA = casAIN;
        casB = casBIN;
    }

    public void Activate()
    {
        casA.DestroyWallNextTo(casB);
        casB.DestroyWallNextTo(casA);
    }
}
