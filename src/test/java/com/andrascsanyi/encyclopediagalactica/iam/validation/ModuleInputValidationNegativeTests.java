package com.andrascsanyi.encyclopediagalactica.iam.validation;

import static org.assertj.core.api.Assertions.assertThat;

import com.andrascsanyi.encyclopediagalactica.iam.contracts.ModuleInput;
import com.andrascsanyi.encyclopediagalactica.iam.validation.rules.AddModuleScenarioGroup;
import com.andrascsanyi.encyclopediagalactica.iam.validation.rules.ModuleInput_InvalidDataSetFor_AddModuleScenario;
import jakarta.validation.ConstraintViolation;
import jakarta.validation.Validation;
import jakarta.validation.Validator;
import java.util.Set;
import org.junit.jupiter.api.BeforeAll;
import org.junit.jupiter.params.ParameterizedTest;
import org.junit.jupiter.params.provider.ArgumentsSource;

public class ModuleInputValidationNegativeTests {

    private static Validator validator;

    @BeforeAll
    public static void Init() {
        validator = Validation.buildDefaultValidatorFactory().getValidator();
    }

    @ParameterizedTest
    @ArgumentsSource(ModuleInput_InvalidDataSetFor_AddModuleScenario.class)
    void throwWhenInputIsInvalidForAddNew(ModuleInput input) {

        // Act
        Set<ConstraintViolation<ModuleInput>> violations = validator
            .validate(input, AddModuleScenarioGroup.class);

        // Assert
        assertThat(violations.size()).isGreaterThanOrEqualTo(1);
    }
}
