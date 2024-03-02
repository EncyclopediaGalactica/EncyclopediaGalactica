package com.encyclopediagalactica.document.scenarios;

import com.encyclopediagalactica.api.graphql.DocumentResult;
import com.encyclopediagalactica.document.model.DocumentEntity;

public interface GetDocumentByIdScenario {
    /**
     * Returns the designated {@link DocumentEntity} entity represented by a {@link DocumentResult}
     *
     * @param id identifier of the requested {@link DocumentEntity} entity
     * @return instance of {@link DocumentResult} entity
     */
    DocumentResult getById(Long id);
}
