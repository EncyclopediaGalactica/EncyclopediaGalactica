package com.encyclopediagalactica.document.scenarios;

import com.encyclopediagalactica.api.graphql.DocumentResult;
import com.encyclopediagalactica.document.model.DocumentEntity;

import java.util.List;

public interface GetDocumentsScenario {
    
    /**
     * Returns the list of {@link DocumentEntity} entities represented by {@link DocumentResult}.
     *
     * @return list of {@link DocumentResult} entities
     */
    List<DocumentResult> getAll();
}
