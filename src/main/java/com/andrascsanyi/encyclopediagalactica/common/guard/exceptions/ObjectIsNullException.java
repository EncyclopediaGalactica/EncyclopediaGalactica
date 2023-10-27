package com.andrascsanyi.encyclopediagalactica.common.guard.exceptions;

public class ObjectIsNullException extends RuntimeException {

    public ObjectIsNullException() {
    }

    public ObjectIsNullException(String message) {
        super(message);
    }

    public ObjectIsNullException(String message, Throwable cause) {
        super(message, cause);
    }

    public ObjectIsNullException(Throwable cause) {
        super(cause);
    }

    public ObjectIsNullException(String message, Throwable cause, boolean enableSuppression,
        boolean writableStackTrace) {
        super(message, cause, enableSuppression, writableStackTrace);
    }
}
