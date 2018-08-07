using UnityEngine;
using System.Collections;

public class WallMaker : MonoBehaviour
{
    public Vector3 StartPos;
    public int WidthCount;
    public int HeightCount;
    public GameObject BrickPrefab;

	// Use this for initialization
	void Start ()
    {
        MakeWall();
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    private void MakeWall()
    {
        GameObject wallObject = new GameObject("Wall");
        Vector3 brickPos  = Vector3.zero;
        float BrickWidth  = BrickPrefab.transform.localScale.x;
        float BrickHeight = BrickPrefab.transform.localScale.y;
        float ShiftWidth  = BrickWidth / 2.0f;
        float shiftVal = 0;

        for (int y = 0; y < HeightCount; y++)
        {
            for (int x = 0; x < WidthCount; x++)
            {
                if(y % 2 == 0)
                {
                    shiftVal = 0;
                }
                else 
                {
                    shiftVal = ShiftWidth;
                }

                //shiftVal = y % 2 == 0 ? 0 : ShiftWidth;

                brickPos = new Vector3(x * BrickWidth + shiftVal, y * BrickHeight, 0);
                GameObject.Instantiate(BrickPrefab, brickPos, Quaternion.identity, wallObject.transform);
            }
        }

        wallObject.transform.position = StartPos;
    }
}
