package com.encyclopediagalactica.validation;

import com.encyclopediagalactica.document.dto.DocumentDto;
import com.encyclopediagalactica.document.validation.ValidationExcecption;
import com.encyclopediagalactica.document.validation.scenarios.CreateNewDocumentScenarioImplValidator;
import com.encyclopediagalactica.document.validation.validators.LongValidatorImpl;
import com.encyclopediagalactica.document.validation.validators.StringValidatorImpl;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.params.ParameterizedTest;
import org.junit.jupiter.params.provider.Arguments;
import org.junit.jupiter.params.provider.MethodSource;
import org.springframework.boot.test.context.SpringBootTest;

import java.util.stream.Stream;

import static com.encyclopediagalactica.document.validation.scenarios.ScenarioAbstractValidator.ValidationMode.THROW_AT_FIRST_ERROR;
import static org.assertj.core.api.Assertions.assertThatCode;
import static org.assertj.core.api.Assertions.assertThatThrownBy;

@SpringBootTest
public class CreateNewDocumentScenarioValidationTestsEntityEntity {

    private CreateNewDocumentScenarioImplValidator createNewDocumentScenarioValidator;

    @BeforeEach
    public void beforeEach() {
        createNewDocumentScenarioValidator = new CreateNewDocumentScenarioImplValidator(
                new StringValidatorImpl(),
                new LongValidatorImpl()
        );
    }

    private static Stream<Arguments> shouldThrow_whenInputIsInvalidProvider() {
        return Stream.of(
                Arguments.of(new DocumentDto.DocumentDtoBuilder().id(1L).name("asd").build()),
                Arguments.of(new DocumentDto.DocumentDtoBuilder().id(0L).name(null).build()),
                Arguments.of(new DocumentDto.DocumentDtoBuilder().id(0L).name("").build()),
                Arguments.of(new DocumentDto.DocumentDtoBuilder().id(0L).name(" ").build())
        );
    }

    @ParameterizedTest
    @MethodSource("shouldThrow_whenInputIsInvalidProvider")
    public void shouldThrow_whenInputIsInvalid(DocumentDto dto) {

        // Act && Assert
        assertThatThrownBy(() -> {
            createNewDocumentScenarioValidator.validateAndThrow(dto, THROW_AT_FIRST_ERROR);
        }).isInstanceOf(ValidationExcecption.class);
    }

    private static Stream<Arguments> shouldNotThrow_whenInputIsValidData() {
        return Stream.of(
                Arguments.of(new DocumentDto.DocumentDtoBuilder().id(0L).name("asd").build())
        );
    }

    @ParameterizedTest
    @MethodSource("shouldNotThrow_whenInputIsValidData")
    public void shouldNotThrow_whenInputIsValid(DocumentDto dto) {

        // Act && Assert
        assertThatCode(() -> {
            createNewDocumentScenarioValidator.validateAndThrow(dto);
        }).doesNotThrowAnyException();
    }
}
