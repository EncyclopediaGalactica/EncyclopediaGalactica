package com.encyclopediagalactica.validation.validators;

import com.encyclopediagalactica.document.validation.validators.LongValidator;
import org.junit.jupiter.params.ParameterizedTest;
import org.junit.jupiter.params.provider.Arguments;
import org.junit.jupiter.params.provider.MethodSource;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;

import java.util.stream.Stream;

import static org.assertj.core.api.Assertions.assertThat;

@SpringBootTest
public class LongValidatorTests {

    @Autowired
    private LongValidator longValidator;

    public static Stream<Arguments> equalsToData() {
        return Stream.of(
            Arguments.of(1L, 1L, true),
            Arguments.of(0L, 0L, true),
            Arguments.of(1L, 2L, false),
            Arguments.of(null, 2L, false),
            Arguments.of(1L, null, false),
            Arguments.of(null, null, false)
        );
    }


    @ParameterizedTest
    @MethodSource("equalsToData")
    public void equalsTo(Long number, Long equalsTo, boolean expectedResult) {

        // Act
        boolean result = longValidator.equalsTo(number, equalsTo);

        // Assert
        assertThat(result).isEqualTo(expectedResult);
    }
}
