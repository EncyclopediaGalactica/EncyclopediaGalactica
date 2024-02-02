package com.encyclopediagalactica.document.scenarios;

import com.encyclopediagalactica.document.entities.DocumentEntity;

public interface GetDocumentByIdScenario {
    /**
     * Returns the designated {@link DocumentEntity} entity.
     *
     * @param id identifier of the {@link DocumentEntity} entity
     * @return instance of {@link DocumentEntity} entity
     */
    DocumentEntity getDocumentById(Long id);
}
