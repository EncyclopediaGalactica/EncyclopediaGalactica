package com.andrascsanyi.encyclopediagalactica.common.guard;

public interface LongGuards {

    /**
     * Checks if the provided {@link Long} objects are not equal.
     *
     * @param l1 {@link Long} object
     * @param l2 {@link Long} object
     * @return boolean
     */
    boolean areNotEquals(Long l1, Long l2);

    /**
     * Checks if the provided {@link long} primitive values are not equal.
     *
     * @param l1 {@link long} primitive value
     * @param l2 {@link long} primitive value
     * @return boolean
     */
    boolean areNotEquals(long l1, long l2);
}
