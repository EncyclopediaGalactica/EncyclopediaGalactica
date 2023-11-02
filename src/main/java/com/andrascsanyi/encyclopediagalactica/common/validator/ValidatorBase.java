package com.andrascsanyi.encyclopediagalactica.common.validator;

import com.andrascsanyi.encyclopediagalactica.common.validator.exceptions.ValidationException;
import jakarta.validation.ConstraintViolation;
import jakarta.validation.Validation;
import jakarta.validation.Validator;
import java.util.Set;

public class ValidatorBase<T> {

    protected final Validator validator = Validation.buildDefaultValidatorFactory().getValidator();

    protected void checkViolationsAndThrow(Set<ConstraintViolation<T>> violations) {
        if (violations.isEmpty()) {
            return;
        }

        StringBuilder sb = new StringBuilder().append("Errors: ");
        violations.forEach(v -> {
            sb.append(v.getMessage());
        });

        throw new ValidationException(sb.toString());

    }
}
