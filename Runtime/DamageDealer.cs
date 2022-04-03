using System;
using UnityEngine;

using Debug = Padoru.Diagnostics.Debug;

namespace Padoru.Health
{
	public class DamageDealer : MonoBehaviour, IDamageDealer
	{
		public event Action OnDamage;
		public event Action OnKill;

		public void OnDamageDealt()
		{
			Debug.Log("", Constants.DEBUG_CHANNEL, gameObject);

			OnDamage?.Invoke();
		}

		public void OnKilled()
		{
			Debug.Log("", Constants.DEBUG_CHANNEL, gameObject);

			OnKill?.Invoke();
		}
	}
}
