package com.encyclopediagalactica.document.scenarios;

import com.encyclopediagalactica.api.graphql.Document;
import com.encyclopediagalactica.api.graphql.DocumentInput;
import com.encyclopediagalactica.document.infra.mappers.DocumentEntityMapper;
import com.encyclopediagalactica.document.infra.repositories.DocumentRepository;
import com.encyclopediagalactica.document.infra.validation.CreateDocumentScenarioValidation;
import com.encyclopediagalactica.document.model.DocumentEntity;
import com.encyclopediagalactica.utils.StringPropertyUtils;
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

    @Autowired
    private StringPropertyUtils stringPropertyUtils;

    @Override
    public Document create(DocumentInput documentInput) {
        try {
            DocumentEntity documentEntity =
                    DocumentEntityMapper.INSTANCE.mapDocumentInputToDocumentEntity(documentInput);
            stringPropertyUtils.stripStringProperties(documentEntity);
            validate(documentEntity);
            DocumentEntity savedEntity = documentRepository.save(documentEntity);
            return DocumentEntityMapper.INSTANCE.mapDocumentEntityToDocument(savedEntity);

        } catch (Exception exception) {
            throw new CreateDocumentScenarioException(
                    "Create scenarios process failed.",
                    exception);
        }
    }

    private void validate(DocumentEntity documentEntity) {
        Set<ConstraintViolation<DocumentEntity>> errors =
                validator.validate(documentEntity, CreateDocumentScenarioValidation.class);
        if (!errors.isEmpty()) {
            StringBuilder builder = new StringBuilder();
            errors.forEach(item -> builder.append(item.getMessage()));
            throw new ValidationException(builder.toString());
        }
    }
}
