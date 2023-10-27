package com.andrascsanyi.encyclopediagalactica.document.validation;

import static org.assertj.core.api.Assertions.assertThat;

import com.andrascsanyi.encyclopediagalactica.document.contracts.DocumentInput;
import com.andrascsanyi.encyclopediagalactica.document.validation.rules.AddNewDocumentInputInvalidDataSet;
import com.andrascsanyi.encyclopediagalactica.document.validation.rules.AddNewDocumentInputValidationRule;
import jakarta.validation.ConstraintViolation;
import jakarta.validation.Validation;
import jakarta.validation.Validator;
import java.util.Set;
import org.junit.jupiter.api.BeforeAll;
import org.junit.jupiter.params.ParameterizedTest;
import org.junit.jupiter.params.provider.ArgumentsSource;

public class DocumentInputValidationTests {

    private static Validator validator;

    @BeforeAll
    public static void Init() {
        validator = Validation.buildDefaultValidatorFactory().getValidator();
    }

    @ParameterizedTest
    @ArgumentsSource(AddNewDocumentInputInvalidDataSet.class)
    void throwWhenInputIsInvalidForAddNew(DocumentInput input) {

        // Act
        Set<ConstraintViolation<DocumentInput>> violations = validator
            .validate(input, AddNewDocumentInputValidationRule.class);

        // Assert
        assertThat(violations.size()).isGreaterThanOrEqualTo(1);
    }

}
