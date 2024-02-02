package com.encyclopediagalactica.document.scenarios;

import com.encyclopediagalactica.api.graphql.Document;
import com.encyclopediagalactica.document.entities.DocumentEntity;
import com.encyclopediagalactica.document.mappers.DocumentEntityMapper;
import com.encyclopediagalactica.document.repositories.DocumentRepository;
import jakarta.validation.ValidationException;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.validation.Errors;
import org.springframework.validation.Validator;

import java.util.stream.Collectors;

@Service
public class CreateDocumentEntityScenarioImpl implements CreateDocumentEntityScenario {

    @Autowired
    private Validator validator;

    @Autowired
    private DocumentRepository documentRepository;

    @Override
    public Document createDocument(Document document) {
        try {

            validate(document);
            DocumentEntity documentEntity = DocumentEntityMapper.INSTANCE.mapDocumentToDocumentEntity(document);
            DocumentEntity savedEntity = documentRepository.save(documentEntity);
            Document result = DocumentEntityMapper.INSTANCE.mapDocumentEntityToDocument(savedEntity);

        } catch (Exception exception) {
            throw new CreateDocumentScenarioException(
                    "Create scenarios process failed.",
                    exception
            );
        }
    }

    private void validate(Document document) {
        Errors errors = validator.validateObject(document);
        if (errors.hasErrors()) {
            StringBuilder builder = new StringBuilder();

            errors.getAllErrors().stream()
                    .map(i -> builder.append(i.getDefaultMessage()).append(" "))
                    .collect(Collectors.toList());

            throw new ValidationException();
        }
    }
}
