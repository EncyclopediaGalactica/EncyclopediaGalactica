package com.encyclopediagalactica.document.scenarios;

import com.encyclopediagalactica.document.entities.DocumentEntity;
import com.encyclopediagalactica.document.repositories.DocumentRepository;

import java.util.List;

public class GetDocumentsScenarioImpl implements GetDocumentsScenario {

    private final DocumentRepository documentRepository;

    public GetDocumentsScenarioImpl(DocumentRepository documentRepository) {
        this.documentRepository = documentRepository;
    }

    @Override
    public List<DocumentEntity> getDocuments() {
        return documentRepository.findAll();
    }
}
