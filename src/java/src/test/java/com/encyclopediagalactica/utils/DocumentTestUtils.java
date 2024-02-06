package com.encyclopediagalactica.utils;

import com.encyclopediagalactica.api.graphql.Document;
import com.encyclopediagalactica.document.model.DocumentEntity;

public interface DocumentTestUtils {
    Iterable<DocumentEntity> createDocumentEntities(int amount, Boolean isIdZero);

    /**
     * Creates the given amount {@link Document} object.
     *
     * @param amount   the amount
     * @param isIdZero if true Id value will be zero at every item
     * @return {@link java.util.List<Document>}
     */
    Iterable<Document> createDocuments(int amount, Boolean isIdZero);
}
