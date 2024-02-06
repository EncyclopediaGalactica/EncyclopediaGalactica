package com.encyclopediagalactica.document.scenarios;

import com.encyclopediagalactica.api.graphql.Document;
import com.encyclopediagalactica.document.infra.mappers.DocumentEntityMapper;
import com.encyclopediagalactica.document.infra.repositories.DocumentNotFoundException;
import com.encyclopediagalactica.document.infra.repositories.DocumentRepository;
import com.encyclopediagalactica.document.infra.validation.ModifyDocumentScenarioValidation;
import com.encyclopediagalactica.document.model.DocumentEntity;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import javax.validation.ConstraintViolation;
import javax.validation.ValidationException;
import javax.validation.Validator;
import java.util.Objects;
import java.util.Set;

@Service
public class ModifyDocumentScenarioImpl implements ModifyDocumentScenario {

    @Autowired
    private Validator validator;

    @Autowired
    private DocumentRepository documentRepository;

    @Override
    public Document modify(Document input) {
        try {
            DocumentEntity toBeModified = checkIfExist(input);
            DocumentEntity mappedInput =
                    DocumentEntityMapper.INSTANCE.mapDocumentToDocumentEntity(input);
            validate(mappedInput);
            DocumentEntity result = overrideAndSave(toBeModified, mappedInput);
            return DocumentEntityMapper.INSTANCE.mapDocumentEntityToDocument(result);
        } catch (Exception e) {
            throw new ModifyDocumentScenarioException(e.getMessage(), e);
        }
    }

    private DocumentEntity overrideAndSave(DocumentEntity original, DocumentEntity modifications) {

        if (!Objects.equals(original.getName(), modifications.getName())) {
            original.setName(modifications.getName());
        }

        if (Objects.equals(original.getDesc(), modifications.getDesc())) {
            original.setDesc(modifications.getDesc());
        }

        documentRepository.save(original);
        return original;
    }

    private void validate(DocumentEntity documentEntity) {
        Set<ConstraintViolation<DocumentEntity>> result =
                validator.validate(documentEntity, ModifyDocumentScenarioValidation.class);
        if (!result.isEmpty()) {
            StringBuilder builder = new StringBuilder();
            result.stream().map(i -> builder.append(i.getMessage()).append(" "));
            throw new ValidationException(builder.toString());
        }

    }

    private DocumentEntity checkIfExist(Document document) {
        return documentRepository.findById(Long.parseLong(document.getId()))
                .orElseThrow(() -> new DocumentNotFoundException(String.format(
                        "Document Entity with id: %s has not been found.", document.getId())));
    }
}
