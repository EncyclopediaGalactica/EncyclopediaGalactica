package com.encyclopediagalactica.document.scenarios;

import com.encyclopediagalactica.api.graphql.Document;
import com.encyclopediagalactica.document.model.DocumentEntity;

public interface ModifyDocumentScenario {

    /**
     * Updates the designated {@link DocumentEntity} with the data provided by the {@link Document}
     * object.
     *
     * @param modifications the modifications
     * @return {@link Document} object including the changes
     */
    Document modify(Document modifications);
}
