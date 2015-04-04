﻿using UnityEngine;
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
		int _rand = Random.Range(0, 1);

		if(_rand == 0)
		{
			MoveRight();
			Invoke("stopBot", 0.5f);
			Invoke("Jump", 0.6f);
			Invoke("MoveRight", 1.2f);
			Invoke("stopBot", 1.7f);
			Invoke("Punch", 1.8f);
		}
		else
		{

		}

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
