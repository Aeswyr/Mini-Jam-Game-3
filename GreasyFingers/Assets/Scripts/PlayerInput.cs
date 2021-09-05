using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    [HideInInspector] public float horizontal;		//Float that stores horizontal input
    [HideInInspector] public float vertical;		//Float that stores vertical input
	[HideInInspector] public bool jumpHeld;			//Bool that stores jump pressed
	[HideInInspector] public bool jumpPressed;		//Bool that stores jump held
	[HideInInspector] public bool crouchHeld;		//Bool that stores crouch pressed
	[HideInInspector] public bool crouchPressed;	//Bool that stores crouch held

    [HideInInspector] public bool summonPressed;
    [HideInInspector] public bool dashPressed;
    [HideInInspector] public bool attackPressed;

	bool readyToClear;								//Bool used to keep input in sync

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
	{
		//Clear out existing input values
		ClearInput();

		//If the Game Manager says the game is over, exit
		//if (GameManager.IsGameOver())
		//	return;

		//Process keyboard, mouse, gamepad (etc) inputs
		ProcessInputs();

		//Clamp the horizontal input to be between -1 and 1
		horizontal = Mathf.Clamp(horizontal, -1f, 1f);
		vertical   = Mathf.Clamp(vertical, -1f, 1f);
	}

    void FixedUpdate()
	{
		//In FixedUpdate() we set a flag that lets inputs to be cleared out during the 
		//next Update(). This ensures that all code gets to use the current inputs
		readyToClear = true;
	}

	void ClearInput()
	{
		//If we're not ready to clear input, exit
		if (!readyToClear)
			return;

		//Reset all inputs
		horizontal		= 0f;
		vertical		= 0f;
		jumpPressed		= false;
		jumpHeld		= false;
		crouchPressed	= false;
		crouchHeld		= false;
		attackPressed   = false;
        summonPressed   = false;
		dashPressed 	= false;
		readyToClear	= false;
	}

    void ProcessInputs()
	{
		//Accumulate horizontal axis input
		horizontal		= Input.GetAxis("Horizontal");
		vertical		= Input.GetAxis("Vertical");

		//Accumulate button inputs
		jumpPressed		= jumpPressed || Input.GetButtonDown("Jump");
		jumpHeld		= jumpHeld || Input.GetButton("Jump");
        summonPressed   = summonPressed || Input.GetButtonDown("Summon");
        dashPressed		= dashPressed || Input.GetButtonDown("Dash");
        attackPressed	= attackPressed || Input.GetButtonDown("Attack");
	}
}
