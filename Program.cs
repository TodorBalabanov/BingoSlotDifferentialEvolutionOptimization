
using System;

namespace BingoSlotDifferentialEvolutionOptimization {
class MainClass {
	public static void Main (string[] args) {
		int[][] reels = {
			new int[] {7,4,8,7,5,10,11,8,7,5,9,6,12,4,7,4,7,5,10,7,7,5,5,8,5,9,6,7,4,3,7,3,6,5,11,6,6,3,4,10,3,10,5,5,8,5,10,5,4,6,10,4,7,3,10,6,8,11,6,3,10,6,4,},
			new int[] {3,6,9,4,8,11,8,4,3,10,8,10,8,4,6,7,6,6,8,5,12,8,9,3,8,10,8,12,8,9,7,7,8,5,3,4,7,10,11,10,9,11,12,5,12,6,8,4,7,4,9,4,4,10,4,11,6,8,11,6,9,11,4,},
			new int[] {9,7,3,11,7,9,3,3,5,6,6,9,5,11,7,4,4,6,12,7,10,7,3,9,8,11,9,5,11,6,9,5,9,5,4,4,9,5,11,9,7,4,4,6,4,11,4,3,7,11,10,8,8,8,10,4,10,6,10,12,8,3,5,},
			new int[] {8,4,6,4,7,11,9,9,8,7,4,10,11,7,5,7,5,6,11,5,7,7,10,11,12,6,11,11,10,10,9,12,6,7,8,3,11,7,10,7,4,7,3,10,11,4,11,6,8,9,7,11,7,6,12,7,9,5,8,11,10,4,10,},
			new int[] {4,8,7,5,12,12,11,4,4,8,3,11,11,8,5,7,10,10,12,8,5,3,7,7,11,3,11,7,8,11,6,11,11,11,10,11,7,9,11,7,7,10,12,12,7,3,12,5,12,3,5,6,4,6,12,3,7,10,5,9,4,9,4,},
		};

		DiscreteDifferentialEvolution dde = new DiscreteDifferentialEvolution (reels, 0.9, 3);
		dde.optimize ();
		//Console.WriteLine( dde );
		SlotMachineSimulation simulation = new SlotMachineSimulation ();
		simulation.load( dde.best() );
		simulation.simulate( 3 );
		Console.WriteLine(simulation);
	}
}
}
