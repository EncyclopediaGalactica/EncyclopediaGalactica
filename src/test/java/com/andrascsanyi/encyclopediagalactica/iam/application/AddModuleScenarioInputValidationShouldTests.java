package com.andrascsanyi.encyclopediagalactica.iam.application;

import static org.assertj.core.api.Assertions.assertThatThrownBy;

import com.andrascsanyi.encyclopediagalactica.iam.application.exceptions.InputValidationException;
import com.andrascsanyi.encyclopediagalactica.iam.application.exceptions.ValueAlreadyExistsException;
import com.andrascsanyi.encyclopediagalactica.iam.contracts.ModuleInput;
import com.andrascsanyi.encyclopediagalactica.iam.contracts.ModuleOutput;
import com.andrascsanyi.encyclopediagalactica.iam.validation.rules.ModuleInput_InvalidDataSetFor_AddModuleScenario;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.params.ParameterizedTest;
import org.junit.jupiter.params.provider.ArgumentsSource;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.annotation.DirtiesContext;
import org.springframework.test.annotation.DirtiesContext.ClassMode;

@SpringBootTest
@DirtiesContext(classMode = ClassMode.AFTER_EACH_TEST_METHOD)
public class AddModuleScenarioInputValidationShouldTests {

    @Autowired
    private AddModuleScenario addModuleScenario;

    @ParameterizedTest
    @ArgumentsSource(ModuleInput_InvalidDataSetFor_AddModuleScenario.class)
    public void throw_whenInputIsInvalid(ModuleInput input) {
        assertThatThrownBy(() -> addModuleScenario.addModule(input))
            .isInstanceOf(InputValidationException.class);

    }

    @Test
    public void throw_whenNameUniqueConstraintIsViolated() {

        // Arrange
        ModuleInput moduleInput = ModuleInput.builder()
            .name("name")
            .description("description")
            .build();
        ModuleOutput result = addModuleScenario.addModule(moduleInput);

        // Act
        assertThatThrownBy(() -> addModuleScenario.addModule(moduleInput))
            .isInstanceOf(ValueAlreadyExistsException.class);
    }
}
