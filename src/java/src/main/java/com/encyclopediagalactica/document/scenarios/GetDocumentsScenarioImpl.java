package com.encyclopediagalactica.document.scenarios;

import com.encyclopediagalactica.api.graphql.DocumentResult;
import com.encyclopediagalactica.document.infra.mappers.DocumentEntityMapper;
import com.encyclopediagalactica.document.infra.repositories.DocumentRepository;
import com.encyclopediagalactica.document.model.DocumentEntity;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
public class GetDocumentsScenarioImpl implements GetDocumentsScenario {
    
    private final DocumentRepository documentRepository;
    
    public GetDocumentsScenarioImpl(DocumentRepository documentRepository) {
        
        this.documentRepository = documentRepository;
    }
    
    @Override
    public List<DocumentResult> getAll() {
        
        List<DocumentEntity> documentEntities = documentRepository.findAll();
        return DocumentEntityMapper.INSTANCE.mapDocumentEntitiesToDocumentResults(documentEntities);
    }
}
