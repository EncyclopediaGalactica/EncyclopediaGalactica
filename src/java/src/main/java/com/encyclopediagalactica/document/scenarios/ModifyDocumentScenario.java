package com.encyclopediagalactica.document.scenarios;

import com.encyclopediagalactica.api.graphql.DocumentInput;
import com.encyclopediagalactica.api.graphql.DocumentResult;
import com.encyclopediagalactica.document.model.DocumentEntity;

public interface ModifyDocumentScenario {
    
    /**
     * Updates the designated {@link DocumentEntity} with the data provided by the {@link DocumentInput}
     * object.
     *
     * @param modifications the modifications
     * @return {@link DocumentResult} object including the changes
     */
    DocumentResult modify(DocumentInput modifications);
}
