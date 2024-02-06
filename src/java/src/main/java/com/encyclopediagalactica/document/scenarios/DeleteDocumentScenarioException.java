package com.encyclopediagalactica.document.scenarios;

public class DeleteDocumentScenarioException extends RuntimeException {
    public DeleteDocumentScenarioException() {
    }

    public DeleteDocumentScenarioException(String message) {
        super(message);
    }

    public DeleteDocumentScenarioException(String message, Throwable cause) {
        super(message, cause);
    }

    public DeleteDocumentScenarioException(Throwable cause) {
        super(cause);
    }

    public DeleteDocumentScenarioException(String message, Throwable cause, boolean enableSuppression, boolean writableStackTrace) {
        super(message, cause, enableSuppression, writableStackTrace);
    }
}
