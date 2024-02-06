package com.encyclopediagalactica.document.scenarios;

import com.encyclopediagalactica.document.infra.repositories.DocumentRepository;
import jakarta.validation.ValidationException;
import jakarta.validation.Validator;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class DeleteDocumentScenarioImpl implements DeleteDocumentScenario {

    @Autowired
    private Validator validator;

    @Autowired
    private DocumentRepository documentRepository;

    @Override
    public void delete(Long id) {
        try {
            deleteEntity(id);
        } catch (Exception e) {
            throw new DeleteDocumentScenarioException(
                    "Deleting document process failed.",
                    e
            );
        }
    }

    private void deleteEntity(Long id) {
        validateId(id);
        documentRepository.deleteById(id);
    }

    private void validateId(Long id) {
        if (id == 0) {
            throw new ValidationException();
        }
    }
}
