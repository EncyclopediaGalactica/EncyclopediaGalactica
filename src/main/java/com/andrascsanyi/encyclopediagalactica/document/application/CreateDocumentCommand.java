package com.andrascsanyi.encyclopediagalactica.document.application;


import com.andrascsanyi.encyclopediagalactica.document.application.exceptions.InputValidationException;
import com.andrascsanyi.encyclopediagalactica.document.application.exceptions.NotFoundException;
import com.andrascsanyi.encyclopediagalactica.document.application.exceptions.UnknownErrorException;
import com.andrascsanyi.encyclopediagalactica.document.application.exceptions.ValueAlreadyExistsException;
import com.andrascsanyi.encyclopediagalactica.document.contracts.DocumentInput;
import com.andrascsanyi.encyclopediagalactica.document.contracts.DocumentResult;
import com.andrascsanyi.encyclopediagalactica.document.entities.Document;

/**
 * Creates {@link Document} entity in the system.
 */
public interface CreateDocumentCommand {

    /**
     * Executes the business logic to create a new {@link Document} based on the provided
     * {@link DocumentInput} entity and returns a {@link DocumentResult} Dto representing the
     * created entity.
     *
     * @param input {@link DocumentInput}
     * @return {@link DocumentResult}
     * @throws InputValidationException    when the provided input is invalid
     * @throws NotFoundException           when the entity not found
     * @throws ValueAlreadyExistsException when some unique attribute already exists
     * @throws UnknownErrorException       when unknown error happens
     */
    DocumentResult execute(DocumentInput input);
}
