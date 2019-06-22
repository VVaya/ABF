using System;
using ConsoleApp1.System;

namespace ConsoleApp1 {

	public class Attack: IAction {

		public string Name => "Attack";
		public string Description => "Deal "+ _damage + " Damage to enemy";

		private int _damage;
		private IBattleField _battleField;

		public Attack(IBattleField battleField, int damage){
			_battleField = battleField;
			_damage = damage;
		}

		public void Activate() {
			DisplayDescription();
			var target = ReadInput();
			ApplyEffect(target);
			
		}

		private void ApplyEffect(int index) {
			var monsters = _battleField.Monsters;
			if(index >= monsters.Length) {
				Console.WriteLine("Aw ! you miss ...");
			} else {
				var target = monsters[index];
				if(target.HP <= 0) {
					Console.WriteLine("You hit a corpse ...");
				}
				target.HP -= _damage;
				Console.WriteLine("Deal " + _damage + " damage to " + target.Name +" and hp remain "+target.HP);
			}
		}

		private int ReadInput() {
			var input = Console.ReadLine();
			int.TryParse(input, out int index);
			return index;
		}

		private void DisplayDescription() {
			Console.WriteLine("Select the target by typing enemy index");
			var monsters = _battleField.Monsters;
			for(int i = 0; i < monsters.Length; i++) {
				var monster = monsters[i];
				Console.WriteLine(i + " : " + monster.Name + "(HP : " + monster.HP + ")");
			}
		}

	}

}
