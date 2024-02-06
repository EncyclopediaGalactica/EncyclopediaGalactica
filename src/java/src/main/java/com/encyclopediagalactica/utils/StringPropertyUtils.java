package com.encyclopediagalactica.utils;

import com.encyclopediagalactica.document.model.DocumentEntity;

public interface StringPropertyUtils {

    /**
     * Removes all the whitespace characters from the start and the end of
     * string properties.
     *
     * @param documentEntity a {@link DocumentEntity} entity.
     */
    void stripStringProperties(DocumentEntity documentEntity);
}
