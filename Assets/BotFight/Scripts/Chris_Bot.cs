using UnityEngine;
using System.Collections;

public class Chris_Bot : MonoBehaviour {

	private BotController m_bot;
	private bool canStartBot = false;

	// Use this for initialization
	void Start () {
	
		m_bot = GetComponent<BotController>();
		m_bot.botDebug = true;

	}
	
	// Update is called once per frame
	void Update () {

		if(m_bot.canMove && canStartBot == false)
		{
			canStartBot = true;
			startMoving();
		}

	}

	private void startMoving()
	{	
		m_bot.Action_ThrowGrenade();
		m_bot.Action_ThrowGrenade();
		Invoke("actionList", 0.5f);


	}

	private void actionList()
	{
		MoveRight();
		Invoke("stopBot", 0.5f);
		Invoke("Jump", 0.6f);
		Invoke("MoveRight", 1.2f);
		Invoke("stopBot", 1.7f);
		Invoke("Punch", 1.8f);
		Invoke("throwGrenade", 2.0f);

	}

	private void destoryBot()
	{
		Destroy(this.gameObject);
	}

	private void throwGrenade()
	{
		m_bot.Action_ThrowGrenade();
	}

	private void Punch()
	{
		m_bot.Action_Punch();
	}

	private void stopBot()
	{
		m_bot.Action_StopMovement();
	}

	private void MoveRight()
	{
		m_bot.Action_RunRight();
	}

	private void MoveLeft()
	{
		m_bot.Action_RunLeft();
	}

	private void Jump()
	{
		m_bot.Action_Jump();
	}

}
