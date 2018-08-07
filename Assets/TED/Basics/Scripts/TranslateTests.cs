using UnityEngine;
using System.Collections;

public enum TranslateTestType
{
    BasicTranslate, 
    SimpleLerp,
    AnimationCurveLerp,
}

public class TranslateTests : MonoBehaviour
{
    public AnimationCurve Curve;
    public float TimeScale;
    public TranslateTestType TestType;

    public Transform TargetTrans;
    private Vector3 mDefaultPos;
    private float t;

    private Animator mAnimator;

	// Use this for initialization
	void Start ()
    {
        mDefaultPos = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        switch (TestType)
        {
            case TranslateTestType.BasicTranslate:
                BasicTranslate();
                break;
            case TranslateTestType.SimpleLerp:
                SimpleLerpUpdate();
                break;
            case TranslateTestType.AnimationCurveLerp:
                AnimationCurveUpdate();
                break;
        }

        // Timer 
        t += Time.deltaTime * TimeScale;

        if (t > 1.0f)
        {
            t = 0.0f;
        }
    }

    private void AnimationCurveUpdate()
    {
        float curvedTime = Curve.Evaluate(t);
        transform.position = Vector3.Lerp(mDefaultPos, TargetTrans.position, curvedTime);
        
    }

    private void SimpleLerpUpdate()
    {
        transform.position = Vector3.Lerp(mDefaultPos, TargetTrans.position, t);
    }

    private void BasicTranslate()
    {
        transform.position += Vector3.right * TimeScale * Time.deltaTime;

        // Hedefe ulaşınca başa al
        if(transform.position.x > TargetTrans.position.x)
        {
            transform.position = mDefaultPos;
        }
    }
}
