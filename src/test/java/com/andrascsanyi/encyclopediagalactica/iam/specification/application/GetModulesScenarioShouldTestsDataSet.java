package com.andrascsanyi.encyclopediagalactica.iam.specification.application;

import com.andrascsanyi.encyclopediagalactica.iam.contracts.ModuleInput;
import com.andrascsanyi.encyclopediagalactica.iam.contracts.ModuleOutput;
import java.util.Arrays;
import java.util.stream.Stream;
import org.junit.jupiter.api.extension.ExtensionContext;
import org.junit.jupiter.params.provider.Arguments;
import org.junit.jupiter.params.provider.ArgumentsProvider;

public class GetModulesScenarioShouldTestsDataSet implements ArgumentsProvider {

    @Override
    public Stream<? extends Arguments> provideArguments(ExtensionContext context) throws Exception {
        return Stream.of(
            Arguments.of(
                Arrays.asList(),
                Arrays.asList()
            ),
            Arguments.of(
                Arrays.asList(
                    ModuleInput.builder().name("111").description("111").build()
                ),
                Arrays.asList(
                    ModuleOutput.builder().name("111").description("111").build()
                )
            ),
            Arguments.of(
                Arrays.asList(
                    ModuleInput.builder().name("111").description("111").build(),
                    ModuleInput.builder().name("222").description("222").build()
                ),
                Arrays.asList(
                    ModuleOutput.builder().name("111").description("111").build(),
                    ModuleOutput.builder().name("222").description("222").build()
                )
            )
        );
    }
}
