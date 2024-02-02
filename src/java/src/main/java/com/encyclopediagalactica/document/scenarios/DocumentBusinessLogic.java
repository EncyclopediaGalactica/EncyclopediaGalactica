package com.encyclopediagalactica.document.scenarios;

import com.encyclopediagalactica.document.dto.DocumentDto;
import com.encyclopediagalactica.document.entities.DocumentEntity;

import java.util.List;

public interface DocumentBusinessLogic {

    /**
     * Returns the list of {@link DocumentEntity} entities.
     *
     * @return list of {@link DocumentEntity} entities
     */
    List<DocumentDto> getDocuments();

    /**
     * Returns the designated {@link DocumentEntity} entity.
     *
     * @param id identifier of the {@link DocumentEntity} entity
     * @return instance of {@link DocumentEntity} entity
     */
    DocumentDto getDocument(Long id);

    /**
     * Creates a {@link DocumentEntity} based on the provided {@link DocumentDto} object.
     *
     * @param documentDto the provided input object
     * @return {@link DocumentDto} representing the newly created {@link DocumentEntity} entity
     */
    DocumentDto createDocument(DocumentDto documentDto);

    /**
     * Modifies the designated {@link DocumentEntity} entity based on the provided {@link DocumentDto} object
     *
     * @param documentId the designated {@link DocumentEntity}
     * @param document   the new properties
     * @return the modified {@link DocumentDto}
     */
    DocumentDto modifyDocument(Long documentId, DocumentDto document);
}
