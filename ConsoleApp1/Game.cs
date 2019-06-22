using System;
using System.Collections.Generic;
using ConsoleApp1.System;

namespace ConsoleApp1 {

	public interface ICharacter {
		string Name { get; set; }
		int HP { get; set; }
	}

	public interface ITurnAction {
		void TakeTurn();
	}

	public class Monster: ICharacter, ITurnAction {
		public string Name { get; set; }
		public int HP { get; set; }
		public int ATKPower { get; set; }

		private ICharacter _player;

		public Monster(ICharacter player) {
			_player = player;
		}

		public void TakeTurn() {
			_player.HP -= ATKPower;
			Console.WriteLine(Name + " attack " + _player.Name + " and deal " + ATKPower);
		}

	}

	public class Player: ICharacter, ITurnAction {
		public string Name { get; set; }
		public int HP { get; set; }
		public IAction[] Actions { get; set; }

		public void TakeTurn() {
			Console.WriteLine("Select your action (by type index)");
			Actions.ListAction();
			int index = 0;
			while(!Actions.ReadActionInput(out index)) {
				Console.WriteLine("!!! intput error try again !!!");
			}
			Actions[index].Activate();
		}
	
	}

	public class Game : IBattleField{
		public ICharacter[] Monsters => _monsters;
		private Monster[] _monsters;
		private Player _player;

		private List<ITurnAction> Initialize() {
			_player = new Player {
				HP = 10,
				Name = "AFPlayer",
				Actions = new IAction[] {
					new Attack(this, 5),
					new Attack(this, 20),
					new Attack(this, 100)
				}
			};
			_monsters = RandomMonster(_player);

			List<ITurnAction> characters = new List<ITurnAction>();
			characters.Add(_player);
			characters.AddRange(_monsters);
			return characters;
		}

		public void StartGame() {
			var turners = Initialize();
			while(true) {
				Console.WriteLine("==========================================================");
				foreach(var turner in turners) {
					turner.TakeTurn();
					Console.WriteLine("------------------------------------------------------------");
				}
				Console.WriteLine("Next round ? (y/n)");
				if(Console.ReadLine().Equals("n")) {
					return;
				}
			}

		}

		public Monster[] RandomMonster(ICharacter player) {
			Random r = new Random();
			int quantity = r.Next(2, 5);

			Monster[] monsters = new Monster[quantity];
			for(int i = 0; i < quantity; i++) {
				monsters[i] = new Monster(player);
				monsters[i].HP = r.Next(10, 50);
				monsters[i].Name = "monster [" + i + "]";
			}

			return monsters;
		}
	}


}
