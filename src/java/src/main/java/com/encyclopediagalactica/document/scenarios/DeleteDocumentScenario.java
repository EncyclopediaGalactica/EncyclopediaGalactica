package com.encyclopediagalactica.document.scenarios;

import com.encyclopediagalactica.document.model.DocumentEntity;

public interface DeleteDocumentScenario {

    /**
     * Deletes the designated {@link DocumentEntity} from the system.
     *
     * @param id the id of the entity will be deleted.
     */
    void delete(Long id);
}
