package com.andrascsanyi.encyclopediagalactica.iam.infra.mappers;

import static org.assertj.core.api.Assertions.assertThat;

import com.andrascsanyi.encyclopediagalactica.iam.contracts.ModuleInput;
import com.andrascsanyi.encyclopediagalactica.iam.contracts.ModuleOutput;
import com.andrascsanyi.encyclopediagalactica.iam.entities.Module;
import java.util.List;
import org.junit.jupiter.params.ParameterizedTest;
import org.junit.jupiter.params.provider.ArgumentsSource;

public class ModuleMapperShouldTests {

    @ParameterizedTest
    @ArgumentsSource(ModuleInputToModuleMapperDataSet.class)
    public void map_moduleInputToModule(ModuleInput input, Module expected) {
        // Act
        Module result = ModuleMappers.INSTANCE.mapModuleInputToModule(input);

        // Assert
        assertThat(result.getId()).isEqualTo(expected.getId());
        assertThat(result.getName()).isEqualTo(expected.getName());
        assertThat(result.getDescription()).isEqualTo(expected.getDescription());
    }

    @ParameterizedTest
    @ArgumentsSource(ModuleToModuleOutputDataSet.class)
    public void map_moduleToModuleOutput(Module module, ModuleOutput expected) {
        // Act
        ModuleOutput result = ModuleMappers.INSTANCE.mapModuleToModuleOutput(module);

        // Assert
        assertThat(result.getId()).isEqualTo(expected.getId());
        assertThat(result.getName()).isEqualTo(expected.getName());
        assertThat(result.getDescription()).isEqualTo(expected.getDescription());
    }

    @ParameterizedTest
    @ArgumentsSource(ModuleListToModuleOutputListDataSet.class)
    public void map_ModuleListToModuleOutputList(List<Module> input, List<ModuleOutput> expected) {
        // Act
        List<ModuleOutput> result = ModuleMappers.INSTANCE
            .mapModuleListToModuleOutputList(input);

        // Assert
        assertThat(result.size()).isEqualTo(expected.size());

        expected.forEach(item -> {
            ModuleOutput res = expected.stream().filter(i -> i.getId().equals(item.getId()))
                .findFirst().get();
            assertThat(res.getId()).isEqualTo(item.getId());
            assertThat(res.getName()).isEqualTo(item.getName());
            assertThat(res.getDescription()).isEqualTo(item.getDescription());
        });
    }
}
