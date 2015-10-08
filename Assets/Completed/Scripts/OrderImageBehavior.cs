﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

namespace Completed
{
	public class OrderImageBehavior : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler{
		public static GameObject itemBeingDragged;
		Vector3 startPosition;
		Transform startParent;

		#region IBeginDragHandler implementation

		public void OnBeginDrag (PointerEventData eventData)
		{
			itemBeingDragged = gameObject;
			startPosition = transform.position;
			startParent = transform.parent;

			GetComponent<CanvasGroup> ().blocksRaycasts = false;
		}

		#endregion

		#region IDragHandler implementation

		public void OnDrag (PointerEventData eventData)
		{
			transform.position = Input.mousePosition;
		}

		#endregion

		#region IEndDragHandler implementation

		public void OnEndDrag (PointerEventData eventData)
		{	
			itemBeingDragged = null;
			GetComponent<CanvasGroup> ().blocksRaycasts = true;
			
			print ("aqui1");
			if (transform.parent == startParent) {
				transform.position = startPosition;
			} else {
				print ("aqui");
				transform.parent.GetChild (0).gameObject.transform.position = startPosition;
			}
		}

		#endregion



	}
}