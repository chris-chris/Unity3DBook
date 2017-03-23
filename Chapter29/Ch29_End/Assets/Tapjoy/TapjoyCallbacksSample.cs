using UnityEngine;

namespace TapjoyUnity {
	public class TapjoyCallbacksSample : TapjoyRuntimeCallbacks {
		// Called after successful Tapjoy connect, but before Tapjoy initializes Fyber
		// Gets user id to pass to Fyber on init
		public override string GetFyberUserId() {
			//TODO: Return your fyber user id here
			return "";
		}
	}
}
