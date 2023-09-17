using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleTurbo : Command
{
    private BikeController _Controller;

    public ToggleTurbo(BikeController controller)
    {
        _Controller = controller;
    }

    public override void Execute()
    {
        _Controller.ToggleTurbo(); 
    }
}
