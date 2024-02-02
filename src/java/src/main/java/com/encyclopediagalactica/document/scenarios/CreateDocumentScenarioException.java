package com.encyclopediagalactica.document.scenarios;

public class CreateDocumentScenarioException extends RuntimeException {
    public CreateDocumentScenarioException() {
    }

    public CreateDocumentScenarioException(String message) {
        super(message);
    }

    public CreateDocumentScenarioException(String message, Throwable cause) {
        super(message, cause);
    }

    public CreateDocumentScenarioException(Throwable cause) {
        super(cause);
    }

    public CreateDocumentScenarioException(String message, Throwable cause, boolean enableSuppression, boolean writableStackTrace) {
        super(message, cause, enableSuppression, writableStackTrace);
    }
}
