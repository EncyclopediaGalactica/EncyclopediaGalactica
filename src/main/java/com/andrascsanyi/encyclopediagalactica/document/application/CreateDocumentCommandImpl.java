package com.andrascsanyi.encyclopediagalactica.document.application;

import com.andrascsanyi.encyclopediagalactica.common.guard.Guards;
import com.andrascsanyi.encyclopediagalactica.common.guard.exceptions.ObjectIsNullException;
import com.andrascsanyi.encyclopediagalactica.common.validator.exceptions.ValidationException;
import com.andrascsanyi.encyclopediagalactica.document.application.exceptions.InputValidationException;
import com.andrascsanyi.encyclopediagalactica.document.application.exceptions.UnknownErrorException;
import com.andrascsanyi.encyclopediagalactica.document.contracts.DocumentInput;
import com.andrascsanyi.encyclopediagalactica.document.contracts.DocumentResult;
import com.andrascsanyi.encyclopediagalactica.document.entities.Document;
import com.andrascsanyi.encyclopediagalactica.document.infra.mappers.DocumentMapper;
import com.andrascsanyi.encyclopediagalactica.document.infra.repositories.DocumentRepository;
import com.andrascsanyi.encyclopediagalactica.document.validation.DocumentInputValidatorForCreateDocumentCommand;
import lombok.NonNull;
import org.springframework.stereotype.Service;

@Service
public class CreateDocumentCommandImpl implements CreateDocumentCommand {

    private final DocumentInputValidatorForCreateDocumentCommand<DocumentInput> validator;
    private final Guards guards;
    private final DocumentMapper mapper;
    private final DocumentRepository documentRepository;

    public CreateDocumentCommandImpl(
        @NonNull DocumentInputValidatorForCreateDocumentCommand<DocumentInput> validatorImpl,
        @NonNull Guards guardsImpl,
        @NonNull DocumentMapper mapperImpl,
        @NonNull DocumentRepository documentRepositoryImpl) {
        validator = validatorImpl;
        guards = guardsImpl;
        mapper = mapperImpl;
        documentRepository = documentRepositoryImpl;
    }

    @Override
    public DocumentResult execute(DocumentInput input) {
        try {
            return executeBusinessLogic(input);
        } catch (ObjectIsNullException
                 | ValidationException e) {
            throw new InputValidationException(
                "The provided input is null.",
                e
            );
        } catch (Exception e) {
            throw new UnknownErrorException(
                "Unknown error happened.",
                e
            );
        }
    }

    private DocumentResult executeBusinessLogic(DocumentInput input) {
        guards.ObjectGuards().throwIfNull(input);
        validator.validateAndThrow(input);
        Document inputDocument = mapper.documentInputToDocument(input);
        Document result = documentRepository.save(inputDocument);
        return mapper.documentToDocumentInput(result);
    }

}
