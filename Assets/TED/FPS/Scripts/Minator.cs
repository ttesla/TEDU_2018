using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Minator : MonoBehaviour
{
    private Transform mPlayerTransform;
    private Animator mAnimator;
    private NavMeshAgent mAgent;

	// Use this for initialization
	void Start ()
    {
        mPlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        mAgent = GetComponent<NavMeshAgent>();
        mAnimator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        mAgent.SetDestination(mPlayerTransform.position);
        mAnimator.SetFloat("Speed", mAgent.velocity.magnitude);
        mAnimator.SetFloat("SpeedMultiplier", mAgent.speed / 3.5f);
    }

    /// <summary>
    /// Bu metodu, animation event çağırıyor, içerisine ayak sesi eklenebilir.
    /// </summary>
    private void PlayFootStep()
    {

    }
}
