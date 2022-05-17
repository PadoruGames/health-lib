using Padoru.Core;
using System;

namespace Padoru.Health
{
	public interface IHealth
	{
		event Action OnHeal;
		event Action OnDamage;
		event Action OnDeath;

		int CurrentHealth { get; }
		int MaxHealth { get; }
		SubscribableValue<float> NormalizedCurrentHealth { get; }
		bool IsAlive { get; }

		void Damage(int damageAmount, IDamageDealer damageDealer = null);
		void Heal(int healAmount);
		void Kill(IDamageDealer damageDealer = null);
		void SetMaxHealth(int maxHealth);
		void Reset();
	}
}
