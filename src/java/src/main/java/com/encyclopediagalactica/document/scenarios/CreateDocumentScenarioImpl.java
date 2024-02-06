package com.encyclopediagalactica.document.scenarios;

import com.encyclopediagalactica.api.graphql.Document;
import com.encyclopediagalactica.document.infra.mappers.DocumentEntityMapper;
import com.encyclopediagalactica.document.infra.repositories.DocumentRepository;
import com.encyclopediagalactica.document.infra.validation.CreateDocumentScenarioValidation;
import com.encyclopediagalactica.document.model.DocumentEntity;
import jakarta.validation.ConstraintViolation;
import jakarta.validation.ValidationException;
import jakarta.validation.Validator;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.Set;

@Service
public class CreateDocumentScenarioImpl implements CreateDocumentScenario {

    @Autowired
    private Validator validator;

    @Autowired
    private DocumentRepository documentRepository;

    @Override
    public Document create(Document document) {
        try {
            DocumentEntity documentEntity =
                    DocumentEntityMapper.INSTANCE.mapDocumentToDocumentEntity(document);
            validate(documentEntity);
            DocumentEntity savedEntity = documentRepository.save(documentEntity);
            return DocumentEntityMapper.INSTANCE.mapDocumentEntityToDocument(savedEntity);

        } catch (Exception exception) {
            throw new CreateDocumentScenarioException("Create scenarios process failed.",
                    exception);
        }
    }

    private void validate(DocumentEntity documentEntity) {
        Set<ConstraintViolation<DocumentEntity>> errors =
                validator.validate(documentEntity, CreateDocumentScenarioValidation.class);
        if (!errors.isEmpty()) {
            StringBuilder builder = new StringBuilder();
            errors.stream().map(item -> builder.append(item.getMessage()));
            throw new ValidationException(builder.toString());
        }
    }
}