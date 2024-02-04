package com.encyclopediagalactica.document.scenarios;

import com.encyclopediagalactica.api.graphql.Document;

import java.util.List;

public interface GetDocumentsScenario {

    /**
     * Returns the list of {@link Document} entities.
     *
     * @return list of {@link Document} entities
     */
    List<Document> getAll();
}
