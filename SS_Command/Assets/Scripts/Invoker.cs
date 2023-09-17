using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

class Invoker : MonoBehaviour
{
    //Once we have encapsulated each of the commands into 
    //  individual classes, we can now code the critical ingredient
    //  that will make our replay work...this Invoker.

    //Invoker is an attentive bookkeeper, where it keeps track of 
    //  the commands that have been executed in a ledger.

    //VARIABLES
    private bool _isRecording;
    private bool _isReplaying;
    private float _replayTime;
    private float _recordingTime;
    private SortedList<float, Command> _recordedCommands = new SortedList<float, Command>(); 

    //PART ONE
    //We are adding a command to the _recorderCommands sorted 
    //  list every time Invoker executes a new one. 
    public void ExecuteCommand(Command command)
    {
        command.Execute(); 

        if(_isRecording)
        {
            _recordedCommands.Add(_recordingTime, command);
        }

        Debug.Log("Recorded Time: " + _recordingTime);
        Debug.Log("Recorded Command: " + command);
    }

    public void Record()
    {
        _recordingTime = 0.0f;
        _isRecording = true; 
    }

    //PART TWO
    //In this section we are going to implement our replay behavior

    //We are using FixedUpdate() to record and replay commands.
    //Normally we usually use Update() but with FixedUpdate() has 
    //  the advantage of running in fixed time step and is helpful for
    //  time dependent but frame rate independent tasks. 
   

    public void Replay()
    {
        _replayTime = 0.0f;
        _isReplaying = true;

        if(_recordedCommands.Count <= 0)
        {
            Debug.LogError("No commands to replay!");
        }

        _recordedCommands.Reverse(); 
    }

    void FixedUpdate()
    {
        if(_isRecording)
        {
            _recordingTime += Time.fixedDeltaTime; 
        }

        if(_isReplaying)
        {
            _replayTime += Time.deltaTime;

            if (_recordedCommands.Any())
            {
                if (Mathf.Approximately(_replayTime, _recordedCommands.Keys[0]))
                {
                    Debug.Log("Replay Time: " + _replayTime);
                    Debug.Log("Replay Command: " + _recordedCommands.Values[0]);

                    _recordedCommands.Values[0].Execute();
                    _recordedCommands.Remove(0); 
                }
            }
            else
            {
                _isReplaying = false; 
            }
        }
    }

}
