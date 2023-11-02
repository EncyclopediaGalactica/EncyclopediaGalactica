package com.andrascsanyi.encyclopediagalactica.document.validation;

import com.andrascsanyi.encyclopediagalactica.document.contracts.DocumentInput;
import com.andrascsanyi.encyclopediagalactica.document.validation.rules.AddNewDocumentInputValidationRule;
import jakarta.validation.ConstraintViolation;
import java.util.Set;
import org.springframework.stereotype.Service;

@Service
public class DocumentInputValidatorForCreateDocumentCommandImpl
    extends com.andrascsanyi.encyclopediagalactica.common.validator.ValidatorBase<DocumentInput>
    implements DocumentInputValidatorForCreateDocumentCommand<DocumentInput> {

    @Override
    public void validateAndThrow(DocumentInput o) {
        Set<ConstraintViolation<DocumentInput>> violations = validator
            .validate(o, AddNewDocumentInputValidationRule.class);
        checkViolationsAndThrow(violations);
    }
}
