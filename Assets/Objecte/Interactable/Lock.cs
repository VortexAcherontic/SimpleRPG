using UnityEngine;
using System.Collections.Generic;

public class Lock : MonoBehaviour {
		GUI_Helper GUI_ZoD = new GUI_Helper ();
	
		public List<KeyCode> LockOrder = new List<KeyCode> ();
		List<KeyCode> TmpLockOrder = new List<KeyCode> ();
	
		int tryToSolve = 0;
		public bool isLocked;
	
		void Update () {
				if (IsSolving == false) {
						return;
				}
		
				if (Input.GetKeyDown (KeyCode.Escape)) {
						IsSolving = false;
				}
		
				if (Input.GetKeyDown (KeyCode.LeftArrow)) {
						if (TmpLockOrder [tryToSolve] == KeyCode.LeftArrow) {
								tryToSolve++;
								//klick sound zum bestätigen das man das richtige getroffen hat?
						} else {
								tryToSolve = 0;
						}
				}
				if (Input.GetKeyDown (KeyCode.RightArrow)) {
						if (TmpLockOrder [tryToSolve] == KeyCode.RightArrow) {
								tryToSolve++;
								//klick sound zum bestätigen das man das richtige getroffen hat?
						} else {
								tryToSolve = 0;
						}
				}	
		
				if (tryToSolve >= TmpLockOrder.Count) {
						IsSolving = false;
						isLocked = false;
				}
		}
	
		public void generateLock (int Diff) {
				for (int i = 0; i<Diff; i++) {
						int a = Random.Range (0, 2);
						if (a == 0) {
								LockOrder.Add (KeyCode.LeftArrow);
						} else {
								LockOrder.Add (KeyCode.RightArrow);
						}
				}
				isLocked = true;
				Debug.Log (LockOrder);
		}
	
		bool IsSolving = false;
	
		void OnGUI () {
				if (IsSolving == false) {
						return;
				}
				GUI_ZoD.Label ("Tolles interface", 11, new Rect (50, 50, 1920 - 100, 1080 - 100));
		}
	
		public void solveLock () {
				IsSolving = true;
				TmpLockOrder = LockOrder;
		}
}

