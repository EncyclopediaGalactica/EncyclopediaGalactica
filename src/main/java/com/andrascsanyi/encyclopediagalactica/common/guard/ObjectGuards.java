package com.andrascsanyi.encyclopediagalactica.common.guard;


import com.andrascsanyi.encyclopediagalactica.common.guard.exceptions.ObjectIsNullException;

public interface ObjectGuards {

    /**
     * Checks if the provided {@link Object} is not null.
     *
     * @param o provided {@link Object}
     * @return boolean
     */
    boolean isNotNull(Object o);

    /**
     * Throws if the provided {@link Object} is null.
     *
     * @param o the provided {@link Object}
     * @throws ObjectIsNullException when the provided {@link Object} is indeed null.
     */
    void throwIfNull(Object o);
}
