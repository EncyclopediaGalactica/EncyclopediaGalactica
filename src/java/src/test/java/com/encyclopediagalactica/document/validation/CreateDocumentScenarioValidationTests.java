package com.encyclopediagalactica.document.validation;

import com.encyclopediagalactica.document.infra.validation.CreateDocumentScenarioValidation;
import com.encyclopediagalactica.document.model.DocumentEntity;
import jakarta.validation.ConstraintViolation;
import jakarta.validation.Validator;
import org.junit.jupiter.params.ParameterizedTest;
import org.junit.jupiter.params.provider.Arguments;
import org.junit.jupiter.params.provider.MethodSource;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;

import java.util.Set;
import java.util.stream.Stream;

import static org.assertj.core.api.Assertions.assertThat;

@SpringBootTest
public class CreateDocumentScenarioValidationTests {

    @Autowired
    private Validator validator;

    public static Stream<Arguments> data() {
        return Stream.of(
                Arguments.of(1, "asd", "asd"),
                Arguments.of(0, null, "asd"),
                Arguments.of(0, "", "asd"),
                Arguments.of(0, "a", "asd"),
                Arguments.of(0, "asd", null),
                Arguments.of(0, "asd", ""),
                Arguments.of(0, "asd", "a")
        );
    }

    @ParameterizedTest
    @MethodSource("data")
    public void tests(
            Integer id,
            String name,
            String desc
    ) {
        // Given
        DocumentEntity entity = DocumentEntity.builder()
                .id(Long.valueOf(id))
                .name(name)
                .desc(desc)
                .build();
        // When
        Set<ConstraintViolation<DocumentEntity>> result = validator.validate(entity, CreateDocumentScenarioValidation.class);

        // Then
        assertThat(result.size()).isGreaterThanOrEqualTo(1);
    }
}
