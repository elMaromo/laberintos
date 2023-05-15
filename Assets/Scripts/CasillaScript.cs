// desarrollado en el master de programacion de videojuegos de la uma marzo 2023
// Juan Hern�ndez Mart�n

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasillaScript : MonoBehaviour
{
    public GameObject xMor, xLes, zMor, zLes, yMor, yLes;
    [HideInInspector] public int posX, posY, posZ;

    public void DestroyWallNextTo(CasillaScript otherCasilla)
    {
        if (posX != otherCasilla.posX)
        {
            if (posX < otherCasilla.posX)
            {
                //xMor.SetActive(false);
                Destroy(xMor);
            }
            else
            {
                //xLes.SetActive(false);
                Destroy(xLes);
            }
        }

        if (posY != otherCasilla.posY)
        {
            if (posY < otherCasilla.posY)
            {
                //yMor.SetActive(false);
                Destroy(yMor);
            }
            else
            {
                //yLes.SetActive(false);
                Destroy(yLes);
            }
        }

        if (posZ != otherCasilla.posZ)
        {
            if (posZ < otherCasilla.posZ)
            {
                //zMor.SetActive(false);
                Destroy(zMor);
            }
            else
            {
                //zLes.SetActive(false);
                Destroy(zLes);
            }
        }
    }
    public void removeOutsideWalls( int tamX, int tamY, int tamZ, float newAlpha )
    {
        Color newColor = xLes.GetComponent<Renderer>().material.color;
        newColor.a = newAlpha;

        if ( posX== 0)
        {
            xLes.GetComponent<Renderer>().material.color = newColor;
        }

        if (posY == 0)
        {
            yLes.GetComponent<Renderer>().material.color = newColor;
        }

        if (posZ == 0)
        {
            zLes.GetComponent<Renderer>().material.color = newColor;
        }

        if (posX == tamX-1)
        {
            xMor.GetComponent<Renderer>().material.color = newColor;
        }

        if (posY == tamY-1)
        {
            yMor.GetComponent<Renderer>().material.color = newColor;
        }

        if (posZ == tamZ-1)
        {
            zMor.GetComponent<Renderer>().material.color = newColor;
        }
    }

    public void colorate(int tamX, int tamY, int tamZ)
    {
        float red = ((float)posX / (float)tamX);
        float green = ((float)posY / (float)tamY);
        float blue = ((float)posZ / (float)tamZ);

        Color newColor = new Color(red, green, blue);

        xLes.GetComponent<Renderer>().material.color = newColor;
        yLes.GetComponent<Renderer>().material.color = newColor;
        zLes.GetComponent<Renderer>().material.color = newColor;
        xMor.GetComponent<Renderer>().material.color = newColor;
        yMor.GetComponent<Renderer>().material.color = newColor;
        zMor.GetComponent<Renderer>().material.color = newColor;
    }
}
