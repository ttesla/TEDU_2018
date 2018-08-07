using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CoRoutineTests : MonoBehaviour
{
	void Start ()
    {
        Invoke("Test", 2.0f);
        InvokeRepeating("TestRepeat", 1.0f, 1.0f);

        var mCor = StartCoroutine(TestCorotuine(1));

        //StopCoroutine(mCor);
    }

    void Test()
    {
        Debug.Log("Test");
    }

    void TestRepeat()
    {
        Debug.Log("Test Repeaet");
    }

    IEnumerator TestCorotuine(int a)
    {
        Debug.Log("Coroutinee1");
        yield return new WaitForSeconds(2.0f);

        Debug.Log("Coroutinee2");

        while (true)
        {
            Debug.Log("Coroutinee3: " + a);
            yield return new WaitForSeconds(1.0f);
        }
    }

}
