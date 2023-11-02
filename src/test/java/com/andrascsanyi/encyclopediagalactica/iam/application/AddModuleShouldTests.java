package com.andrascsanyi.encyclopediagalactica.iam.application;

import static org.assertj.core.api.Assertions.assertThat;

import com.andrascsanyi.encyclopediagalactica.iam.contracts.ModuleInput;
import com.andrascsanyi.encyclopediagalactica.iam.contracts.ModuleOutput;
import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.annotation.DirtiesContext;
import org.springframework.test.annotation.DirtiesContext.ClassMode;

@SpringBootTest
@DirtiesContext(classMode = ClassMode.AFTER_EACH_TEST_METHOD)
public class AddModuleShouldTests {

    @Autowired
    private AddModuleScenario addModuleScenario;

    @Test
    public void throw_whenNameUniqueConstraintIsViolated() {

        // Arrange
        ModuleInput moduleInput = ModuleInput.builder()
            .name("name")
            .description("description")
            .build();

        // Act
        ModuleOutput result = addModuleScenario.addModule(moduleInput);

        // Assert
        assertThat(result.getId()).isGreaterThanOrEqualTo(1);
        assertThat(result.getName()).isEqualTo(moduleInput.getName());
        assertThat(result.getDescription()).isEqualTo(moduleInput.getDescription());
    }

}
