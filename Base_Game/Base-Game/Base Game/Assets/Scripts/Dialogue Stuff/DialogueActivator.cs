﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueActivator : MonoBehaviour, IInteractable
{
	[SerializeField] public DialogueObject dialogueObject;
	[SerializeField] private GameObject dialogueBox;


	public void UpdateDialogueObject(DialogueObject dialogueObject)
	{
		this.dialogueObject = dialogueObject;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag("Player") && other.TryGetComponent(out PlayerMovement player))
		{
			player.Interactable = this;
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if(other.CompareTag("Player") && other.TryGetComponent(out PlayerMovement player))
		{
			if (player.Interactable is DialogueActivator dialogueActivator && dialogueActivator == this)
			{
				player.Interactable = null;
			}
		}
	}

	public void Interact(PlayerMovement player)
	{
		foreach(DialogueResponseEvents responseEvents in GetComponents<DialogueResponseEvents>())
		{
			if(responseEvents.DialogueObject == dialogueObject)
			{
				player.DialogueUI.AddResponseEvents(responseEvents.Events);
				break;
			}
		}
	
		player.DialogueUI.ShowDialogue(dialogueObject);
	}
}
