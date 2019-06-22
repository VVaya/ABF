using System;

public interface IAction {
	string Name { get; }
	string Description { get; }
	void Activate();
}

public static class IActionExtension {
	public static void ListAction(this IAction[] actions) {
		for(int i = 0; i < actions.Length; i++) {
			var action = actions[i];
			Console.WriteLine(i + " : " + action.Name + "(" + action.Description + ")");
		}
	}

	public static bool ReadActionInput(this IAction[] actions, out int index) {
		int.TryParse(Console.ReadLine(), out index);
		return index >= 0 && index < actions.Length;

	}
}