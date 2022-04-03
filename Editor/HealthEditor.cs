using UnityEditor;
using UnityEngine;

namespace Padoru.Health.Editor
{
	[CustomEditor(typeof(Health))]
	public class HealthEditor : UnityEditor.Editor
	{
		private Health t;

		private void OnEnable()
		{
			t = (Health)target;
		}

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			if (!Application.isPlaying)
			{
				return;
			}

			if (GUILayout.Button("Damage 10%"))
			{
				DamagePercent(10);
			}

			if (GUILayout.Button("Heal 10%"))
			{
				HealPercent(10);
			}
		}

		private void DamagePercent(float percent)
		{
			var damageAmount = GetHealthPercent(percent);
			t.Damage(damageAmount);
		}

		private void HealPercent(float percent)
		{
			var healAmount = GetHealthPercent(percent);
			t.Heal(healAmount);
		}

		private int GetHealthPercent(float percent)
		{
			var multiplier = percent / 100;
			return (int)(t.MaxHealth * multiplier);
		}
	}
}
