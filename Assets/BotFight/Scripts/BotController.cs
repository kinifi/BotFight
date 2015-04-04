using UnityEngine;
using System.Collections;

public class BotController : MonoBehaviour {

	#region Variables

	public bool botDebug = false;

	/// <summary>
	/// Can the player move?
	/// </summary>
	public bool canMove = false;

	/// <summary>
	/// Can the player jump?
	/// </summary>
	public bool canJump = false;

	//the animator component
	private Animator _anim;

	//false = left, true = right
	private bool standing = true;

	//Xaxis value of the input; Example: Horizontal arrow keys
	private float XaxisValue;

	/// <summary>
	/// The speed of the player
	/// </summary>
	private Vector2 m_speed = new Vector2(8.0f, 0.0f);

	/// <summary>
	/// The height of the bots jump
	/// </summary>
	private Vector2 m_JumpHeight = new Vector2(0, 400.0f);

	/// <summary>
	/// Is this being played by a human?
	/// </summary>
	public bool isHuman = true;

	/// <summary>
	/// The Rigidbody attached to the player
	/// </summary>
	private Rigidbody2D _rigidPlayer;

	/// <summary>
	/// The collision layers to hit
	/// </summary>
	public LayerMask m_collisionToHit;

	#endregion

	#region botDebug Variables

	/// <summary>
	/// The scroll position of the debug
	/// </summary>
	private Vector2 scrollPosition;

	private string botDebugValue = "";

	#endregion

	#region General Methods

	// Use this for initialization
	void Start () {

		//get the animator attached to this gameobject
		_anim = GetComponent<Animator>();
		//get the rigidbody attached to this player
		_rigidPlayer = GetComponent<Rigidbody2D>();

		DebugBot("Bot: " + gameObject.name + " initialized");

	}

	// Update is called once per frame
	private void Update () {

		//only do these actions if a player is controlling them
		if(isHuman && canMove)
		{
			isHumanActions();
		}

		//toggle the direction of the player depending on the xaxis
		if(_rigidPlayer.velocity.x > 0 && !standing)
		{
			Action_ToggleFlipDirection();
			
		}
		else if(_rigidPlayer.velocity.x < 0 && standing)
		{
			Action_ToggleFlipDirection();
		}

		//check if the player is on the ground or not
		groundCheck();

	}

	#endregion

	#region isHuman Methods

	/// <summary>
	/// Is called if isHuman is true
	/// </summary>
	private void isHumanActions()
	{
		XaxisValue = Input.GetAxis("Horizontal");
		_anim.SetFloat("Horizontal", Mathf.Abs(XaxisValue));

		//move the player if we need to
		if(Input.GetAxis("Horizontal") > 0.5f)
		{
			Action_RunRight();
		}
		else if(Input.GetAxis("Horizontal") < -0.5f)
		{
			Action_RunLeft();
		}



	}

	#endregion

	/// <summary>
	/// Check if the player is on the ground or not and play the appropriate animation
	/// </summary>
	private void groundCheck()
	{

		Debug.DrawRay(transform.position, -Vector2.up * 1.0f, Color.green);
		RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 1.0f, m_collisionToHit);
		if(hit.collider != null)
		{
			if(hit.transform.tag == "Ground")
			{
				_anim.SetBool("isGrounded", true);
				canJump = true;
			}
			else
			{
				//Debug.Log(hit.transform.name);

			}
		}
		else
		{
			_anim.SetBool("isGrounded", false);
			canJump = false;
		}
	}

	#region Bot Actions

	/// <summary>
	/// Can the Bot Move?
	/// </summary>
	/// <returns><c>true</c>, if bot can move, <c>false</c> otherwise.</returns>
	public bool botCanMove()
	{
		return canMove;
	}

	/// <summary>
	/// Is called when the timer countdown ends; Allows player to move
	/// </summary>
	public void botStart()
	{
		canMove = true;
		DebugBot("Start bots!");
	}

	public void Action_StopMovement()
	{
		_rigidPlayer.velocity = new Vector2(0,0);
		_anim.SetFloat("Horizontal", 0.0f);
	}

	/// <summary>
	/// Jump Action for the Bot
	/// </summary>
	public void Action_Jump()
	{
		if(canJump)
		{
			_rigidPlayer.AddForce(m_JumpHeight);
			DebugBot("Jump!");
		}
	}

	/// <summary>
	/// Plays the Punch Action; Damages within close combat.
	/// </summary>
	public void Action_Punch()
	{
		_anim.SetTrigger("Punch");
		DebugBot("Punch");
	}

	/// <summary>
	/// Action to make bot Run Left
	/// </summary>
	public void Action_RunLeft()
	{
		DebugBot("Move Left");
		_anim.SetFloat("Horizontal", 1.0f);
		_rigidPlayer.velocity = -m_speed;
		/*
		if(!standing)
		{
			Action_ToggleFlipDirection();
		}
		*/
	}

	/// <summary>
	/// Action to make bot run Right
	/// </summary>
	public void Action_RunRight()
	{
		DebugBot("Move Right");
		_anim.SetFloat("Horizontal", 1.0f);
		_rigidPlayer.velocity = m_speed;
		/*
		if(standing)
		{
			Action_ToggleFlipDirection();
		}
		*/
	}

	/// <summary>
	/// Flip the player direction
	/// </summary>
	public void Action_ToggleFlipDirection() {

		//standing = right !standing = left
		standing = !standing;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
		DebugBot("Flip Bot");
	}

	#endregion

	#region bot Debug


	public void DebugBot(string _log)
	{
		botDebugValue += _log + "\n";
	}

	/// <summary>
	/// Displays the Debug Values
	/// </summary>
	private void OnGUI()
	{
		if(botDebug)
		{

			GUILayout.BeginArea (new Rect(0, 0, 200, Screen.height));
			scrollPosition = GUILayout.BeginScrollView(scrollPosition);

			botDebugValue = GUILayout.TextArea(botDebugValue, 2000);

			GUILayout.EndScrollView();

			GUILayout.EndArea();
		}
	}

	#endregion

}
