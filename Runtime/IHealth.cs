using System;

namespace Padoru.Health
{
	public interface IHealth
	{
		event Action OnHeal;
		event Action OnDamage;
		event Action OnDeath;
		event Action OnHealthChanged;
		event Action OnMaxHealthChanged;

		int CurrentHealth { get; }
		int MaxHealth { get; }
		float CurrentHealthNormalized { get; }
		bool IsAlive { get; }

		void Damage(int damageAmount, IDamageDealer damageDealer = null);
		void Heal(int healAmount);
		void SetMaxHealth(int maxHealth);
		void Reset();
	}
}
