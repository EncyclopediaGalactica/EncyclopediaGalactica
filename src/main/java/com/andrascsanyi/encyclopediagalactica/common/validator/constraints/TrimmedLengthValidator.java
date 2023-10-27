package com.andrascsanyi.encyclopediagalactica.common.validator.constraints;

import jakarta.validation.ConstraintValidator;
import jakarta.validation.ConstraintValidatorContext;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

public class TrimmedLengthValidator implements ConstraintValidator<TrimmedLength, String> {

    private static final Logger LOG = LoggerFactory.getLogger(TrimmedLengthValidator.class);

    private int min;
    private int max;

    @Override
    public void initialize(TrimmedLength trimmedLengthConstraintAnnotation) {
        min = trimmedLengthConstraintAnnotation.min();
        max = trimmedLengthConstraintAnnotation.max();
        validateParameters();
    }

    @Override
    public boolean isValid(String s, ConstraintValidatorContext constraintValidatorContext) {
        if (s == null) {
            return true;
        }

        int length = s.trim().length();
        return length >= min && length <= max;
    }

    private void validateParameters() {
        if (min < 0) {
            String message = "Min value must be equal or greater to zero!";
            LOG.error(message);
            throw new ConstraintConfigurationException(message);
        }
        if (max < 0) {
            String message = "Max value must be equal or greater to zero!";
            LOG.error(message);
            throw new ConstraintConfigurationException(message);
        }
        if (max < min) {
            String message = "Max must be greater than Min!";
            LOG.error(message);
            throw new ConstraintConfigurationException(message);
        }
    }
}
