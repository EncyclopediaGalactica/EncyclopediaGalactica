package com.andrascsanyi.encyclopediagalactica.common.validator.constraints;

public class ConstraintConfigurationException extends RuntimeException {

    public ConstraintConfigurationException() {
    }

    public ConstraintConfigurationException(String message) {
        super(message);
    }

    public ConstraintConfigurationException(String message, Throwable cause) {
        super(message, cause);
    }

    public ConstraintConfigurationException(Throwable cause) {
        super(cause);
    }

    public ConstraintConfigurationException(String message, Throwable cause,
        boolean enableSuppression,
        boolean writableStackTrace) {
        super(message, cause, enableSuppression, writableStackTrace);
    }
}
