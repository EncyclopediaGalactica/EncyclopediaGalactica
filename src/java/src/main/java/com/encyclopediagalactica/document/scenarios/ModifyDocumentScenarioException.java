package com.encyclopediagalactica.document.scenarios;

public class ModifyDocumentScenarioException extends RuntimeException {
    public ModifyDocumentScenarioException() {
    }

    public ModifyDocumentScenarioException(String message) {
        super(message);
    }

    public ModifyDocumentScenarioException(String message, Throwable cause) {
        super(message, cause);
    }

    public ModifyDocumentScenarioException(Throwable cause) {
        super(cause);
    }

    public ModifyDocumentScenarioException(String message, Throwable cause, boolean enableSuppression, boolean writableStackTrace) {
        super(message, cause, enableSuppression, writableStackTrace);
    }
}
