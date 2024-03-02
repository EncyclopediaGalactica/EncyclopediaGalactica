package com.encyclopediagalactica.utils;

import com.encyclopediagalactica.api.graphql.DocumentInput;
import com.encyclopediagalactica.document.model.DocumentEntity;

public interface DocumentTestUtils {
    Iterable<DocumentEntity> createDocumentEntities(int amount, Boolean isIdZero);
    
    /**
     * Creates the given amount {@link DocumentInput} object.
     *
     * @param amount   the amount
     * @param isIdZero if true Id value will be zero at every item
     * @return {@link java.util.List<DocumentInput>}
     */
    Iterable<DocumentInput> createDocuments(int amount, Boolean isIdZero);
}
