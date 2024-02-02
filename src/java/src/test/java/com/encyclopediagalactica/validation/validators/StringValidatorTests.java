package com.encyclopediagalactica.validation.validators;

import com.encyclopediagalactica.document.validation.validators.StringValidator;
import org.junit.jupiter.params.ParameterizedTest;
import org.junit.jupiter.params.provider.Arguments;
import org.junit.jupiter.params.provider.MethodSource;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;

import java.util.stream.Stream;

import static org.assertj.core.api.Assertions.assertThat;

@SpringBootTest
public class StringValidatorTests {

    @Autowired
    private StringValidator stringValidator;

    private static Stream<Arguments> longerOrEqualThanData() {
        return Stream.of(
            Arguments.of(null, 3, false),
            Arguments.of("", 3, false),
            Arguments.of("   ", 3, false),
            Arguments.of("asd", 3, true),
            Arguments.of("asdd", 3, true)
        );
    }

    @ParameterizedTest
    @MethodSource("longerOrEqualThanData")
    public void longerOrEqualThan(String str, int length, boolean expectedResult) {
        // Act
        boolean result = stringValidator.isLongerOrEqualThan(str, length);

        // Assert
        assertThat(result).isEqualTo(expectedResult);
    }

    public static Stream<Arguments> isNullData() {
        return Stream.of(
            Arguments.of(null, true),
            Arguments.of("not null", false)
        );
    }

    @ParameterizedTest
    @MethodSource("isNullData")
    public void isNull(String str, boolean expectedResult) {
        // Act
        boolean result = stringValidator.isNull(str);

        // Assert
        assertThat(result).isEqualTo(expectedResult);
    }

    public static Stream<Arguments> isEmptyData() {
        return Stream.of(
            Arguments.of("", true),
            Arguments.of("  ", true),
            Arguments.of("a", false)
        );
    }

    @ParameterizedTest
    @MethodSource("isEmptyData")
    public void isEmpty(String str, boolean expectedResult) {
        // Act
        boolean result = stringValidator.isEmpty(str);

        // Assert
        assertThat(result).isEqualTo(expectedResult);
    }
}
