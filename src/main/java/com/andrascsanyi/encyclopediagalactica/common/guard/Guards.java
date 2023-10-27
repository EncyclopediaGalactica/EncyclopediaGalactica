package com.andrascsanyi.encyclopediagalactica.common.guard;

/**
 * Guard interface.
 *
 * <p>
 * It collects the type specific guards.
 * </p>
 */
public interface Guards {

    /**
     * Long type related guards.
     *
     * @return Long guards
     */
    LongGuards LongGuards();

    /**
     * Object related guards.
     *
     * @return Object related guards.
     */
    ObjectGuards ObjectGuards();
}
