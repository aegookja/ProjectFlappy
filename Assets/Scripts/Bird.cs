﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
	private Animator animator;
	private Rigidbody2D physicsBody;

	private SkillManager skillManager;

	private void Start()
	{
		animator = GetComponent<Animator>();
		physicsBody = GetComponent<Rigidbody2D>();

		skillManager = new SkillManager(this);
		skillManager.AddSkill(new Flap());
	}

	private void FixedUpdate()
	{
		if (Input.GetButtonDown("Flap"))
		{
			skillManager.CastSkill(0);
		}

		if (Input.GetButtonDown("Special"))
		{
			skillManager.CastSkill(1);
		}
	}

	public void SkillEnd()
	{
		skillManager.SkillEnd();
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Pipe")
		{
			gameObject.SetActive(false);
		}
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "ScoringArea")
		{
			FlappyManager.instance.score.value += 5;
		}
	}

	public void TriggerAnimation(string name)
	{
		animator.SetTrigger(name);
	}

	public void AddForce(Vector2 force)
	{
		physicsBody.AddForce(force, ForceMode2D.Impulse);
	}

	public void Loiter(float duration)
	{
		FlappyManager.instance.Loiter(duration);
	}
}
