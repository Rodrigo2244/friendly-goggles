using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class selectableBehavior : MonoBehaviour, ISelectHandler {

	public AudioClip sfx;

	public void OnSelect(BaseEventData eventData){
		AudioSource.PlayClipAtPoint(sfx,transform.position);
	}
}