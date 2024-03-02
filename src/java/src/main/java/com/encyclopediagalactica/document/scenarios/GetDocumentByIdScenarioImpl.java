package com.encyclopediagalactica.document.scenarios;

import com.encyclopediagalactica.api.graphql.DocumentResult;
import com.encyclopediagalactica.document.infra.mappers.DocumentEntityMapper;
import com.encyclopediagalactica.document.infra.repositories.DocumentNotFoundException;
import com.encyclopediagalactica.document.infra.repositories.DocumentRepository;
import com.encyclopediagalactica.document.model.DocumentEntity;
import org.springframework.stereotype.Service;

@Service
public class GetDocumentByIdScenarioImpl implements GetDocumentByIdScenario {
    
    private final DocumentRepository documentRepository;
    
    public GetDocumentByIdScenarioImpl(DocumentRepository documentRepository) {
        
        this.documentRepository = documentRepository;
    }
    
    @Override
    public DocumentResult getById(Long id) {
        
        DocumentEntity result = documentRepository.findById(id)
            .orElseThrow(() -> new DocumentNotFoundException(
                String.format("Document with id: %s does not exist.", id)));
        return DocumentEntityMapper.INSTANCE.mapDocumentEntityToDocument(result);
    }
}
