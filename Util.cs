
using System;

namespace BingoSlotDifferentialEvolutionOptimization {
class Util {
	/**
	 * Enforce symbols diversity.
	 */
	public const bool STRICT_SYMBOLS_DIVERSITY = true;

	/**
	 * Report in steps.
	 */
	public const bool REEVALUATE_TARGET_VECTOR = true;

	/**
	 * Report in steps.
	 */
	public const bool VERBOSE = true;

	/**
	* Pseudo-random number generator.
	*/
	public static Random prng = new Random ();
}
}

