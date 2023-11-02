package com.andrascsanyi.encyclopediagalactica.iam.infra.mappers.datasets;

import com.andrascsanyi.encyclopediagalactica.iam.contracts.ModuleOutput;
import com.andrascsanyi.encyclopediagalactica.iam.entities.Module;
import java.util.Arrays;
import java.util.Collections;
import java.util.List;
import java.util.stream.Stream;
import org.junit.jupiter.api.extension.ExtensionContext;
import org.junit.jupiter.params.provider.Arguments;
import org.junit.jupiter.params.provider.ArgumentsProvider;

public class ModuleListToModuleOutputListDataSet implements ArgumentsProvider {

    @Override
    public Stream<? extends Arguments> provideArguments(ExtensionContext context) throws Exception {
        return Stream.of(
            Arguments.of(List.of(), List.of()),
            Arguments.of(
                Collections.singletonList(
                    Module.builder().id(100L).name("name").description("desc").build()),
                Collections.singletonList(
                    ModuleOutput.builder().id(100L).name("name").description("desc").build())
            ),
            Arguments.of(
                Arrays.asList(
                    Module.builder().id(100L).name("name").description("desc").build(),
                    Module.builder().id(200L).name("name2").description("desc2").build()
                ),
                Arrays.asList(
                    ModuleOutput.builder().id(100L).name("name").description("desc").build(),
                    ModuleOutput.builder().id(200L).name("name2").description("desc2").build()
                )
            ));
    }
}
