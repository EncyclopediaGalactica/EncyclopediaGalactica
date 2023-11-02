package com.andrascsanyi.encyclopediagalactica.iam.infra.mappers;

import com.andrascsanyi.encyclopediagalactica.iam.contracts.ModuleInput;
import com.andrascsanyi.encyclopediagalactica.iam.entities.Module;
import java.util.stream.Stream;
import org.junit.jupiter.api.extension.ExtensionContext;
import org.junit.jupiter.params.provider.Arguments;
import org.junit.jupiter.params.provider.ArgumentsProvider;

public class ModuleInputToModuleMapperDataSet implements ArgumentsProvider {

    @Override
    public Stream<? extends Arguments> provideArguments(ExtensionContext context) throws Exception {
        return Stream.of(
            Arguments.of(
                ModuleInput.builder().id(100L).name("name").description("desc").build(),
                Module.builder().id(100L).name("name").description("desc").build()
            ),
            Arguments.of(
                ModuleInput.builder().id(0L).name("").description("").build(),
                Module.builder().id(0L).name("").description("").build()
            ),
            Arguments.of(
                ModuleInput.builder().id(100L).name("  ").description("  ").build(),
                Module.builder().id(100L).name("  ").description("  ").build()
            )
        );
    }
}
