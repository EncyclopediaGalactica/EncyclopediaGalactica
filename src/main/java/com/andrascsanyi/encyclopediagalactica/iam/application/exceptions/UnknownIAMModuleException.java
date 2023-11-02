package com.andrascsanyi.encyclopediagalactica.iam.application.exceptions;

public class UnknownIAMModuleException extends RuntimeException {

    public UnknownIAMModuleException() {
    }

    public UnknownIAMModuleException(String message) {
        super(message);
    }

    public UnknownIAMModuleException(String message, Throwable cause) {
        super(message, cause);
    }

    public UnknownIAMModuleException(Throwable cause) {
        super(cause);
    }

    public UnknownIAMModuleException(String message, Throwable cause, boolean enableSuppression,
        boolean writableStackTrace) {
        super(message, cause, enableSuppression, writableStackTrace);
    }
}
