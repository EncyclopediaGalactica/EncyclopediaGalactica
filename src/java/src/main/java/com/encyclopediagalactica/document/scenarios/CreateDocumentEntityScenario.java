package com.encyclopediagalactica.document.scenarios;

import com.encyclopediagalactica.api.graphql.Document;
import com.encyclopediagalactica.document.entities.DocumentEntity;

public interface CreateDocumentEntityScenario {
    /**
     * Creates a {@link DocumentEntity} based on the provided {@link DocumentEntity} object.
     *
     * @param documentEntityEntityDto the provided input object
     * @return {@link DocumentEntity} representing the newly created {@link DocumentEntity} entity
     */
    Document createDocument(Document documentEntityEntityDto);
}
