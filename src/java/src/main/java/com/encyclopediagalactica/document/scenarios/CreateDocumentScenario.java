package com.encyclopediagalactica.document.scenarios;

import com.encyclopediagalactica.api.graphql.DocumentInput;
import com.encyclopediagalactica.api.graphql.DocumentResult;
import com.encyclopediagalactica.document.model.DocumentEntity;

public interface CreateDocumentScenario {
    /**
     * Creates a {@link DocumentEntity} based on the provided {@link DocumentInput} object.
     *
     * @param document the provided input object
     * @return {@link DocumentResult} representing the newly created {@link DocumentEntity} entity
     */
    DocumentResult create(DocumentInput document);
}
