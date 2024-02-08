package com.encyclopediagalactica.document.scenarios;

import com.encyclopediagalactica.api.graphql.Document;
import com.encyclopediagalactica.api.graphql.DocumentInput;
import com.encyclopediagalactica.document.model.DocumentEntity;

public interface CreateDocumentScenario {
    /**
     * Creates a {@link DocumentEntity} based on the provided {@link Document} object.
     *
     * @param document the provided input object
     * @return {@link Document} representing the newly created {@link Document} entity
     */
    Document create(DocumentInput document);
}
