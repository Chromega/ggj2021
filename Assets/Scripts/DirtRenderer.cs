using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtRenderer : MonoBehaviour
{
    Texture2D myTex;
    int size = 256;
    new Collider collider;

    bool wasRaking = false;
    Vector2 lastRakingPosNormalized;
    Vector2Int lastRakingPos;
    float lastRakingAngle;
    float numRakePoints = 4;
    float rakePixelGap = 4;

    // Start is called before the first frame update
    void Start()
    {
        myTex = new Texture2D(size, size);

        GetComponent<MeshRenderer>().material.SetTexture("Texture2D_b4158f584f96400e94a95969559d0450", myTex);

        collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        //myTex.SetPixel(Random.Range(0, 128), Random.Range(0, 128), Color.black);
        Vector2 pos;
        float angle;
        if (GetNormalizedRakePosition(out pos, out angle))
        {
            lastRakingPosNormalized = pos;
            int endPx = Mathf.RoundToInt(size * pos.x);
            int endPy = Mathf.RoundToInt(size * pos.y);

            int startPx = wasRaking ? lastRakingPos.x : endPx;
            int startPy = wasRaking ? lastRakingPos.y : endPy;

            //White out old zone
            for (int offset = (int)(-(numRakePoints - 1) / 2.0f); offset < (int)(-(numRakePoints - 1) / 2.0f + numRakePoints); ++offset)
            {
                float offsetMag = offset*rakePixelGap;

                int tineStartPx = (int)(offsetMag * Mathf.Cos(lastRakingAngle + Mathf.PI / 2) + startPx);
                int tineStartPy = (int)(offsetMag * Mathf.Sin(lastRakingAngle + Mathf.PI / 2) + startPy);
                int tineEndPx = (int)(offsetMag * Mathf.Cos(angle + Mathf.PI / 2) + endPx);
                int tineEndPy = (int)(offsetMag * Mathf.Sin(angle + Mathf.PI / 2) + endPy);

                foreach (Vector2Int p in GetPointsOnLine(tineStartPx, tineStartPy, tineEndPx, tineEndPy))
                {
                    if (p.x >= 0 && p.x < size && p.y >= 0 && p.y < size)
                    {
                        myTex.SetPixel(p.x, p.y, Color.white);
                    }
                }
            }


            for (int i = 0; i < numRakePoints; ++i)
            {
                float offsetMag = (-(numRakePoints - 1) / 2.0f + i) * rakePixelGap;

                int tineStartPx = (int)(offsetMag * Mathf.Cos(lastRakingAngle+Mathf.PI/2) + startPx);
                int tineStartPy = (int)(offsetMag * Mathf.Sin(lastRakingAngle + Mathf.PI / 2) + startPy);
                int tineEndPx = (int)(offsetMag * Mathf.Cos(angle + Mathf.PI / 2) + endPx);
                int tineEndPy = (int)(offsetMag * Mathf.Sin(angle + Mathf.PI / 2) + endPy);

                foreach (Vector2Int p in GetPointsOnLine(tineStartPx, tineStartPy, tineEndPx, tineEndPy))
                {
                    if (p.x >= 0 && p.x < size && p.y >= 0 && p.y < size)
                    {
                        myTex.SetPixel(p.x, p.y, Color.black);
                    }
                }
            }
            


            lastRakingPos.x = endPx;
            lastRakingPos.y = endPy;
            lastRakingAngle = angle;
            myTex.Apply();
            wasRaking = true;
        }
        else
        {
            wasRaking = false;
        }
    }

    bool GetNormalizedRakePosition(out Vector2 pos, out float angle)
    {
        pos = Vector2.zero;
        angle = 0;

        if (!RakeController.Instance || !RakeController.Instance.IsRaking())
        {
            return false;
        }

        Ray ray = new Ray(RakeController.Instance.transform.position+.1f*Vector3.up, Vector3.down);
        RaycastHit hitInfo;

        if (collider.Raycast(ray, out hitInfo, 999999f))
        {
            pos = new Vector2(hitInfo.textureCoord.x, hitInfo.textureCoord.y);
            /*float deltaY = pos.y - lastRakingPosNormalized.y;
            float deltaX = pos.x - lastRakingPosNormalized.x;
            if (deltaX == 0 && deltaY == 0)
                angle = lastRakingAngle;
            else
                angle = Mathf.Atan2(deltaY, deltaX);*/
            Vector3 direction = RakeController.Instance.rakeRoot.transform.right;
            direction.y = 0;
            direction.Normalize();

            angle = Mathf.Atan2(direction.z, direction.x);
            return true;
        }
        else
        {
            return false;
        }
    }

    bool GetNormalizedCursorPosition(out Vector2 pos, out float angle)
    {
        pos = Vector2.zero;
        angle = 0;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (collider.Raycast(ray, out hitInfo, 999999f))
        {
            pos = new Vector2(hitInfo.textureCoord.x, hitInfo.textureCoord.y);
            float deltaY = pos.y - lastRakingPosNormalized.y;
            float deltaX = pos.x - lastRakingPosNormalized.x;
            if (deltaX == 0 && deltaY == 0)
                angle = lastRakingAngle;
            else
                angle = Mathf.Atan2(deltaY, deltaX);
            return true;
        }
        else
        {
            return false;
        }
    }

    //http://ericw.ca/notes/bresenhams-line-algorithm-in-csharp.html
    public static IEnumerable<Vector2Int> GetPointsOnLine(int x0, int y0, int x1, int y1)
    {
        if (x0 == x1 && y0 == y1)
        {
            yield return new Vector2Int(x0, y0);
            yield break;
        }

        bool steep = Mathf.Abs(y1 - y0) > Mathf.Abs(x1 - x0);
        if (steep)
        {
            int t;
            t = x0; // swap x0 and y0
            x0 = y0;
            y0 = t;
            t = x1; // swap x1 and y1
            x1 = y1;
            y1 = t;
        }
        if (x0 > x1)
        {
            int t;
            t = x0; // swap x0 and x1
            x0 = x1;
            x1 = t;
            t = y0; // swap y0 and y1
            y0 = y1;
            y1 = t;
        }
        int dx = x1 - x0;
        int dy = Mathf.Abs(y1 - y0);
        int error = dx / 2;
        int ystep = (y0 < y1) ? 1 : -1;
        int y = y0;
        for (int x = x0; x <= x1; x++)
        {
            yield return new Vector2Int((steep ? y : x), (steep ? x : y));
            error = error - dy;
            if (error < 0)
            {
                y += ystep;
                error += dx;
            }
        }
        yield break;
    }
}
