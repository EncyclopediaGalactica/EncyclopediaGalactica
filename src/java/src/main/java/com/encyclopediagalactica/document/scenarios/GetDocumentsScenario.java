package com.encyclopediagalactica.document.scenarios;

import com.encyclopediagalactica.document.entities.DocumentEntity;

import java.util.List;

public interface GetDocumentsScenario {
    /**
     * Returns the list of {@link DocumentEntity} entities.
     *
     * @return list of {@link DocumentEntity} entities
     */
    List<DocumentEntity> getDocuments();
}
