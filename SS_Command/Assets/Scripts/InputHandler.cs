using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    //The primary responsibility of this script/class is to
    //  listen for the player's inputs and invoke the appropriate
    //  commands.

    //In this section we are initializing our commands and mapping 
    //  them to specific inputs. 
    //Note that our InputHandler is only aware that BikeController
    //  exists, but does not need to know how it functions.

    //VARIABLES
    private Invoker _invoker;
    private bool _isReplaying;
    private bool _isRecording;
    private BikeController _bikeController;
    private Command _buttonA, _buttonD, _buttonW;

    void Start()
    {
        _invoker = gameObject.AddComponent<Invoker>();
        _bikeController = FindObjectOfType<BikeController>();

        _buttonA = new TurnLeft(_bikeController);
        _buttonD = new TurnRight(_bikeController);
        _buttonW = new ToggleTurbo(_bikeController); 
    }

    void Update()
    {
        if(!_isReplaying && !_isRecording)
        {
            if(Input.GetKeyUp(KeyCode.A))
            {
                _invoker.ExecuteCommand(_buttonA); 
            }
            if(Input.GetKeyUp(KeyCode.D))
            {
                _invoker.ExecuteCommand(_buttonD);
            }
            if(Input.GetKeyUp(KeyCode.W))
            {
                _invoker.ExecuteCommand(_buttonW); 
            }
        }
    }

    void OnGUI()
    {
        if(GUILayout.Button("Start Recording"))
        {
            _bikeController.ResetPosition();
            _isReplaying = false;
            _isRecording = true;
            _invoker.Record(); 
        }
        if(GUILayout.Button("Stop Recording"))
        {
            _bikeController.ResetPosition();
            _isRecording = false; 
        }
        if(!_isRecording)
        {
            if(GUILayout.Button("Start Replay"))
            {
                _bikeController.ResetPosition();
                _isRecording = false;
                _isReplaying = true;
                _invoker.Replay(); 
            }
        }
    }
}
