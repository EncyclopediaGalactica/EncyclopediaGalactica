package com.encyclopediagalactica.document.scenarios;

import com.encyclopediagalactica.api.graphql.Document;

public interface GetDocumentByIdScenario {
    /**
     * Returns the designated {@link Document} entity.
     *
     * @param id identifier of the {@link Document} entity
     * @return instance of {@link Document} entity
     */
    Document getById(Long id);
}
