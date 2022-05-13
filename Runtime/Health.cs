using Padoru.Core;
using System;
using UnityEngine;

using Debug = Padoru.Diagnostics.Debug;

namespace Padoru.Health
{
	public class Health : MonoBehaviour, IHealth
	{
		[SerializeField] private int maxHealth;

		public event Action OnHeal;
		public event Action OnDamage;
		public event Action OnDeath;

        public int CurrentHealth { get; private set; }
        public int MaxHealth => maxHealth;
		public bool IsAlive => CurrentHealth > 0;

		public SubscribableValue<float> NormalizedCurrentHealth { get; private set; }

		private void Awake()
		{
			CurrentHealth = MaxHealth;

			NormalizedCurrentHealth = new SubscribableValue<float>((float)CurrentHealth / MaxHealth);

			Debug.Log($"Health component initialized with {MaxHealth} maxHealth", Constants.DEBUG_CHANNEL, gameObject);
		}

		public void Damage(int damageAmount, IDamageDealer damageDealer = null)
		{
			if (!IsAlive)
			{
				Debug.LogWarning($"Could not damage unit because it was already death", Constants.DEBUG_CHANNEL, gameObject);
				return;
			}

			CurrentHealth = Mathf.Clamp(CurrentHealth - damageAmount, 0, MaxHealth);
			OnDamage?.Invoke();
			NormalizedCurrentHealth.Value = (float)CurrentHealth / MaxHealth;
			damageDealer?.OnDamageDealt();

			Debug.Log($"Unit damaged by {damageAmount}. Current Health: {CurrentHealth}. Damage dealer: {damageDealer}", Constants.DEBUG_CHANNEL, gameObject);

			if (!IsAlive)
			{
				Die();
			}
		}

		public void Heal(int healAmount)
		{
			if (!IsAlive)
			{
				Debug.LogWarning($"Could not heal unit because it was already death", Constants.DEBUG_CHANNEL, gameObject);
				return;
			}

			CurrentHealth = Mathf.Clamp(CurrentHealth + healAmount, 0, MaxHealth);
			OnHeal?.Invoke();
			NormalizedCurrentHealth.Value = (float)CurrentHealth / MaxHealth;

			Debug.Log($"Unit healed by {healAmount}. Current Health: {CurrentHealth}", Constants.DEBUG_CHANNEL, gameObject);
		}

		public void SetMaxHealth(int maxHealth)
		{
			this.maxHealth = maxHealth;
			NormalizedCurrentHealth.Value = (float)CurrentHealth / MaxHealth;

			Debug.Log($"Unit max health changed to {maxHealth}", Constants.DEBUG_CHANNEL, gameObject);
		}

		public void Reset()
		{
			CurrentHealth = maxHealth;
		}

		private void Die(IDamageDealer damageDealer = null)
		{
			OnDeath?.Invoke();
			damageDealer?.OnKilled();

			Debug.Log($"Unit died", Constants.DEBUG_CHANNEL, gameObject);
		}
	}
}
