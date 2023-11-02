package com.andrascsanyi.encyclopediagalactica.iam.specification.application;

import static org.assertj.core.api.Assertions.assertThat;

import com.andrascsanyi.encyclopediagalactica.iam.application.AddModuleScenario;
import com.andrascsanyi.encyclopediagalactica.iam.application.GetModulesScenario;
import com.andrascsanyi.encyclopediagalactica.iam.contracts.ModuleInput;
import com.andrascsanyi.encyclopediagalactica.iam.contracts.ModuleOutput;
import java.util.List;
import org.junit.jupiter.params.ParameterizedTest;
import org.junit.jupiter.params.provider.ArgumentsSource;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.annotation.DirtiesContext;
import org.springframework.test.annotation.DirtiesContext.ClassMode;

@SpringBootTest
@DirtiesContext(classMode = ClassMode.AFTER_EACH_TEST_METHOD)
public class GetModulesScenarioShouldTests {

    @Autowired
    private GetModulesScenario getModulesScenario;

    @Autowired
    private AddModuleScenario addModuleScenario;

    @ParameterizedTest
    @ArgumentsSource(GetModulesScenarioShouldTestsDataSet.class)
    public void return_allItems(List<ModuleInput> input, List<ModuleOutput> expected) {

        // Arrange
        input.forEach(item -> {
            addModuleScenario.addModule(item);
        });

        // Act
        List<ModuleOutput> result = getModulesScenario.getModules();

        // Assert
        expected.forEach(item -> {
            ModuleOutput r = result.stream()
                .filter(f -> f.getName().equals(item.getName())
                    && f.getDescription().equals(item.getDescription()))
                .findFirst()
                .get();

            assertThat(r).isNotNull();
        });
    }
}
