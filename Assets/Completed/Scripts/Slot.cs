using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

namespace Completed
{
	public class Slot : MonoBehaviour, IDropHandler {

		public GameObject item {
			get {
				if(transform.childCount > 0){
					return transform.GetChild (0).gameObject;
				}
				return null;
			}
		
		}

		#region IDropHandler implementation

		public void OnDrop (PointerEventData eventData)
		{
			item.transform.SetParent (OrderImageBehavior.itemBeingDragged.transform.parent);

			OrderImageBehavior.itemBeingDragged.transform.SetParent(transform);
		}

		#endregion
	}
}