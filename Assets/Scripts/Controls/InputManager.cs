using Godot;
using System;
using System.Collections.Generic;

public enum ButtonInput {None, Direction1, Direction2, Direction3, Direction4, Direction5, Direction6, Direction7, Direction8, Direction9, Attack1, Attack2, Jump, CoyoteJump, Number1, Number2, Number3, Number4, Special};

public enum MotionInput {None, Numpad236, Numpad623, Numpad456}

public enum Action {Jump, Grounded, Attack}

public partial class InputManager : Node
{
	public List<ButtonInput> inputList = new List<ButtonInput>();
    public List<float> inputTimes = new List<float>();
    public List<ButtonInput> previousPressedInputs = new List<ButtonInput>();
    public List<float> previousPressedTimes = new List<float>();
    public List<Action> previousPerfomedActions= new List<Action>();
    public List<float> previousPerformedTimes = new List<float>();
    private List<ButtonInput> inputBufferList = new List<ButtonInput>{ButtonInput.Jump, ButtonInput.CoyoteJump, ButtonInput.Attack1, ButtonInput.Attack2};
    List<float> inputBufferTimes = new List<float>{0.15f, 0.15f, 0.15f, 0.15f}; // move this to Constants later

    private ButtonInput previousInputDirection = ButtonInput.None;
    private ButtonInput currentInputDirection;
    private float xDir;
    private float yDir;
    private float removalTime = 0.5f; //0.15f; //0.0166666666 is one frame at 60fps. We should probably set this to something times time.deltaTime

    [Export] Move move;
    [Export] NewJump jump;
    [Export] Attack attack;
    // [Export] MovementController controller; // so this was basically just used to check if facing right in Unity
	
    // Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        move = GetNode<Move>("../Move");
		jump = GetNode<NewJump>("../NewJump"); // update this to jump later
		attack = GetNode<Attack>("../Attack");
	}

	// // Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        if (move != null && move.ProcessMode != ProcessModeEnum.Disabled)
        {
            DetectMoveInput();
        }

        if (jump != null && jump.ProcessMode != ProcessModeEnum.Disabled)
        {
            DetectJumpInput();
        }

        if (attack != null && attack.ProcessMode != ProcessModeEnum.Disabled)
        {
            DetectAttackInput();
        }

        RemoveOldInputs();
	}

    private void DetectMoveInput()
    {
        // for new input system:

        Vector2 inputAxis = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		

        xDir = inputAxis.X;
        yDir = inputAxis.Y;
        currentInputDirection = ConvertAxisInputToNumpad(xDir, yDir);
        //Debug.Log(xDir + " " + yDir);
        //Debug.Log(currentInputDirection);

        

        if (previousInputDirection == ButtonInput.None || previousInputDirection != currentInputDirection)
        {
            AddInputRequestToList(currentInputDirection, Time.GetTicksMsec()/1000f);
            previousInputDirection = currentInputDirection;
            //Debug.Log(GetMostRecentInputTime(currentInputDirection));
        }
        // above is for new input system
    }

    public bool DetectJumpInput()
    {
        if (Input.IsActionJustPressed("ui_accept")) // replace this with custom action later
        {
            AddInputRequestToList(ButtonInput.Jump, Time.GetTicksMsec()/1000f);
            SetPreviousPressedTime(ButtonInput.Jump, Time.GetTicksMsec()/1000f);
            return true;
        }
        return false;
    }

    private void DetectAttackInput()
    {
        if (Input.IsActionJustPressed("LeftMouse")) // add more options for other controllers later
        {
            AddInputRequestToList(ButtonInput.Attack1, Time.GetTicksMsec()/1000f); // new input system
            SetPreviousPressedTime(ButtonInput.Attack1, Time.GetTicksMsec()/1000f);
        }
    }

    public bool HasInputBeenRequested(ButtonInput input)
    {
        //return inputQueue.Contains(input); // disabled for new input system
        return inputList.Contains(input);
    }

    public void RemoveOldInputs()
    {
        while (inputTimes.Count > 0 && Time.GetTicksMsec()/1000f >= inputTimes[0] + removalTime)
        {
            inputList.RemoveAt(0);
            inputTimes.RemoveAt(0);
        }
    }
    public float GetInputBufferTime(ButtonInput input)
    {
        int inputBufferIndex = inputBufferList.IndexOf(input);
        return inputBufferTimes[inputBufferIndex];
    }

    public float GetMostRecentInputTime(ButtonInput input)
    {
        List<ButtonInput> reversedInputList = new List<ButtonInput>(inputList);
        reversedInputList.Reverse();
        int mostRecentIndex = inputList.Count - reversedInputList.IndexOf(input) - 1;
        return mostRecentIndex >= inputList.Count ? -1 : inputTimes[mostRecentIndex];
    }
    public void AddInputRequestToList(ButtonInput input, float time)
    {
        inputList.Add(input);
        inputTimes.Add(time);
    }

    public void SetPreviousPressedTime(ButtonInput input, float time)
    {
        int index = previousPressedInputs.IndexOf(input);
        if (index == -1)
        {
            previousPressedInputs.Add(input);
            previousPressedTimes.Add(time);
        }
        else
        {
            previousPressedTimes[index] = time;
        }
    }

    public float? GetPreviousActionTime(Action action)
    {
        int index = previousPerfomedActions.IndexOf(action);
        return index == -1 ? -1 : previousPerformedTimes[index];
    }

    public void SetPreviousActionTime(Action action, float time)
    {
        int index = previousPerfomedActions.IndexOf(action);
        if (index == -1)
        {
            previousPerfomedActions.Add(action);
            previousPerformedTimes.Add(time);
        }
        else
        {
            previousPerformedTimes[index] = time;
        }
    }

    public void RemoveAllInstancesOfActionFromInputList(ButtonInput input)
    {
        // Check if input is in queue, and if so, remove it, then repeat until all instances are removed

        int index = inputList.IndexOf(input);
        while (index != -1)
        {
            inputList.RemoveAt(index);
            inputTimes.RemoveAt(index);
            index = inputList.IndexOf(input);
        }
    }

    public ButtonInput ConvertAxisInputToNumpad(float horizontal, float vertical)
    {
        if (horizontal > 0.5)
        {
            if (vertical > 0.5)
            {
                return ButtonInput.Direction9;
            }
            else if (vertical < -0.5)
            {
                return ButtonInput.Direction3;
            }
            else
            {
                return ButtonInput.Direction6;
            }
        }
        else if (horizontal < -0.5)
        {
            if (vertical > 0.5)
            {
                return ButtonInput.Direction7;
            }
            else if (vertical < -0.5)
            {
                return ButtonInput.Direction1;
            }
            else
            {
                return ButtonInput.Direction4;
            }
        }
        else
        {
            if (vertical > 0.5)
            {
                return ButtonInput.Direction8;
            }
            else if (vertical < -0.5)
            {
                return ButtonInput.Direction2;
            }
            else
            {
                return ButtonInput.Direction5;
            }
        }
    }

    public MotionInput DetectCommandInput()
    {

        // foreach (Input input in inputList)
        // {
        //     Debug.Log(input);
        // }

        List<List<ButtonInput>> allCommandInputs;
        if (move != null && move.IsFacingRight) // we might want to move isFacingRight to a general movement class
        {
            allCommandInputs = new List<List<ButtonInput>>(Constants.ALL_MOTION_INPUTS_RIGHT);
        }
        else if (move != null && !move.IsFacingRight)
        {
            allCommandInputs = new List<List<ButtonInput>>(Constants.ALL_MOTION_INPUTS_LEFT);
        }
        else
        {
            return MotionInput.None;
        }

        foreach (List<ButtonInput> commandInputList in allCommandInputs)
        {
            List<ButtonInput> subList = new List<ButtonInput>(inputList);
            subList.Reverse();
            int commandInputIndex = allCommandInputs.IndexOf(commandInputList); // this is messy, can we just switch to a regular for loop?
            MotionInput currentCommandInput = Constants.ALL_COMMAND_INPUTS[commandInputIndex];
            List<float> bufferTimes = new List<float>(Constants.ALL_MOTION_INPUTS_BUFFER_TIMES[commandInputIndex]);
            List<ButtonInput> currentCommandInputList = new List<ButtonInput>(commandInputList);
            // Debug.Log("checking for " + currentCommandInput);
            // foreach (ButtonInput input in currentCommandInputList)
            // {
            //     Debug.Log(currentCommandInput + ", " + input);
            // }
            currentCommandInputList.Reverse();
            int motionIndex = 0;
            int bufferTimeIndex = 0;
            int index = subList.IndexOf(currentCommandInputList[motionIndex]); // this is messy too, we use this line in and out of the loop
            while (index != -1)
            {
                motionIndex++;
                if(motionIndex >= currentCommandInputList.Count)
                {
                    return currentCommandInput;
                }
                // Debug.Log("index " + index);
                // foreach (Input input in subList)
                // {
                //     Debug.Log(currentCommandInput + ", size of sublist " + subList.Count + " input " + input);
                // }
                // Debug.Log("index + 1: " + (index + 1) + ", subList size - 1: " + (subList.Count - 1));
                subList = subList.GetRange(index + 1, (subList.Count - (index + 1)));
                // Debug.Log(motionIndex);
                // Debug.Log(currentCommandInput + ", checking for " + currentCommandInputList[motionIndex]);
                index = subList.IndexOf(currentCommandInputList[motionIndex]);
                float timeBetweenInputs = GetMostRecentInputTime(currentCommandInputList[motionIndex - 1]) - GetMostRecentInputTime(currentCommandInputList[motionIndex]);
                // Debug.Log("time between inputs " + timeBetweenInputs);
                if (timeBetweenInputs > bufferTimes[bufferTimeIndex])
                {
                    index = -1;
                    // Debug.Log("you're too slow");
                }
                bufferTimeIndex++;
                
            }
        }

        return MotionInput.None;
    }

}
