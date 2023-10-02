using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Attach this to a Fighter in order to direct it without player input
public class FighterAI : MonoBehaviour
{
    public enum State
    {
        Idle,           //the fighter doesn't do anything
        Aimless,        //the fighter wanders without intention
        Aggressive,     //the fighter attempts to knock opponents off the board
        Defensive,      //the fighter attempts to avoid falling to its death
        Strategic       //the fighter attempts to reach a power up
    };

    FighterMovement fighter;
    State state = State.Idle;
    Vector3 inputSim = Vector3.zero;
    Coroutine waitRoutine;
    VectorMaker vm;

    private void Awake()
    {
        fighter = GetComponent<FighterMovement>();
        vm = new VectorMaker();
        EnterState();
    }

    private void FixedUpdate()
    {
        switch (state)
        {
            case State.Aimless:
                inputSim = vm.RandomYRotation(inputSim, -60, 60);
                break;
            default:
                break;
        }
        fighter.SetMovement(inputSim);
    }

    public void SetState(State s)
    {
        if (state == s)
            return;
        LeaveState();
        state = s;
        EnterState();
    }

    //cleans up whatever needs to be handled when leaving the current state
    private void LeaveState()
    {

    }

    //initializes whatever needs to be handled when entering the current state
    private void EnterState()
    {
        float minWaitTime;
        float maxWaitTime;
        switch (state)
        {
            case State.Aimless:
                minWaitTime = 0.1f;
                maxWaitTime = 2f;
                inputSim = vm.RandomHorizontalDirection();
                break;
            default:
            case State.Idle:
                minWaitTime = 0.5f;
                maxWaitTime = 3f;
                inputSim = Vector3.zero;
                break;
        }
        if (waitRoutine != null)
            StopCoroutine(waitRoutine);
        waitRoutine = StartCoroutine(WaitRoutine(Random.Range(minWaitTime, maxWaitTime)));
    }

    //waits a certain amount of time and then switches the AI state
    private IEnumerator WaitRoutine(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        State newState;
        switch (state)
        {
            case State.Idle:
                newState = State.Aimless;
                break;
            case State.Aimless:
            default:
                newState = State.Idle;
                break;
        }
        waitRoutine = null;
        SetState(newState);
    }

    
}
