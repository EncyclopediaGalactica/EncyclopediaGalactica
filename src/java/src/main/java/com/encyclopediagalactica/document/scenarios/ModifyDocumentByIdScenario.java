package com.encyclopediagalactica.document.scenarios;

import com.encyclopediagalactica.document.dto.DocumentDto;
import com.encyclopediagalactica.document.entities.DocumentEntity;

public interface ModifyDocumentByIdScenario {
    /**
     * Modifies the designated {@link DocumentEntity} entity based on the provided {@link DocumentDto} object
     *
     * @param documentId the designated {@link DocumentEntity}
     * @param document   the new properties
     * @return the modified {@link DocumentDto}
     */
    DocumentDto modifyDocument(Long documentId, DocumentDto document);
}
