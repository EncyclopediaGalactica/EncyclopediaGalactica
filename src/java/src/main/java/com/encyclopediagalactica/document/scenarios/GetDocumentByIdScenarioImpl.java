package com.encyclopediagalactica.document.scenarios;

import com.encyclopediagalactica.document.entities.DocumentEntity;
import com.encyclopediagalactica.document.repositories.DocumentNotFoundException;
import com.encyclopediagalactica.document.repositories.DocumentRepository;

public class GetDocumentByIdScenarioImpl implements GetDocumentByIdScenario {

    private final DocumentRepository documentRepository;

    public GetDocumentByIdScenarioImpl(DocumentRepository documentRepository) {
        this.documentRepository = documentRepository;
    }

    @Override
    public DocumentEntity getDocumentById(Long id) {
        return documentRepository.findById(id)
                .orElseThrow(() -> new DocumentNotFoundException(
                        String.format("Document with id: %s does not exist.", id)));
    }
}
