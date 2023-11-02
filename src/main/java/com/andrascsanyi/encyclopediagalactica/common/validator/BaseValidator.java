package com.andrascsanyi.encyclopediagalactica.common.validator;

import com.andrascsanyi.encyclopediagalactica.common.validator.exceptions.ValidationException;

/**
 * Base Validator interface
 *
 * @param <T> Type of the Object is under validation
 */
public interface BaseValidator<T> {

    /**
     * Validates the provided object and throws if the object is invalid.
     *
     * @param o The object to be validated.
     * @throws ValidationException when provided object is invalid.
     */
    void validateAndThrow(T o);
}
